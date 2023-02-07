using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [SerializeField] Transform[] smallSpawnPoints;
    [SerializeField] GameObject[] itemsToSpawn;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnSmallItem(int itemToSpawn)
    {
        Instantiate(itemsToSpawn[itemToSpawn], smallSpawnPoints[Random.Range(0,smallSpawnPoints.Length)].position, Quaternion.identity);
    }
}
