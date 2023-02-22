using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHigh : PickUp
{
    [Header("Jump attributes")]
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float minJumpHeight;
    
    private float maxJumpHeightToRestore;
    private float minJumpHeightToRestore;
    
    public override void ApplyPlayer()
    {
        
        PlayerVelocity pv = player.GetComponent<PlayerVelocity>();
        maxJumpHeightToRestore = pv.MaxJumpHeight;
        pv.MaxJumpHeight = maxJumpHeight;
        
        minJumpHeightToRestore = pv.MinJumpHeight;
        pv.MinJumpHeight = minJumpHeight;
        pv.updateJumpHeight();

    }
    public override void ApplySoul()
    {
        throw new System.NotImplementedException();
    }
    public override void RemovePlayer()
    {
        if (PlayerExist()) 
        {
            PlayerVelocity pv = player.GetComponent<PlayerVelocity>();
            if (pv != null)
            {
                pv.MaxJumpHeight = maxJumpHeightToRestore;
                pv.MinJumpHeight = minJumpHeightToRestore;
                pv.updateJumpHeight();
            }
        }
        
    }
    public override void RemoveSoul()
    {
        throw new System.NotImplementedException();
    }
}
