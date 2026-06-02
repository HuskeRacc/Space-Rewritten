using UnityEngine;

public class ImpactSoundContainer : MonoBehaviour
{
    public static ImpactSoundContainer instance;

    public AudioClip[] impactClipsMetal;

    public AudioClip[] impactClipsSoft;

    private void Awake()
    {
        instance = this;
    }


}
