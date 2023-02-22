using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxDimSoulMovement", menuName = "Skill/MaxDimSoulMovement", order = 1)]
public class MaxDimSoulMovement : Skill
{
    [Header("Affect only player")]
    [SerializeField] private Vector3 newScale;
    private Vector3 oldScale;
    public override void AttachSkill(PlayerInput player, SoulController soul)
    {
        if (affectPlayer)
        {
            oldScale = player.GetScaleSoulPanel();
            player.SetScaleToSoulPanel(newScale);
        }
    }

    public override void DetachSkill(PlayerInput player, SoulController soul)
    {
        if (affectPlayer)
        {
            player.SetScaleToSoulPanel(oldScale);
        }
    }
}
