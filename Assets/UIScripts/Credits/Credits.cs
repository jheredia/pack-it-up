using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "MainMenu";
    public void LoadMainMenu()
    {
        GameManager.Instance.StopMainTheme();
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
}
