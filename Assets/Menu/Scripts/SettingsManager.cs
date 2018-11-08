using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour { 

    public Toggle fullscreenToggle; // initialise UI elements
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button applyVideoButton;
    public Button applyAudioButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameQualitySettings;

    void OnEnable()
    {
        gameQualitySettings = new GameSettings();




        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { onMusicVolumeChange(); });
        applyVideoButton.onClick.AddListener(delegate { onVideoButtonClick(); });
        applyAudioButton.onClick.AddListener(delegate { onAudioButtonClick(); });



        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        resolutions = Screen.resolutions; // getting an array of all avaliable resolutions
        foreach(Resolution resolution in resolutions)
            {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        if (File.Exists(Application.persistentDataPath + "/qualitysettings.json") == true) // only load the file if it exists.
        {

            LoadSettings();
        }
        else
        {
            resolutionDropdown.RefreshShownValue();
        }

    }

    // when the ui is interacted with, change the values of the variables the ui elements represent.

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
    public void onMusicVolumeChange() 
    {
        musicSource.volume = gameQualitySettings.musicVolume = musicVolumeSlider.value;
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
        File.WriteAllText(Application.persistentDataPath + "/qualitysettings.json", gameDataJson);
        Debug.Log("Saving as JSON: " + gameDataJson);
    }
    public void LoadSettings() 
    {
        File.ReadAllText(Application.persistentDataPath + "/qualitysettings.json");
        gameQualitySettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/qualitysettings.json"));

 
        fullscreenToggle.isOn = gameQualitySettings.fullscreen;
        textureQualityDropdown.value = gameQualitySettings.textureQuality;
        antialiasingDropdown.value = gameQualitySettings.antialiasing;
        vSyncDropdown.value = gameQualitySettings.vSync;
        resolutionDropdown.value = gameQualitySettings.resolutionIndex;
        musicVolumeSlider.value = gameQualitySettings.musicVolume;

        Screen.fullScreen = gameQualitySettings.fullscreen;
        resolutionDropdown.RefreshShownValue();


    }
}

