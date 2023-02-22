using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : Menu
{
    public static SkillMenu instance;
    [SerializeField] private SingleSkill[] skills;
    
    
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

    internal void UnlockSkill(string namePassed)
    {
        for (int i=0; i < skills.Length; i++)
        {
            if (skills[i].GetName().Equals(namePassed))
            {
                skills[i].Unlock();
                return;
            }
        }
    }

    internal void LockSkill(string name)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetName().Equals(name))
            {
                skills[i].Lock();
                return;
            }
        }
    }

}
