using UnityEngine;

public class ErrorNotificationSystem : MonoBehaviour
{
    public static ErrorNotificationSystem instance;

    [Header("Notification Upgrades")]
    public bool oxygenUpgradeBought = false;
    public bool generatorUpgradeBought = false;
    public bool solarUpgradeBought = false;
    public bool lightUpgradeBought = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}