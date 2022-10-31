using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 10;
  public float xRange = 3;
 public float yRange = 2.5f;
 public GameObject playerFx;
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
          Destroy(this.gameObject);
          gameObject.SetActive(false);
          SceneManager.LoadScene(0);
      }
    }
    
}