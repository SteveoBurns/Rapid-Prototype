using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private bool isPaused = false;
    public void LoadScene(string _scene) => SceneManager.LoadScene(_scene);
    
    
    public void Pause()
    {
        if(!isPaused)
        {
            GameManager.theManager.PopUpPanel("Pause");
            isPaused = true;
        }
        else if(isPaused)
        {
            GameManager.theManager.PopUp.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
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
