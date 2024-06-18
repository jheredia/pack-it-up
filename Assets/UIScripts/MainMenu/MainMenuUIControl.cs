using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIControl : MonoBehaviour
{
    public const string GAMEPLAY_SCENE = "GameLoop";
    public const string CREDITS_SCENE = "Credits";

    public Image mainCharacter;
    public List<Sprite> mainMenuCharacters;

    private GameManager _gameManager;

    private void Awake()
    {
        LoadRandomCharacter();
        _gameManager = GameManager.Instance;
    }

    private void LoadRandomCharacter()
    {
        int pos = Random.Range(0, 2);
        if (mainMenuCharacters.Count > 0)
        {
            mainCharacter.sprite = mainMenuCharacters[pos];
        }
        else
        {
            Debug.Log("no sprites in mainMenuCharactersList");
        }
    }

    public void LoadGame()
    {
        _gameManager.StartGame();
    }


    public void LoadCredits()
    {
        SceneManager.LoadScene(CREDITS_SCENE);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


}
