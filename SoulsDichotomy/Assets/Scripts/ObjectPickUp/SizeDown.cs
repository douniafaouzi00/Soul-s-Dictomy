using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDown : PickUp
{
   
    private Vector3 Scale;
   
    public override void ApplyPlayer()
    {
        Scale = new Vector3(0.5f, 0.5f, 0.5f);
        player.transform.localScale = Scale;
    }

    public override void ApplySoul()
    {
        throw new System.NotImplementedException();
    }

    public override void RemovePlayer()
    {
        Scale = new Vector3(1f, 1f, 1f);
        player.transform.localScale = Scale;
    }

    public override void RemoveSoul()
    {
        throw new System.NotImplementedException();
    }
}
