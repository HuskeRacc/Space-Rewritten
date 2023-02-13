using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [SerializeField] Transform[] smallSpawnPoints;
    [SerializeField] GameObject[] itemsToSpawn;

    [SerializeField] ParticleSystem selectedParticleSystem;

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
        int randomSelection = Random.Range(0, smallSpawnPoints.Length);
        selectedParticleSystem = smallSpawnPoints[randomSelection].GetComponent<ParticleSystem>();
        selectedParticleSystem.Play();
        craftingAudioSource.PlayOneShot(craftingCompleteAudioClip);
        Instantiate(itemsToSpawn[localItemToSpawn], smallSpawnPoints[randomSelection].position, Quaternion.identity);

    }
}
