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

    [SerializeField] TextMeshProUGUI oxygenValueText;

    [Header("Hunger")]
    [Range(0, 100)] public float hunger = 100f;
    [SerializeField] float hungerTickTime = 3f;
    [SerializeField] float hungerDecreaseRate = 0.01f;
    [SerializeField] float savedHungerDecreaseRate;

    [SerializeField] TextMeshProUGUI hungerValue;


    [Header("Fatigue")]
    [Range(0, 100)] public float fatigue = 100f;
    [SerializeField] float fatigueTickTime = 5f;
    [SerializeField] float fatigueDecreaseRate = 0.01f;
    [SerializeField] float savedFatigueDecreaseRate;
    [SerializeField] float fatigueGainRate = 0.01f;
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
        StartCoroutine(DecreaseFatigue());
    }

    private void Update()
    {
        shipOxygen = ship.shipOxygen;
        ClampOxygen();
        ClampValues();
        IncreaseOxygen();
        NoOxygenCheck();
        UpdateUI();
    }

    private void ClampValues()
    {
        if (hunger >= 100)
            hunger = 100;

        if (hunger <= 0)
        {
            hunger = 0;
            //Die
        }

        if (fatigue >= 100)
            fatigue = 100;

        if (fatigue <= 0)
        {
            fatigue = 0;
            //Die
        }
    }

    #region hunger

    public void HungerIncrease(float hungerIncreaseValue)
    {
        Debug.Log("Food increased by " + hungerIncreaseValue);
        hunger += hungerIncreaseValue;
    }

    #endregion

    #region Fatigue

    public void InvokeSleep()
    {
        savedFatigueDecreaseRate = fatigueDecreaseRate;
        fatigueDecreaseRate = 0;
        PlayerMovement.instance.canMove = false;
        InvokeRepeating(nameof(Sleep), 0.1f, 0.1f);
    }

    public void InvokeSleepBreak()
    {
        fatigueDecreaseRate = savedFatigueDecreaseRate;
        PlayerMovement.instance.canMove = true;
        CancelInvoke(nameof(Sleep));
    }

    private void Sleep()
    {
        fatigue += fatigueGainRate;
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
        fatigueValue.text = fatigue.ToString("F2");
        oxygenValueText.text = oxygen.ToString("F0");

        if (oxygen < 50)
        {
            oxygenValueText.gameObject.SetActive(true);
        }
        else
        {
            oxygenValueText.gameObject.SetActive(false);
        }
    }
    #endregion

    #region IENumerators
    IEnumerator DecreaseHunger()
    {
        hunger -= hungerDecreaseRate;
        yield return new WaitForSeconds(hungerTickTime);
        StartCoroutine(DecreaseHunger());
    }

    IEnumerator DecreaseFatigue()
    {
        fatigue -= fatigueDecreaseRate;
        yield return new WaitForSeconds(fatigueTickTime);
        StartCoroutine(DecreaseFatigue());
    }
    #endregion
}
