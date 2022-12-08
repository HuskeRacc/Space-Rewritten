using UnityEngine;

public class ShipLights : MonoBehaviour
{
    [SerializeField] PowerGenerator power;

    [SerializeField] GameObject[] lights;

    private void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Lights");
    }

    private void Update()
    {
        ToggleLights();
    }

    void ToggleLights()
    {
        if(power.powerGeneratorActive == false)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
        }
    }
}
