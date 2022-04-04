using UnityEngine;
using UnityEngine.Audio;

public class MixerVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterLvl(float masterLvl)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(masterLvl) * 20);
    }

    public void SetMusicLvl(float musicLvl)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicLvl) * 20);
    }
    public void SetEffectsLvl(float effectsLvl)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(effectsLvl) * 20);
    }
}
