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
    [SerializeField] private DroneDamageManager droneDamageManager;

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

        if(droneDamageManager == null)
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
            $"PWR: {powerStatus}" + 
            $"OXY: {oxygenStatus}\n" +
            $"O2: {shipSystems.shipOxygen:0}%\n" +  
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
            $"EFF: {shipSystems.solarEfficiency:0}%  BAT: {shipSystems.shipBattery:0}%\n" +
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



    private string GetWorstDamagedPartText()
    {
        if (droneDamageManager.jawsDamaged)
            return "JAWS DAMAGED";

        if (droneDamageManager.cargoDamaged)
            return "CARGO DAMAGED";

        if (droneDamageManager.chassisDamaged)
            return "CHASSIS DAMAGED";

        if (droneDamageManager.thrustersDamaged)
            return "THRUSTERS DAMAGED";

        return "NO FAULTS";
    }
}
