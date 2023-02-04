using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerNeeds : MonoBehaviour
{
    public static PlayerNeeds instance;

    [Header("Oxygen")]
    [Range(0, 100)] public float oxygen = 100f;
    [SerializeField] float maxPlayerOxygen = 100f;
    [SerializeField] float shipOxygen;

    [SerializeField] float oxygenTickTime;

    [SerializeField] ShipSystems ship;

    [SerializeField] bool alreadyDamaged = false;

    [SerializeField] float suffocationDamage = 10f;
    [SerializeField] float damageCooldown = 5f;

    [Header("Hunger")]
    [Range(0, 100)] public float hunger = 100f;
    [SerializeField] float hungerTickTime = 3f;
    [SerializeField] float hungerDecreaseRate = 0.01f;

    [SerializeField] TextMeshProUGUI hungerValue;
    [SerializeField] TextMeshProUGUI fatigueValue;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(LowerOxygen());
        StartCoroutine(Breathe());
        StartCoroutine(DecreaseHunger());
    }

    private void Update()
    {
        shipOxygen = ship.shipOxygen;
        ClampOxygen();
        ClampHunger();
        NoOxygenCheck();
        UpdateUI();
    }

    #region hunger

    private void ClampHunger()
    {
        if (hunger >= 100)
            hunger = 100;

        if(hunger <= 0)
        {
            hunger = 0;
            //Die
        }
    }

    public void HungerIncrease(float hungerIncreaseValue)
    {
        Debug.Log("Food increased by " + hungerIncreaseValue);
        hunger += hungerIncreaseValue;
    }

    #endregion

    #region oxygen

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

    #endregion

    #region UI
    private void UpdateUI()
    {
        hungerValue.text = hunger.ToString("F2");
        fatigueValue.text = "TBI";
    }
    #endregion

    #region Hunger IENumerators
    IEnumerator DecreaseHunger()
    {
        hunger -= hungerDecreaseRate;
        yield return new WaitForSeconds(hungerTickTime);
        StartCoroutine(DecreaseHunger());
    }
    #endregion
}
