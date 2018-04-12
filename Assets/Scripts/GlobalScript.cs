using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public delegate void TheSceneChanged(string prevScene);

    public static string Level;
    public static string PrevLevel;


    public static bool Active;

    public static PlayerController PlayerController;
    public static event TheSceneChanged SceneChanged;


    public static bool Paused = false;

    public static Krilk Krilk;
    public static Gnox Gnox;


    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        Krilk = PlayerController._krilk;
        Gnox = PlayerController._gnox;


        Level = SceneManager.GetActiveScene().name;
        if (Level == "MainMenu") //first scene in build index... should be zero
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            Active = false;
        }


        SceneManager.activeSceneChanged += SceneManagerActiveSceneChanged;

        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        SceneChanged?.Invoke(PrevLevel);
    }

    private void SceneManagerActiveSceneChanged(Scene current, Scene next)
    {
        PrevLevel = Level;
        Level = next.name;

        if (!Active && Level != "MainMenu")
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(true);

            Active = true;
        }
    }
}