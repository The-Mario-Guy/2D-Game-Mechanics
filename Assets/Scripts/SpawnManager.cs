using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float xRange = 3;
    public float yRange = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        float spawnXPos = Random.Range(-xRange , xRange);
        float spawnYPos = Random.Range(-yRange, yRange);
// Spanws the Enemey in a random location in between the positions above
        Instantiate(EnemyPrefab, new Vector2(spawnXPos, spawnYPos), EnemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
