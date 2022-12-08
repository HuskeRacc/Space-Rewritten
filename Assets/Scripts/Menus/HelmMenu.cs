using TMPro;
using UnityEngine;

public class HelmMenu : MonoBehaviour
{

    [SerializeField] PlayerMovement player;
    [SerializeField] DroneManager droneManager;

    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI heldCurrencyText;
    [SerializeField] TextMeshProUGUI batteryTXT;

    public void OnClick_SendDrone()
    {
        if (droneManager.status == 0)
        {
            droneManager.Sending();
        }

        if (droneManager.status == 2)
        {
            droneManager.Returning();
        }
    }

    public void OnClick_Back()
    {
        this.gameObject.SetActive(false);
        player.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        statusText.text = droneManager.droneStatus;
        buttonText.text = droneManager.buttonStatus;
        heldCurrencyText.text = "Material Worth: $" + droneManager.droneHeldCurrency.ToString("F1");
        batteryTXT.text = "Battery Power: " + droneManager.battery.ToString("F0") + "%";
    }
}
