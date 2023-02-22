using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : Menu
{
    public static SettingsMenu instance;
    CustomizeInput customizeinput;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] higlitedTopPart;


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
        customizeinput = GetComponent<CustomizeInput>();
    }

    public override void Open()
    {
        base.Open();
        ActivePanleIndex(0);
        higlitedTopPart[0].SetActive(true);
        higlitedTopPart[1].SetActive(false);
    }

    public void ActivePanleIndex(int index)
    {
        for(int i=0; i< panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
        if (index == 1)
        {
            customizeinput.SetText();
        }
    }

    public CustomizeInput GetCustomInput()
    {
        return customizeinput;
    }
}
