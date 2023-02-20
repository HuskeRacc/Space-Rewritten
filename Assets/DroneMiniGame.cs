using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroneMiniGame : MonoBehaviour
{
    [SerializeField] GameObject[] clickableAsteroidsGO;
    [SerializeField] int asteroidsSpawned = 0;

    [SerializeField] float materialGainAmountMin;
    [SerializeField] float materialGainAmountMax;

    private void Start()
    {
        StartCoroutine(SetSpawnRate());
    }

    IEnumerator SetSpawnRate()
    {
        if (DroneManager.instance.status == 2 && asteroidsSpawned == 0 && DroneManager.instance.mode == 0)
        {
            int randomSpawnTime = Random.Range(30, 120);
            Debug.Log("Asteroid spawn coroutine success, staring cooldown of: " + randomSpawnTime.ToString());
            yield return new WaitForSeconds(randomSpawnTime);

            if(DroneManager.instance.status == 2 && asteroidsSpawned == 0 && DroneManager.instance.mode == 0)
                Invoke(nameof(SpawnRandomAsteroid), 0);
        }
        yield return new WaitForSeconds(10);
        StartCoroutine(SetSpawnRate());
    }

    void SpawnRandomAsteroid()
    {

            clickableAsteroidsGO[Random.Range(0, clickableAsteroidsGO.Length)].SetActive(true);
            asteroidsSpawned++;

    }

    public void OnClick_MineAsteroid()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);

        int materialToGain;
        materialToGain = Random.Range(0, 10);
        
        if(materialToGain > 5 || materialToGain == 5)
        {
            DroneManager.instance.satoniumAmount += Random.Range(materialGainAmountMin,materialGainAmountMax);
        }
        else if(materialToGain < 5)
        {
            DroneManager.instance.fueliumAmount += Random.Range(materialGainAmountMin, materialGainAmountMax);
        }

        asteroidsSpawned = 0;
    }
}
