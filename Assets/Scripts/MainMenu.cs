using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsGUI;
    public GameObject titleGUI;
    public GameObject cheats1;
    public GameObject cheats2;
    private bool cheat = false;
        

    private void Start()
    {
        GetComponent<waitTrigger>().ActivateWait("swing", 3.1f);
    }

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
        titleGUI.SetActive(false);
        settingsGUI.SetActive(true);
    }

    public void Cheats()
    {
        if (!cheat)
        {
            cheats1.SetActive(false);
            cheats2.SetActive(true);
            cheat = true;
        }
        else if (cheat)
        {
            cheats2.SetActive(false);
            cheats1.SetActive(true);
            cheat = false;
        }
    }

    public void Return()
    {
        titleGUI.SetActive(true);
        settingsGUI.SetActive(false);
    }
}