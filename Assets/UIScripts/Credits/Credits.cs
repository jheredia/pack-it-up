using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "MainMenu";
    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMenu(this, EventArgs.Empty);
    }
}
