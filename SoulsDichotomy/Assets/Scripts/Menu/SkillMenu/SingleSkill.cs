using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleSkill : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Skill mySkill;
    private bool unlocked;
    private bool alreadySet;
    private void Start()
    {
        if (!alreadySet)
            Lock();
    }

    public string GetName()
    {
        return mySkill.nameS;
    }

    private void OnEnable()
    {
        //SetUp();
    }

    private void SetUp()
    {
        if (unlocked)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    public void Lock()
    {
        alreadySet = true;
        unlocked = false;
        title.text = "Locked";
        description.text = "Unknown";
        icon.sprite = mySkill.imageNotActive;
    }

    public void Unlock()
    {
        unlocked = true;
        alreadySet = true;
        title.text = mySkill.nameS;
        description.text = mySkill.description;
        icon.sprite = mySkill.imageActive;
    }

}
