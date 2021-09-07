using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Volume Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    private bool isPaused = false;
    public void LoadScene(string _scene) => SceneManager.LoadScene(_scene);
    
    
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefs();
    }

    
    
    /// <summary>
    /// Loads all the saved values for settings from playerprefs.
    /// </summary>
    public void LoadPlayerPrefs()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            musicVolumeSlider.value = volume;
            VolumeSlider(volume);
        }
        else
        {
            VolumeSlider(0.5f);
        }

        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfxVolumeSlider.value = sfxVolume;
            SFXSlider(sfxVolume);
        }
        else
        {
            SFXSlider(0.5f);
        }
    }
    
     
#region Volume Sliders
    /// <summary>
    /// Function for the Master Volume slider. Saves value to PlayerPrefs.
    /// </summary>
    /// <param name="_volume">Float passed in from the slider</param>
    public void VolumeSlider(float _volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", _volume);
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("MusicGroup", _volume);        
    }

    /// <summary>
    /// Function for the SFX Volume slider. Saves value to PlayerPrefs.
    /// </summary>
    /// <param name="_volume">Float passed in from the slider</param>
    public void SFXSlider(float _volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", _volume);
        _volume = VolumeRemap(_volume);
        audioMixer.SetFloat("SFXGroup", _volume);
    }

    /// <summary>
    /// Remaps the passed in float from the slider(value 0-1) and outputs to audio scale
    /// </summary>
    /// <param name="value">Passed in slider value</param>
    /// <returns>Remapped float value to pass into the audio mixer</returns>
    public float VolumeRemap(float value)
    {
        return -40 + (value - 0) * (20 - -40) / (1 - 0);
    }
#endregion
    
    /// <summary>
    /// Handles Pause functionality for the UI button.
    /// </summary>
    public void Pause()
    {
        GameManager.theManager.canResume = true;
        GameManager.theManager.PopUpPanel("Pause");
    }

    /// <summary>
    /// Handles the resume functionality for the resume button.
    /// </summary>
    public void Resume()
    {
        GameManager.theManager.PopUp.SetActive(false);
        GameManager.theManager.canResume = false;
        CharacterMotor.paused = false;
    }
    
    /// <summary>
    /// Quits from both the Play Mode in the Unity Editor and the Built Application.
    /// </summary>
    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }
    
    
}
