using UnityEngine;
using TMPro;

public class TerminalScreen : Interactable
{
    private enum TerminalPreviewState
    {
        Normal,
        Warning,
        Critical,
        Offline
    }

    [Header("Terminal")]
    [SerializeField] private TerminalType terminalType;

    [Header("Preview")]
    [SerializeField] private TextMeshProUGUI previewText;
    [SerializeField] private float previewUpdateRate = 0.5f;

    [Header("Preview Colours")]
    [SerializeField] private Color normalColour = Color.green;
    [SerializeField] private Color warningColour = Color.yellow;
    [SerializeField] private Color criticalColour = Color.red;
    [SerializeField] private Color offlineColour = Color.gray;

    [Header("Live Data References")]
    [SerializeField] private ShipSystems shipSystems;
    [SerializeField] private PowerGenerator powerGenerator;
    [SerializeField] private OxygenGenerator oxygenGenerator;
    [SerializeField] private ShipMaterialBank materialBank;
    [SerializeField] private DroneDamageManager droneDamageManager;

    private float nextPreviewUpdateTime;

    private void Start()
    {
        if (shipSystems == null)
            shipSystems = ShipSystems.instance;

        if (materialBank == null)
            materialBank = ShipMaterialBank.instance;

        if (powerGenerator == null)
            powerGenerator = FindAnyObjectByType<PowerGenerator>();

        if (oxygenGenerator == null)
            oxygenGenerator = FindAnyObjectByType<OxygenGenerator>();

        if (droneDamageManager == null)
            droneDamageManager = FindAnyObjectByType<DroneDamageManager>();

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
        if (previewText == null)
            return;

        previewText.text = GetPreviewText();
        previewText.color = GetColourForState(GetPreviewState());
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

    private TerminalPreviewState GetPreviewState()
    {
        switch (terminalType)
        {
            case TerminalType.Helm:
                return GetHelmPreviewState();

            case TerminalType.Shop:
                return GetShopPreviewState();

            case TerminalType.Systems:
                return GetSystemsPreviewState();

            case TerminalType.Repair:
                return GetRepairPreviewState();

            case TerminalType.Solar:
                return GetSolarPreviewState();

            default:
                return TerminalPreviewState.Offline;
        }
    }

    private string GetHelmPreview()
    {
        float thrustium = materialBank != null ? materialBank.thrustiumBanked : 0f;

        return
            "HELM CONTROL\n" +
            $"THRUSTIUM: {thrustium:0}\n" +
            "DESTINATION: LOCAL STATION\n" +
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
            "MATERIAL BANK:\n" +
            $"SA: {materialBank.satoniumBanked:0}\n" +
            $"FU: {materialBank.fueliumBanked:0}\n" +
            $"TH: {materialBank.thrustiumBanked:0}\n" +
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
            $"PWR: {powerStatus}  OXY: {oxygenStatus}\n" +
            $"O2: {shipSystems.shipOxygen:0}%\n" +
            $"BAT: { shipSystems.shipBattery:0}%\n" +
            $"FUEL: {shipSystems.fuel:0}%\n" +
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
            $"EFFICIENCY: {shipSystems.solarEfficiency:0}%\n" +
            "[E] OPEN";
    }

    private string GetRepairPreview()
    {
        if (droneDamageManager == null)
        {
            return
                "REPAIR BAY\n" +
                "DRONE STATUS UNKNOWN\n" +
                "[E] OPEN";
        }

        return
            "REPAIR BAY\n" +
            $"JAWS: {PartState(droneDamageManager.jawsDamaged)}  CARGO: {PartState(droneDamageManager.cargoDamaged)}\n" +
            $"CHAS: {PartState(droneDamageManager.chassisDamaged)}  THR: {PartState(droneDamageManager.thrustersDamaged)}\n" +
            "[E] OPEN";
    }

    private string PartState(bool damaged)
    {
        return damaged ? "DMG" : "OK";
    }

    private TerminalPreviewState GetHelmPreviewState()
    {
        if (materialBank == null)
            return TerminalPreviewState.Offline;

        if (materialBank.thrustiumBanked <= 0)
            return TerminalPreviewState.Warning;

        return TerminalPreviewState.Normal;
    }

    private TerminalPreviewState GetShopPreviewState()
    {
        if (materialBank == null)
            return TerminalPreviewState.Offline;

        return TerminalPreviewState.Normal;
    }

    private TerminalPreviewState GetSystemsPreviewState()
    {
        if (shipSystems == null)
            return TerminalPreviewState.Offline;

        bool powerOff = powerGenerator == null || !powerGenerator.powerGeneratorActive;
        bool oxygenOff = oxygenGenerator == null || !oxygenGenerator.o2GeneratorActive;

        if (powerOff || oxygenOff || shipSystems.shipOxygen <= 20f || shipSystems.fuel <= 10f)
            return TerminalPreviewState.Critical;

        if (shipSystems.shipOxygen <= 50f || shipSystems.fuel <= 30f)
            return TerminalPreviewState.Warning;

        return TerminalPreviewState.Normal;
    }

    private TerminalPreviewState GetSolarPreviewState()
    {
        if (shipSystems == null)
            return TerminalPreviewState.Offline;

        if (!shipSystems.solarsActive)
            return TerminalPreviewState.Critical;

        if (shipSystems.shipBattery <= 30f)
            return TerminalPreviewState.Warning;

        return TerminalPreviewState.Normal;
    }

    private TerminalPreviewState GetRepairPreviewState()
    {
        if (droneDamageManager == null)
            return TerminalPreviewState.Offline;

        int damagedParts = 0;

        if (droneDamageManager.jawsDamaged) damagedParts++;
        if (droneDamageManager.cargoDamaged) damagedParts++;
        if (droneDamageManager.chassisDamaged) damagedParts++;
        if (droneDamageManager.thrustersDamaged) damagedParts++;

        if (damagedParts >= 3)
            return TerminalPreviewState.Critical;

        if (damagedParts > 0)
            return TerminalPreviewState.Warning;

        return TerminalPreviewState.Normal;
    }

    private Color GetColourForState(TerminalPreviewState state)
    {
        switch (state)
        {
            case TerminalPreviewState.Normal:
                return normalColour;

            case TerminalPreviewState.Warning:
                return warningColour;

            case TerminalPreviewState.Critical:
                return criticalColour;

            case TerminalPreviewState.Offline:
                return offlineColour;

            default:
                return normalColour;
        }
    }
}