using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIControl : MonoBehaviour
{
    [SerializeField]
    private bool _debugMode = false;
    public const string GAMEPLAY_SCENE = "GameLoop";
    public const string CREDITS_SCENE = "Credits";
    private const string DEBUG_SCENE = "Playground";

    public Image mainCharacter;
    public List<Sprite> mainMenuCharacters;

    private void Awake()
    {
        LoadRandomCharacter();
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
        if (_debugMode) SceneManager.LoadScene(DEBUG_SCENE);
        else SceneManager.LoadScene(GAMEPLAY_SCENE);
    }


    public void LoadCredits()
    {
        SceneManager.LoadScene(CREDITS_SCENE);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
