using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public GameObject playerFx;
    public GameObject enemyFx;
private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.CompareTag("Player"))
      {
          Instantiate(playerFx, other.transform.position, playerFx.transform.rotation);
          Destroy(other.gameObject);
      }
    }
}
