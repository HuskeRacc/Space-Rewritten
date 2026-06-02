using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{
    [SerializeField] Transform asteroidPrefab;
    [SerializeField] int outerFieldRadius = 1000;
    [SerializeField] int asteroidDensityInCount = 500;
    [SerializeField] GameObject asteroidsFolder;

    private void Start()
    {
        for (int i = 0; i < asteroidDensityInCount; i++)
        {
            Vector3 unitSphere = Random.insideUnitSphere;
            Transform position = Instantiate(asteroidPrefab, unitSphere * outerFieldRadius + Vector3.zero, Random.rotation);
            position.parent = asteroidsFolder.transform;
            position.localScale *= Random.Range(.5f,2.5f);
        }
    }
}
