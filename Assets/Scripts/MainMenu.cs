using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsGUI;
    public GameObject titleGUI;

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

    public void Return()
    {
        titleGUI.SetActive(true);
        settingsGUI.SetActive(false);
    }
}