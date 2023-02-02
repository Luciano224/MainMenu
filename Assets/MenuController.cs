using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;


    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text ControllerSensitivityValue = null;
    [SerializeField] private Slider controllerSensivitySlider = null;
    [SerializeField] private int defaultSensitivity = 4;
    public int mainControllerSensitivity = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertToggleY = null;

    [Header("Graphics Settigns")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaulBrightness = 1;

    [Space(3)]
    [SerializeField] private TMP_Dropdown qualityDropDown;
    [SerializeField] private Toggle FullScreenToggle;


    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;





    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);

    }

    public void LoadGameDialogYes() 
    {

        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel"); 
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit(); 
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (MenuType == "Gameplay")
        {
            ControllerSensitivityValue.text = defaultSensitivity.ToString("0");
            controllerSensivitySlider.value = defaultSensitivity;
            mainControllerSensitivity = defaultSensitivity;
            invertToggleY.isOn = false; 
            GameplayApply();
        }

        if (MenuType == "Graphics")
        {
            //Reset brightness Value
            brightnessSlider.value = defaulBrightness;
            brightnessTextValue.text = defaulBrightness.ToString("0.0");

            qualityDropDown.value = 1;
            QualitySettings.SetQualityLevel(1);

            FullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            GraphicsApply();    
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSensitivity = Mathf.RoundToInt(sensitivity);
        ControllerSensitivityValue.text = sensitivity.ToString("0");

    }

    public void GameplayApply()
    {
        if (invertToggleY.isOn) {
            PlayerPrefs.SetInt("masterInvertY", 1); //its currently a value of 1 so basically the boolean that when we set something true of false is a one or a zero so true = 1, false = 0 so we can read that another time

        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0); //same stuff here, 0 equals to false
        }

        PlayerPrefs.SetFloat("masterSensitivity", mainControllerSensitivity);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        //change your brightness with your post proccesing or whatever it is

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }

}
