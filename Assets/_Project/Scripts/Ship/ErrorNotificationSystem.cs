using UnityEngine;

public class ErrorNotificationSystem : MonoBehaviour
{
    public static ErrorNotificationSystem instance;

    [Header("Audio")]
    [SerializeField] private AudioSource errorSoundSource;
    [SerializeField] private AudioClip[] errorSounds;

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

    private void Start()
    {
        if (errorSoundSource == null)
        {
            errorSoundSource = GetComponent<AudioSource>();
        }

        if (errorSoundSource == null)
        {
            Debug.LogWarning("ErrorNotificationSystem is missing an AudioSource.");
        }
    }

    public void OxygenError()
    {
        if (oxygenUpgradeBought)
            PlayErrorSound(0);
    }

    public void GeneratorError()
    {
        if (generatorUpgradeBought)
            PlayErrorSound(1);
    }

    public void SolarError()
    {
        if (solarUpgradeBought)
            PlayErrorSound(2);
    }

    public void LightError()
    {
        if (lightUpgradeBought)
            PlayErrorSound(3);
    }

    private void PlayErrorSound(int index)
    {
        if (errorSoundSource == null)
        {
            Debug.LogWarning("Cannot play error sound. AudioSource is missing.");
            return;
        }

        if (errorSounds == null || index < 0 || index >= errorSounds.Length || errorSounds[index] == null)
        {
            Debug.LogWarning("Cannot play error sound. Missing AudioClip at index: " + index);
            return;
        }

        errorSoundSource.PlayOneShot(errorSounds[index]);
    }
}