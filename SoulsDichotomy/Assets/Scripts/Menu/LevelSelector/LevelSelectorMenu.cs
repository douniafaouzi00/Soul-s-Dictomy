using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorMenu : Menu
{
    public static LevelSelectorMenu instance;

    public GameObject[] panelsLevel;
    private int currPanelOpen;

    [SerializeField] private GameObject buttonPrec;
    [SerializeField] private GameObject buttonNext;


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

    public override void Open()
    {
        base.Open();

        currPanelOpen = 0;
        
        ActivatePanelLevel(currPanelOpen);
    }

    private void ManageButton(int page)
    {
        if (page == 0)
        {
            buttonPrec.SetActive(false);
            buttonNext.SetActive(true);
        }
        else if (page == panelsLevel.Length - 1)
        {
            buttonNext.SetActive(false);
            buttonPrec.SetActive(true);
        }
        else
        {
            buttonNext.SetActive(true);
            buttonPrec.SetActive(true);
        }
    }

    private void ActivatePanelLevel(int index)
    {
        ManageButton(index);
        for (int i=0; i< panelsLevel.Length; i++)
        {
            panelsLevel[i].SetActive(i==index);
        }
    }

    public void NextPanel()
    {
        currPanelOpen = currPanelOpen + 1;
        ActivatePanelLevel(currPanelOpen);
    }

    public void PrevPanel()
    {
        currPanelOpen = currPanelOpen - 1;
        ActivatePanelLevel(currPanelOpen);
    }

    public void LoadLevel(int level)
    {
        Exit();
        MenuManager.instance.CloseAllMenus();
        if (InGameMenu.instance != null)
            InGameMenu.instance.Resume();
        LevelManager.instance.LoadLevel(level);
    }


}
