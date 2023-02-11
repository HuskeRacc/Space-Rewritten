using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [SerializeField] Transform[] smallSpawnPoints;
    [SerializeField] GameObject[] itemsToSpawn;

    public float craftingTime = 10f;

    [SerializeField] AudioSource craftingAudioSource;
    [SerializeField] AudioClip craftingAudioClip;
    [SerializeField] AudioClip craftingCompleteAudioClip;

    int localItemToSpawn;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnSmallItem(int itemToSpawn)
    {
        localItemToSpawn = itemToSpawn;
        craftingAudioSource.PlayOneShot(craftingAudioClip);
        Invoke(nameof(SpawnSmallItemDelay), craftingTime);
    }

    void SpawnSmallItemDelay()
    {
        craftingAudioSource.PlayOneShot(craftingCompleteAudioClip);
        Instantiate(itemsToSpawn[localItemToSpawn], smallSpawnPoints[Random.Range(0, smallSpawnPoints.Length)].position, Quaternion.identity);
    }
}
