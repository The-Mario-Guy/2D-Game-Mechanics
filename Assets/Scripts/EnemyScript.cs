using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D _enemyRb;
    private GameObject _player;
    public GameObject enemyFx;
    // Start is called before the first frame update
    void Start()
    {
        //Grabs info from the Object's RigidBody
        _enemyRb = GetComponent<Rigidbody2D>();
        //Looks for the Player
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Teels the Enemy to look at the Player's position. Afterwords Force is applied to the Enemy towards the Player's Position 
        Vector2 lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * Speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("OoB"))
      {
          Instantiate(enemyFx, transform.position, enemyFx.transform.rotation);
          Destroy(this.gameObject);
      }
    }
}
