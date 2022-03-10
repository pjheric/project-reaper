using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using TMPro;

public class settings : MonoBehaviour
{

    [Header("Sound")]
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sFXVolumeSlider;


    [SerializeField] private GameObject StartMenuPanel; 
    [SerializeField] private GameObject StartMenuSettingsButton; 
    [SerializeField] private  EventSystem es; 


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


    public void OnPressBack()
    {
        this.gameObject.SetActive(false);
        StartMenuPanel.SetActive(true); 
        es.SetSelectedGameObject(StartMenuSettingsButton); 

    }
}
