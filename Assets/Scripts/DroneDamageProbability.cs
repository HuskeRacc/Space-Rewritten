using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDamageProbability : MonoBehaviour
{
    [SerializeField] List<float> cumulativeProbability;

    //creates cumulative list.
    void CumulativeProbability(List<float> probability)
    {
        float probabilitySum = 0;

        cumulativeProbability = new List<float>(); // resets list

        for (int i = 0; i < probability.Count; i++)
        {
            probabilitySum += probability[i]; //add to sum
            cumulativeProbability.Add(probabilitySum); // add new sum to list
        }

        if (probabilitySum > 100f)
            Debug.LogError("Probability exceeded 100%");
    }

    public int ProbabilityCheck(List<float> probability)
    {
        CumulativeProbability(probability);

        float rnd = Random.Range(1, 101);

        for (int i = 0; i < probability.Count; i++)
        {
            if(rnd <= cumulativeProbability[i])
            {
                return i;
            }
        }
        return -1; // return -1 if an error occurs
    }


}
