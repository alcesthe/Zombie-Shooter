using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPoints;
    [SerializeField] Zombie zombiePrefab;
    [SerializeField] int numberOfZombieEachWave = 5;
    [SerializeField] float timeBetweenEachSpawn = 5.0f;


    private Transform[] listOfSpawnPoints;
    
    
    // Start is called before the first frame update
    void Start()
    {
        listOfSpawnPoints = spawnPoints.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (CheckIsAllZombieAreDead())
        {
            StartCoroutine(SpawnZombies());
        }
    }

    IEnumerator SpawnZombies()
    {
        for (int i = 0; i < numberOfZombieEachWave; i++)
        {
            int index = Random.Range(1, listOfSpawnPoints.Length);
            Instantiate(zombiePrefab, listOfSpawnPoints[index].transform.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenEachSpawn);
        }
    }

    private bool CheckIsAllZombieAreDead()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        return zombies.Length <= 0;
    }
}
