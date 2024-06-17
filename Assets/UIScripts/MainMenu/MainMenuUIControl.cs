using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIControl : MonoBehaviour
{
    public const string GAMEPLAY_SCENE = "gamePlay";
    public const string CREDITS_SCENE = "Credits";

    public Image mainCharacter;
    public List<Sprite> mainMenuCharacters;

    private void Awake()
    {
       LoadRandomCharacter(); 
    }

    private void LoadRandomCharacter()
    {
        int pos = (int)Random.Range(0,2);
        if(mainMenuCharacters.Count > 0)
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
        SceneManager.LoadScene(GAMEPLAY_SCENE);
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
