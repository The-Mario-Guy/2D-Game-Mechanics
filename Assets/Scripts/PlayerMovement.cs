using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 10;
  public float xRange = 3;
 public float yRange = 2.5f;
 public float powerUpStrength = 0.2f;
 public GameObject playerFx;
 public GameObject powerUpIndicator;
 public bool hasPowerUp = false;
  private Rigidbody2D _playerRb;
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
     float spawnXPos = Random.Range(-xRange , xRange);
        float spawnYPos = Random.Range(-yRange, yRange);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Allows for movement to be made
        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        _playerRb.AddForce(direction * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("OoB"))
      {
          Instantiate(playerFx, transform.position, playerFx.transform.rotation);
          gameObject.SetActive(false);
          SceneManager.LoadScene(0);
      }
      if(other.gameObject.CompareTag("PowerUp"))
      {
        Destroy(other.gameObject);
        powerUpIndicator.gameObject.SetActive(true);
        hasPowerUp = true;
        StartCoroutine(PowerupCountdownRoutine());
      }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
      if(other.gameObject.CompareTag("Enemy") && hasPowerUp)
      {
        //If you bump into an enemy with a power up the player will grab the Enemy's rigidbody and provide a force to it as well as what direction the force is being applied to
        Rigidbody2D enemyRB = other.gameObject.GetComponent<Rigidbody2D>();
        Vector2 awayFromPlayer = (other.gameObject.transform.position - transform.position);
        enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode2D.Impulse);

        powerUpIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
      }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        powerUpIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
    }
}