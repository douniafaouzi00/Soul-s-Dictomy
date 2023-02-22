using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : ScriptableObject
{
    public string nameS;
    public string description;
    public Sprite imageActive;
    public Sprite imageNotActive;

    [SerializeField] protected bool isUnlock;
    [SerializeField] protected int levelUnlock;
    [SerializeField] protected bool affectPlayer;
    [SerializeField] protected bool affectSoul;
    public virtual void AttachSkill(PlayerInput player, SoulController soul)
    {

    }

    public virtual void DetachSkill(PlayerInput player, SoulController soul)
    {

    }

    public bool IsUnlock()
    {
        return isUnlock;
    }

    public void SetIsUnlock(bool value)
    {
        isUnlock = value;
    }

    public int GetLeveUnlock()
    {
        return levelUnlock;
    }
}
