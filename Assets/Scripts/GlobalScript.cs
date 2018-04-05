﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public delegate void TheSceneChanged(Scene prevScene);

    public static Scene Level;
    public static Scene PrevLevel;


    public static bool Active;
    public static event TheSceneChanged SceneChanged;

    public static PlayerController PlayerController;


    private void Awake()
    {
        PlayerController = GameObject.FindObjectOfType<PlayerController>();

        Level = SceneManager.GetActiveScene();
        if (Level.buildIndex == 0) //first scene in build index... should be zero
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            Active = false;
        }

        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        PrevLevel = Level;
        Level = arg0;
        SceneChanged?.Invoke(PrevLevel);

        if (!Active)
        {
            for (var i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(true);

            Active = true;
        }
    }
}