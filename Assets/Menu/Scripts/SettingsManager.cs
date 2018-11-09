using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class SettingsManager : MonoBehaviour { 

    public Toggle fullscreenToggle; // initialise UI elements

    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public Button applyVideoButton;
    public Button applyAudioButton;

    public Resolution[] resolutions;

    public GameSettings gameQualitySettings;

    public AudioMixer masterMixer;

    public float db;

    void OnEnable()
    {
        gameQualitySettings = new GameSettings();
    
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        masterVolumeSlider.onValueChanged.AddListener(delegate { onMasterVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { onMusicVolumeChange(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { onSFXVolumeChange(); });
        applyVideoButton.onClick.AddListener(delegate { onVideoButtonClick(); });
        applyAudioButton.onClick.AddListener(delegate { onAudioButtonClick(); });

        resolutions = Screen.resolutions; // getting an array of all avaliable resolutions

        foreach (Resolution resolution in resolutions)
            {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    // when the ui is interacted with, change the values of the variables the ui elements represent.

    public void LoadSettings()
    {
        gameQualitySettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        fullscreenToggle.isOn = gameQualitySettings.fullscreen;
        textureQualityDropdown.value = gameQualitySettings.textureQuality;
        antialiasingDropdown.value = gameQualitySettings.antialiasing;
        vSyncDropdown.value = gameQualitySettings.vSync;
        resolutionDropdown.value = gameQualitySettings.resolutionIndex;
        masterVolumeSlider.value = gameQualitySettings.masterVolume;
        musicVolumeSlider.value = gameQualitySettings.musicVolume;
        sfxVolumeSlider.value = gameQualitySettings.sfxVolume;


        Screen.fullScreen = gameQualitySettings.fullscreen;
        resolutionDropdown.RefreshShownValue();

    }


    public void OnFullscreenToggle()
    {
        gameQualitySettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;

    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameQualitySettings.resolutionIndex = resolutionDropdown.value;
  
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameQualitySettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
        gameQualitySettings.antialiasing = antialiasingDropdown.value;
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameQualitySettings.vSync = vSyncDropdown.value;
    }

    public void onMasterVolumeChange()
    {
        gameQualitySettings.masterVolume = masterVolumeSlider.value;
        db = 20f * Mathf.Log10(gameQualitySettings.masterVolume);
        masterMixer.SetFloat("masterVolume", db);
    }

    public void onMusicVolumeChange()
    {
        gameQualitySettings.musicVolume = musicVolumeSlider.value;
        db = 20f * Mathf.Log10(gameQualitySettings.musicVolume);
        masterMixer.SetFloat("musicVolume", db);
    }

    public void onSFXVolumeChange()
    {
        gameQualitySettings.sfxVolume = sfxVolumeSlider.value;
        db = 20f * Mathf.Log10(gameQualitySettings.sfxVolume);
        masterMixer.SetFloat("sfxVolume", db);

    }

    public void onAudioButtonClick() //when the apply button is clicked, save the audio settings
    {
        SaveSettings();
    }

    public void onVideoButtonClick() //when the apply button is clicked, save the video settings
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string gameDataJson = JsonUtility.ToJson(gameQualitySettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", gameDataJson);
        Debug.Log("Saving as JSON: " + gameDataJson);
    }

    


}

