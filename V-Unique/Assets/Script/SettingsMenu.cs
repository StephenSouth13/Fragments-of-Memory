using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;


    void Start()
    {
    }
    void OnEnable()
    {
        // Lấy giá trị volume đang lưu trong máy để gán vào vị trí thanh trượt
        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);

        if (sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);
    }
    public void SetMusicVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(volume);
        }
    }
}
