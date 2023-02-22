using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : Menu
{
    public static GameOverMenu instance;
    [SerializeField] private TextMeshProUGUI titleMenu;
    [SerializeField] private TextMeshProUGUI description;
    public string[] sentenceLost;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Close();
    }

    internal void EndGame()
    {
        titleMenu.text = "GAME OVER!";
        description.text = "You reached the end of the dungeon, only few did it before you! Feel proud of what you just did.";
    }

    internal void LevelFailed()
    {
        titleMenu.text = "You failed the level!";
        description.text = sentenceLost[Random.Range(0, sentenceLost.Length)];
        description.text += "\n\nMalus applied: +" + ScoreManager.instance.GetDieValue() + " s";
    }

    public void TryAgain()
    {
        MenuManager.instance.Back();//chiudo menu

        GameManager.instance.TryAgainSetup();//settuppo i menu

        UIManager.instance.SetUpSlider();//settuppo gli slider

        LevelManager.instance.TryAgainLevel();//ricarico la scenaLevelManager.instance.TryAgainLevel();//ricarico la scena
    }


    public void MainMenu()
    {
        MenuManager.instance.Back();
        LevelManager.instance.BackToMainMenu();
        
    }
}
