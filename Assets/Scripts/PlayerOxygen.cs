using System.Collections;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    [Range(0, 100)] public float oxygen = 100f;
    [SerializeField] float maxPlayerOxygen = 100f;
    [SerializeField] float shipOxygen;

    [SerializeField] float oxygenTickTime;

    [SerializeField] ShipSystems ship;

    [SerializeField] bool alreadyDamaged = false;

    [SerializeField] float suffocationDamage = 10f;
    [SerializeField] float damageCooldown = 5f;

    private void Start()
    {
        StartCoroutine(LowerOxygen());
        StartCoroutine(Breathe());
    }

    private void Update()
    {
        shipOxygen = ship.shipOxygen;
        ClampOxygen();
        NoOxygenCheck();
    }

    void ClampOxygen()
    {
        if (oxygen >= 100)
        {
            oxygen = 100;
        }
    }

    void IncreaseOxygen()
    {
        if(ship.shipOxygen > 0)
        {
            oxygen = maxPlayerOxygen;
        }
    }

    void NoOxygenCheck()
    {
        if (!ship.noOxygen) return;

        if (!alreadyDamaged)
        {
            PlayerMovement.OnTakeDamage(suffocationDamage);

            alreadyDamaged = true;
            Invoke(nameof(ResetDamage), damageCooldown);
        }
    }

    void ResetDamage()
    {
        alreadyDamaged = false;
    }

    IEnumerator LowerOxygen()
    {
        if(ship.noOxygen)
        {
            oxygen--;
            yield return new WaitForSeconds(oxygenTickTime);
            StartCoroutine(LowerOxygen());
        }
        else
        {
            yield return new WaitForSeconds(oxygenTickTime);
            StartCoroutine(LowerOxygen());
        }
    }
    
    IEnumerator Breathe()
    {   if(ship.shipOxygen > 0)
        {
            ship.shipOxygen--;
            yield return new WaitForSeconds(oxygenTickTime);
            StartCoroutine(Breathe());
        }
    }
}
