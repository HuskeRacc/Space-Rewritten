using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DroneRepairsMenu : MonoBehaviour
{
    [SerializeField] GameObject droneRepairsMenu;

    [Header("Audio")]
    [SerializeField] AudioSource repairAudioSource;
    [SerializeField] AudioClip repairSound;

    [Header("Error Graphics")]
    [SerializeField] GameObject droneJawsErrorGraphics;
    [SerializeField] GameObject droneCargoErrorGraphics;
    [SerializeField] GameObject droneChassisErrorGraphics;
    [SerializeField] GameObject droneThrusterErrorGraphics;

    [Header("Repair Prices Text")]
    [SerializeField] TextMeshProUGUI jawsPrice;
    [SerializeField] TextMeshProUGUI cargoPrice;
    [SerializeField] TextMeshProUGUI chassisPrice;
    [SerializeField] TextMeshProUGUI thrusterPrice;

    [Header("Banked Materials")]
    [SerializeField] TextMeshProUGUI bankedSatonium;
    [SerializeField] TextMeshProUGUI bankedFuelium;
    [SerializeField] TextMeshProUGUI bankedThrustium;

    [Header("Buttons")]
    [SerializeField] Button jawsButton;
    [SerializeField] Button cargoButton;
    [SerializeField] Button chassisButton;
    [SerializeField] Button thrusterButton;

    [Header("UI Elements")]
    [SerializeField] GameObject droneVisuals;
    [SerializeField] GameObject droneDeployedErrorTXT;

    public void OnClick_BackButton()
    {
        droneRepairsMenu.SetActive(false);
        PlayerMovement.instance.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateVisuals), 0, 5);
    }

    void UpdateVisuals()
    {
        #region Deployed Error
        if (DroneManager.instance.status != 0)
        {
            droneVisuals.SetActive(false);
            droneDeployedErrorTXT.SetActive(true);
        }
        else
        {
            droneVisuals.SetActive(true);
            droneDeployedErrorTXT.SetActive(false);
        }
        #endregion

        #region Damage Indicators
        if (DroneDamageManager.instance.jawsDamaged)
        {
            droneJawsErrorGraphics.SetActive(true);
            jawsButton.interactable = true;
        }
        else
        {
            droneJawsErrorGraphics.SetActive(false);
            jawsButton.interactable = false;
        }

        if (DroneDamageManager.instance.cargoDamaged)
        {
            droneCargoErrorGraphics.SetActive(true);
            cargoButton.interactable = true;
        }
        else
        {
            droneCargoErrorGraphics.SetActive(false);
            cargoButton.interactable = false;
        }

        if (DroneDamageManager.instance.chassisDamaged)
        {
            droneChassisErrorGraphics.SetActive(true);
            chassisButton.interactable = true;
        }
        else
        {
            droneChassisErrorGraphics.SetActive(false);
            chassisButton.interactable = false;
        }

        if (DroneDamageManager.instance.thrustersDamaged)
        {
            droneThrusterErrorGraphics.SetActive(true);
            thrusterButton.interactable = true;
        }
        else
        {
            droneThrusterErrorGraphics.SetActive(false);
            thrusterButton.interactable = false;
        }
        #endregion

        #region Prices
        jawsPrice.text = "$Satonium " + ShopPrices.instance.repairPrices[0];
        cargoPrice.text = "$Satonium " + ShopPrices.instance.repairPrices[1];
        chassisPrice.text = "$Satonium " + ShopPrices.instance.repairPrices[2];
        thrusterPrice.text = "$Fuelium " + ShopPrices.instance.repairPrices[3];
        #endregion

        #region Banked Values
        bankedSatonium.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");
        bankedFuelium.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
        bankedThrustium.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");
        #endregion
    }

    public void OnClick_RepairJaws()
    {
        if(ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.repairPrices[0])
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.repairPrices[0];
            Debug.Log("Repaired Jaws");
            DroneDamageManager.instance.jawsDamaged = false;

            DroneDamageManager.instance.ResetMaterialGain();

            repairAudioSource.PlayOneShot(repairSound);

            UpdateVisuals();
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Satonium!", 5, false));
        }
    }

    public void OnClick_RepairCargo()
    {
        if (ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.repairPrices[1])
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.repairPrices[1];
            Debug.Log("Repaired Cargo");
            DroneDamageManager.instance.cargoDamaged = false;

            DroneDamageManager.instance.ResetMaterialGain();

            repairAudioSource.PlayOneShot(repairSound);

            UpdateVisuals();
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Satonium!", 5, false));
        }
    }

    public void OnClick_RepairChassis()
    {
        if (ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.repairPrices[2])
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.repairPrices[2];
            Debug.Log("Repaired Chassis");
            DroneDamageManager.instance.chassisDamaged = false;

            DroneDamageManager.instance.ResetTravelTime();

            repairAudioSource.PlayOneShot(repairSound);

            UpdateVisuals();
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Satonium!", 5, false));
        }
    }

    public void OnClick_RepairThruster()
    {
        if (ShipMaterialBank.instance.fueliumBanked >= ShopPrices.instance.repairPrices[3])
        {
            ShipMaterialBank.instance.fueliumBanked -= ShopPrices.instance.repairPrices[3];
            Debug.Log("Repaired Thruster");
            DroneDamageManager.instance.thrustersDamaged = false;

            DroneDamageManager.instance.ResetTravelTime();

            repairAudioSource.PlayOneShot(repairSound);

            UpdateVisuals();
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not Enough Fuelium!", 5, false));
        }
    }
}
