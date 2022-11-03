using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject PowerUpPrefab;
    public float xRange = 3;
    public float yRange = 2.5f;
    public int EnemyCount;
    public int Wavecount = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(Wavecount);
         // Spanws the power up in a random location in between the positions listed in line 35 and 36
          Instantiate(PowerUpPrefab, GenerateSpawnPosition(), PowerUpPrefab.transform.rotation);
          
    }

    // Update is called once per frame
    void Update()
    {
        //Looks for the same object(s)
        EnemyCount = FindObjectsOfType<EnemyScript>().Length;
        if(EnemyCount == 0)
        {
            Wavecount++;
            //Wavecount = Wavecount + 1; also does the same thing (same with Wavecount--;)
            SpawnEnemyWave(Wavecount);
            Instantiate(PowerUpPrefab, GenerateSpawnPosition(), PowerUpPrefab.transform.rotation);
        }
    }
    private Vector2 GenerateSpawnPosition()
    {
        float spawnXPos = Random.Range(-xRange , xRange);
        float spawnYPos = Random.Range(-yRange, yRange);
        Vector2 randomPos = new Vector2(spawnXPos, spawnYPos);
        return randomPos;
    }
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        //Looping statment (i is an index) variable + 1
        for(int i = 0; i < enemiesToSpawn; i ++)
        {
            Instantiate(EnemyPrefab, GenerateSpawnPosition(), EnemyPrefab.transform.rotation); 
        }
      
    }
}
