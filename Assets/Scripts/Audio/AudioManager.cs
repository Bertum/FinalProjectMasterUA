using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioMixer audioMixer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioMixer = Camera.main.GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
    }

    private void Start()
    {
        SetSavedVolume();
    }

    public void MusicVolumeChanged(Slider slider)
    {
        audioMixer.SetFloat(Constants.MUSICVOLUME, slider.value);
        PlayerPrefs.SetFloat(Constants.MUSICVOLUME, slider.value);
    }

    public void EffectVolumeChanged(Slider slider)
    {
        audioMixer.SetFloat(Constants.EFFECTSVOLUME, slider.value);
        PlayerPrefs.SetFloat(Constants.EFFECTSVOLUME, slider.value);
    }

    private void SetSavedVolume()
    {
        if (PlayerPrefs.HasKey(Constants.MUSICVOLUME))
        {
            audioMixer.SetFloat(Constants.MUSICVOLUME, PlayerPrefs.GetFloat(Constants.MUSICVOLUME));
        }

        if (PlayerPrefs.HasKey(Constants.EFFECTSVOLUME))
        {
            audioMixer.SetFloat(Constants.EFFECTSVOLUME, PlayerPrefs.GetFloat(Constants.EFFECTSVOLUME));
        }
    }
}
