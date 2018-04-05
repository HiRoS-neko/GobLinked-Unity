using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start1PlayerGame()
    {
        GlobalScript.PlayerController.SetMode(PlayerController.GameMode.SinglePlayer);
        SceneManager.LoadScene(1);
    }

    public void Start2PlayerGame()
    {
        GlobalScript.PlayerController.SetMode(PlayerController.GameMode.MultiPlayer);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Settings()
    {
    }
}