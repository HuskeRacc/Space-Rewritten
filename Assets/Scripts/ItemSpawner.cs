using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [SerializeField] Transform[] smallSpawnPoints;
    [SerializeField] GameObject[] foodItems;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnSmallItem(int itemToSpawn)
    {
        Instantiate(foodItems[itemToSpawn], smallSpawnPoints[Random.Range(0,smallSpawnPoints.Length)].position, Quaternion.identity);
    }
}
