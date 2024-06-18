using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUIControl : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "MainMenu";
    public GameObject pauseMenuUI;
    bool pauseMenuActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        Time.timeScale = pauseMenuActive ? 0 : 1;
        pauseMenuActive = !pauseMenuActive;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(pauseMenuActive);
        }
        else
        {
            Debug.Log("no pause menu UI assigned");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

}
