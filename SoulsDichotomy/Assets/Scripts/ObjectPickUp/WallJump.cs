using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : PickUp
{
    [Header("Wall jump attributes")]
    [SerializeField] private Vector2 wallJump;
    [SerializeField] private Vector2 wallJumpClimb;
    [SerializeField] private Vector2 wallLeapOff;

    private Vector2 wallJumpToRestore;
    private Vector2 wallJumpClimbToRestore;
    private Vector2 wallLeapOffToRestore;
    public override void ApplyPlayer()
    {
        PlayerVelocity pv = player.GetComponent<PlayerVelocity>();
        pv.AllowWallJump = true;
        wallJumpToRestore = pv.WallJump;
        pv.WallJump = wallJump;
        
        wallJumpClimbToRestore = pv.WallJumpClimb;
        pv.WallJumpClimb = wallJumpClimb;
        
        wallLeapOffToRestore = pv.WallLeapOff;
        pv.WallLeapOff = wallLeapOff;

        pv.WallSlideSpeedMax = 5;

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
                pv.AllowWallJump = false;
                pv.WallJump = wallJumpToRestore;
                pv.WallJumpClimb = wallJumpClimbToRestore;
                pv.WallLeapOff = wallLeapOffToRestore;
                pv.WallSlideSpeedMax = 50;
            }
        }  
    }
    public override void RemoveSoul()
    {
        throw new System.NotImplementedException();
    }
}