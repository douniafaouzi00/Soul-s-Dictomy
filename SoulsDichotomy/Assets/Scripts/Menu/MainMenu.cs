using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{

    public static MainMenu instance;
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

    public void Play()
    {
        MenuManager.instance.Back();
        LevelManager.instance.LoadIntroVideo();
        //Instantiate(levelManager, Vector3.zero, Quaternion.identity);
    }

    public void OpenSettings()
    {
        MenuManager.instance.Open(SettingsMenu.instance);
    }

    public void OpenCredit()
    {
        MenuManager.instance.Open(CreditsMenu.instance);
    }

    public override void Exit()
    {
        Application.Quit();
    }

    public void OpenLevelSelect()
    {
        MenuManager.instance.Open(LevelSelectorMenu.instance);
    }
}
