using UnityEngine;
using TMPro;
public class TerminalScreen : Interactable
{
    [Header("Terminal")]
    [SerializeField] private TerminalType terminalType;

    [Header("Preview")]
    [SerializeField] TextMeshProUGUI previewText;
    [SerializeField] float previewUpdateRate = 0.5f;

    [Header("Live Data References")]
    [SerializeField] private ShipSystems shipSystems;
    [SerializeField] private PowerGenerator powerGenerator;
    [SerializeField] private OxygenGenerator oxygenGenerator;
    [SerializeField] private ShipMaterialBank materialBank;

    float nextPreviewUpdateTime;

    private void Start()
    {
        if (shipSystems == null)
            shipSystems = ShipSystems.instance;

        if (materialBank == null)
            materialBank = ShipMaterialBank.instance;

        if(powerGenerator == null)
            powerGenerator = FindAnyObjectByType<PowerGenerator>();

        if(oxygenGenerator == null)
            oxygenGenerator = FindAnyObjectByType<OxygenGenerator>();

        UpdatePreview();
    }

    private void Update()
    {
        if (Time.time >= nextPreviewUpdateTime)
        {
            UpdatePreview();
            nextPreviewUpdateTime = Time.time + previewUpdateRate;
        }
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        TerminalUIManager.instance.OpenTerminal(terminalType);
    }

    public override void OnLoseFocus()
    {
    }

    private void UpdatePreview()
    {
        if(previewText == null)
        {
            return;
        }
        previewText.text = GetPreviewText();
    }

    private string GetPreviewText()
    {
        switch (terminalType)
        {
            case TerminalType.Helm:
                return GetHelmPreview();

            case TerminalType.Shop:
                return GetShopPreview();

            case TerminalType.Systems:
                return GetSystemsPreview();

            case TerminalType.Repair:
                return GetRepairPreview();

            case TerminalType.Solar:
                return GetSolarPreview();

            default:
                return "UNKNOWN TERMINAL";
        }
    }

    private string GetHelmPreview()
    {
        float thrustium = materialBank != null ? materialBank.thrustiumBanked : 0f;

        return
            "HELM CONTROL\n" +
            $"THRUSTIUM: {thrustium:0}\n" +
            "DEST: STATION\n" +
            "[E] OPEN";
    }

    private string GetShopPreview()
    {
        if (materialBank == null)
        {
            return
                "SHOP\n" +
                "MATERIAL BANK OFFLINE\n" +
                "[E] OPEN";
        }

        return
            "SHOP / FABRICATOR\n" +
            $"SA: {materialBank.satoniumBanked:0}\n" +
            $"FU: {materialBank.fueliumBanked:0}  TH: {materialBank.thrustiumBanked:0}\n" +
            "[E] OPEN";
    }

    private string GetDronePreview()
    {
        return
            "DRONE CONTROL\n" +
            "MINING STATUS\n" +
            "OPEN FOR DETAILS\n" +
            "[E] OPEN";
    }

    private string GetSystemsPreview()
    {
        if (shipSystems == null)
        {
            return
                "SHIP SYSTEMS\n" +
                "STATUS UNKNOWN\n" +
                "[E] OPEN";
        }

        string powerStatus = powerGenerator != null && powerGenerator.powerGeneratorActive
            ? "ON"
            : "OFF";

        string oxygenStatus = oxygenGenerator != null && oxygenGenerator.o2GeneratorActive
            ? "ON"
            : "OFF";

        return
            "SHIP SYSTEMS\n" +
            $"PWR: {powerStatus}  O2 GEN: {oxygenStatus}\n" +
            $"O2: {shipSystems.shipOxygen:0}%  FUEL: {shipSystems.fuel:0}%\n" +
            "[E] OPEN";
    }

    private string GetRepairPreview()
    {
        string powerStatus = powerGenerator != null && powerGenerator.powerGeneratorActive
            ? "OK"
            : "NO POWER";

        string oxygenStatus = oxygenGenerator != null && oxygenGenerator.o2GeneratorActive
            ? "OK"
            : "O2 OFF";

        return
            "REPAIR BAY\n" +
            $"POWER: {powerStatus}\n" +
            $"OXYGEN: {oxygenStatus}\n" +
            "[E] OPEN";
    }

    private string GetSolarPreview()
    {
        if (shipSystems == null)
        {
            return
                "SOLAR ARRAY\n" +
                "STATUS UNKNOWN\n" +
                "[E] OPEN";
        }

        string solarStatus = shipSystems.solarsActive
            ? "ONLINE"
            : "OFFLINE";

        return
            "SOLAR ARRAY\n" +
            $"STATUS: {solarStatus}\n" +
            $"EFF: {shipSystems.solarEfficiency:0}%  BAT: {shipSystems.shipBattery:0}%\n" +
            "[E] OPEN";
    }
}
