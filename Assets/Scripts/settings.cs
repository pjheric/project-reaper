using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    [Header("Sound")]
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sFXVolumeSlider;

    [Header("Screen")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle windowedToggle;

    private void Start()
    {
        GetSettings();
    }

    public void ToggleSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void SetVolume()
    {
        float masterFloat = -80;
        float musicFloat = -80;
        float sFXFloat = -80;

        if (masterVolumeSlider.value != 0)
        {
            masterFloat = Mathf.Log10(masterVolumeSlider.value) * 20;
        }
        if (musicVolumeSlider.value != 0)
        {
            musicFloat = Mathf.Log10(musicVolumeSlider.value) * 20;
        }
        if (sFXVolumeSlider.value != 0)
        {
            sFXFloat = Mathf.Log10(sFXVolumeSlider.value) * 20;
        }

        mixer.SetFloat("masterVolume", masterFloat);
        mixer.SetFloat("musicVolume", musicFloat);
        mixer.SetFloat("sFXVolume", sFXFloat);
    }

    public void SetResolution()
    {
        if (resolutionDropdown.value == 0)
        {
            Screen.SetResolution(3840, 2160, Screen.fullScreen);
        }
        else if (resolutionDropdown.value == 1)
        {
            Screen.SetResolution(2560, 1440, Screen.fullScreen);
        }
        else if (resolutionDropdown.value == 2)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        else if (resolutionDropdown.value == 3)
        {
            Screen.SetResolution(1600, 900, Screen.fullScreen);
        }
        else if (resolutionDropdown.value == 4)
        {
            Screen.SetResolution(1366, 768, Screen.fullScreen);
        }
        else if (resolutionDropdown.value == 5)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
    }

    public void SetWindowed()
    {
        if (windowedToggle.isOn)
        {
            Screen.fullScreen = false;
        }
        else if (!windowedToggle.isOn)
        {
            Screen.fullScreen = true;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Master Volume Slider", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume Slider", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFX Volume Slider", sFXVolumeSlider.value);

        PlayerPrefs.SetInt("Dropdown Selection", resolutionDropdown.value);
        if (windowedToggle.isOn)
        {
            PlayerPrefs.SetInt("Windowed Toggle", 1);
        }
        else if (!windowedToggle.isOn)
        {
            PlayerPrefs.SetInt("Windowed Toggle", 0);
        }

        PlayerPrefs.Save();
    }

    private void GetSettings()
    {
        if (PlayerPrefs.HasKey("Master Volume Slider"))
        {
            Debug.Log("Master volume value found: " + PlayerPrefs.GetFloat("Master Volume Slider"));
        }
        if (PlayerPrefs.HasKey("Music Volume Slider"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume Slider");
        }
        if (PlayerPrefs.HasKey("SFX Volume Slider"))
        {
            sFXVolumeSlider.value = PlayerPrefs.GetFloat("SFX Volume Slider");
        }
        SetVolume();

        resolutionDropdown.value = PlayerPrefs.GetInt("Dropdown Selection");
        SetResolution();

        if (PlayerPrefs.GetInt("Windowed Toggle") == 1)
        {
            windowedToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("Windowed Toggle") == 0)
        {
            windowedToggle.isOn = false;
        }
        SetWindowed();
    }
}
