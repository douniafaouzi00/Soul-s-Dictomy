using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxAmountHp", menuName = "Skill/MaxAmountHp", order = 1)]
public class MaxAmountHp : Skill
{
    [SerializeField] private int addHpAmount;
    public override void AttachSkill(PlayerInput player, SoulController soul)
    {
        if (affectPlayer)
        {
            player.playerHealth.AddMaxHealth(addHpAmount);
        }
        if (affectSoul)
        {
            soul.soulHealth.AddMaxHealth(addHpAmount);
        }
    }
}
