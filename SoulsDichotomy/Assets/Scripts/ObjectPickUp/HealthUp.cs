using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PickUp
{
    [Header("Particualr Attributes for HealthUp")]
    [SerializeField] private int amountHeal;
    public override void ApplyPlayer()
    {
        player.GetComponent<PlayerInput>().Heal(amountHeal);  
    }
    public override void ApplySoul()
    {
        player.GetComponent<PlayerInput>().Heal(amountHeal);
    }
    public override void RemovePlayer()
    {
        throw new System.NotImplementedException();
    }
    public override void RemoveSoul()
    {
        throw new System.NotImplementedException();
    }
}