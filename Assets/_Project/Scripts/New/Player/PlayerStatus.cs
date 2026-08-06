using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    [SerializeField] PlayerNeeds player;
    [SerializeField] ShipSystems ship;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] float popupCooldown = 1f;
    [SerializeField] bool hasDisplayedRecently = false;

    [SerializeField] string hardToBreatheText = "It's getting harder to breathe.";
    [SerializeField] string cantBreatheText = "I can't breathe!";

    [SerializeField] float lowShipOxygenThreshold = 20f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        statusText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!hasDisplayedRecently)
        {
            UpdateStatus();
        }
    }

    void UpdateStatus()
    {
        if (ship.shipOxygen < lowShipOxygenThreshold)
        {
            // The '5' here is now safely ignored by the Coroutine below
            StartCoroutine(TextPopup(hardToBreatheText, true));
        }
        else if (player.oxygen <= 0)
        {
            // The '5' here is also safely ignored
            StartCoroutine(TextPopup(cantBreatheText, true));
        }
        else
        {
            statusText.gameObject.SetActive(false);
        }
    }

    // We leave "int timeDisplayed" here so your other scripts don't throw errors!
    public IEnumerator TextPopup(string text, bool cooldownRequired)
    {
        // 1. Instantly lock the loop
        hasDisplayedRecently = true;

        statusText.text = text;
        statusText.gameObject.SetActive(true);

        // 2. Use your hardcoded time instead of the timeDisplayed variable
        yield return new WaitForSeconds(2f);

        statusText.gameObject.SetActive(false);

        // 3. Process the cooldown
        if (cooldownRequired)
        {
            yield return new WaitForSeconds(popupCooldown);
        }

        // 4. Unlock the loop
        hasDisplayedRecently = false;
    }
}