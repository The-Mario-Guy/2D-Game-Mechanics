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
  private SpriteRenderer _playerSR;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
     float spawnXPos = Random.Range(-xRange , xRange);
      float spawnYPos = Random.Range(-yRange, yRange);
      _playerSR = GetComponent<SpriteRenderer>();
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
          StartCoroutine(DeadRoutine());
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
    IEnumerator DeadRoutine()
    {
     Instantiate(playerFx, transform.position, playerFx.transform.rotation);
     powerUpIndicator.gameObject.SetActive(false);
     hasPowerUp = false;
     //Unity tells the Sprite renderer that this Sprite's renderer is false (there is no _playerSR.disabled)
     _playerSR.enabled = false;
     //gameObject.SetActive(false); (if this is started in the middle of code the program won't finish)
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(0);
    }
}