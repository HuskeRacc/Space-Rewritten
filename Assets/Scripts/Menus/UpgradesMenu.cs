using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;

    [SerializeField] TextMeshProUGUI[] priceTexts;

    [SerializeField] int[] amountBought;

    [SerializeField] int[] maxBuyable;

    [SerializeField] PowerGenerator powerGenerator;

    [SerializeField] TextMeshProUGUI satoniumBankedValue;
    [SerializeField] TextMeshProUGUI thrustiumBankedValue;
    [SerializeField] TextMeshProUGUI fueliumBankedValue;

    [SerializeField] Button[] upgradeButtons;    
    


    private void Update()
    {
        DisplayPrices();
        DisplayBankedValues();
        CheckClamps();
    }

    void CheckClamps()
    {
        if (amountBought[0] == maxBuyable[0])
        {
            upgradeButtons[0].interactable = false;
        }
        if (amountBought[1] == maxBuyable[1])
        {
            upgradeButtons[1].interactable = false;
        }
        if (amountBought[2] == maxBuyable[2])
        {
            upgradeButtons[2].interactable = false;
        }
        if (amountBought[3] == maxBuyable[3])
        {
            upgradeButtons[3].interactable = false;
        }
        if (amountBought[4] == maxBuyable[4])
        {
            upgradeButtons[4].interactable = false;
        }
        if (amountBought[5] == maxBuyable[5])
        {
            upgradeButtons[5].interactable = false;
        }
        if (amountBought[6] == maxBuyable[6])
        {
            upgradeButtons[6].interactable = false;
        }
        if (amountBought[7] == maxBuyable[7])
        {
            upgradeButtons[7].interactable = false;
        }
    }

    void DisplayPrices()
    {
        for (int i = 0; i < priceTexts.Length; i++)
        {
            priceTexts[i].text = "$Thrustium " + ShopPrices.instance.upgradePrices[i].ToString("F2");
        }
    }

    void DisplayBankedValues()
    {
        satoniumBankedValue.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");
        thrustiumBankedValue.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");
        fueliumBankedValue.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
    }

    public void OnClick_Upgrade1()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[1])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[1];
            DroneManager.instance.batteryDepletionRate -= 0.25f;
            amountBought[0]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade2()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[2])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[2];
            DroneManager.instance.maxBatteryCharge += 10;
            amountBought[1]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade3()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[3])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[3];
            DroneManager.instance.timeToTravel -= 1f;
            amountBought[2]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade4()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[4])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[4];
            DroneManager.instance.MaximumMaterialGainTime -= 1;
            amountBought[3]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade5()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[5])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[5];
            DroneManager.instance.maximumMineable += 2.5f;
            amountBought[4]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade6()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[6])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[6];
            ErrorNotificationSystem.instance.oxygenUpgradeBought = true;
            amountBought[5]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade7()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[7])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[7];
            ErrorNotificationSystem.instance.generatorUpgradeBought = true;
            amountBought[6]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_Upgrade8()
    {
        if (ShipMaterialBank.instance.thrustiumBanked >= ShopPrices.instance.upgradePrices[8])
        {
            ShipMaterialBank.instance.thrustiumBanked -= ShopPrices.instance.upgradePrices[8];
            powerGenerator.fuelConsumptionRate -= 0.25f;
            amountBought[7]++;
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Thrustium!", 2, false));
        }
    }

    public void OnClick_BackButton()
    {
        this.gameObject.SetActive(false);
        shopMenu.SetActive(true);
    }
}
