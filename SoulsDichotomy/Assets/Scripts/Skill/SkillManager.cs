using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private PlayerInput player;
    [SerializeField] private SoulController soul;
    
    [SerializeField] private Skill[] skillTree;

    public static SkillManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }




    public void NextLevelToLoad(int levelToLoad)
    {
        foreach (Skill s in skillTree)
        {
            if (s.GetLeveUnlock() == levelToLoad)
            {
                UnlockSkill(s);
            }
        }
        
    }

    public void LoadLevelFromScratch(int levelToLoad)
    {
        
        foreach (Skill s in skillTree)
        {
            if (s.GetLeveUnlock() <= levelToLoad)
            {
                
                UnlockSkill(s);
                   
            }
            else
            {
                if(s.IsUnlock())
                    LockSkill(s);
            }
        }
        
    }

    public void SetUpCharacters(PlayerInput pi, SoulController sc)
    {
        player = pi;
        soul = sc;
    }

    public Skill[] GetSkills()
    {
        return skillTree;
    }

    private void LockSkill(Skill s)
    {
        s.SetIsUnlock(false);
        s.DetachSkill(player, soul);
        SkillMenu.instance.LockSkill(s.nameS);
    }

    private void UnlockSkill(Skill s)
    {
        UIManager.instance.UnlockSkill();
        s.SetIsUnlock(true);
        s.AttachSkill(player, soul);
        SkillMenu.instance.UnlockSkill(s.nameS);
    }



    internal bool AmIReady()
    {
        return player != null && soul != null;
    }

}
