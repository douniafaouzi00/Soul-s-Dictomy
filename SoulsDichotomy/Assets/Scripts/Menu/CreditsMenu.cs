using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : Menu
{
    public static CreditsMenu instance;
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
}
