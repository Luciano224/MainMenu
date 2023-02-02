using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue= null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown = null;

    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle = null;

    [Header("Sensitivity Setting")]
    [SerializeField] private TMP_Text ControllerSensitivityValue = null;
    [SerializeField] private Slider controllerSensivitySlider = null;

    [Header("Invert Y Setting")]
    [SerializeField] private Toggle invertToggleY = null;

    private void Awake()
    {
        if (canUse)
        {
            if (Players.HasKey("MasterVolume"))
            {
                float localVolume = PlayersPrefs.GetFloat("MasterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;

            }
            else
            {
                menuController.ResetButton("Audio");
            }

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
            
            if (PlayersPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPref.GetInt("masterFullScreen");

                if (localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;

                }

                if (PlayerPrefs.HasKey("masterBrightness"))
                {
                    float localBrightness = PlayersPrefs.GetFloat("masterBrightness");

                    brightnessTextValue.text = localBrightness.ToString("0.0");
                    brightnessSlider.value = localBrightness;
                    //change the brightness
 
                }

                if (PlayerPrefs.HasKey("masterSensitivy"))
                {
                    float localSensitivity = PlayerPrefs.GetFloat("masterSensitivity");

                    localSensitivityTextValue.text = localSensitivity.ToString("0 ");
                    controllerSensitivitySlider.value = localSensitivity;
                    menuController.mainControllerSensitivity = Mathf.RoundToInt(localSensitivity);
                }

                if (PlayerPrefs.HasKey("masterInvertY"))
                {
                    if (PlayerPrefs.GetInt("masterInvertY") == 1)
                    {
                        invertToggleY.isOn = true;
                    }
                    else
                    {
                        invertToggleY.isOn = false;
                    }
                }
            }
        }
    }
}
