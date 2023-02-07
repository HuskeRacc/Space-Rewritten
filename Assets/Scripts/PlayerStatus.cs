using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerNeeds player;
    [SerializeField] ShipSystems ship;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] float popupCooldown = 1f;
    [SerializeField] bool hasDisplayedRecently = false;

    [SerializeField] string hardToBreatheText = "It's getting harder to breathe.";
    [SerializeField] string cantBreatheText = "I can't breathe!";

    [SerializeField] float lowShipOxygenThreshold = 20f;

    public static PlayerStatus instance;

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
        if(hasDisplayedRecently)
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if(ship.shipOxygen < lowShipOxygenThreshold)
        {
            StartCoroutine(TextPopup(hardToBreatheText, 5, true));
        } 
        else if(player.oxygen <= 0)
        {
            StartCoroutine(TextPopup(cantBreatheText, 5, true));
        }
        else 
        { 
            statusText.gameObject.SetActive(false); 
        }
    }

    public IEnumerator TextPopup(string text, int timeDisplayed, bool cooldownRequired)
    {
        statusText.text = text;
        statusText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeDisplayed);

        statusText.gameObject.SetActive(false);

        if(cooldownRequired)
        StartCoroutine(PopupCooldown());
    }

    IEnumerator PopupCooldown()
    {
        hasDisplayedRecently = true;
        yield return new WaitForSeconds(popupCooldown);
        hasDisplayedRecently = false;
    }
}
