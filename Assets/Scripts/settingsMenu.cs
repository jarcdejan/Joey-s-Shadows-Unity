using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu instance;

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void OnEnable()
    {
        Debug.Log("SettingsMenu Awake");

        if (volumeSlider != null )
        {
            // Only set the volume if the PlayerPrefs key is present
            if (PlayerPrefs.HasKey("SliderVolumeLevel"))
            {
                float savedVolume = PlayerPrefs.GetFloat("SliderVolumeLevel");
                SetVolume(savedVolume);
            }
        }
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Setting volume to " + volume);

        if (audioMixer != null && volumeSlider != null)
        {
            volumeSlider.value = volume;
            audioMixer.SetFloat("volume", volume*40 - 30);

            PlayerPrefs.SetFloat("SliderVolumeLevel", volume);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("audioMixer or volumeSlider is null. Check your references in the Inspector.");
        }
    }

    public void OnVolumeSliderChanged()
    {
        SetVolume(volumeSlider.value);
    }

}
