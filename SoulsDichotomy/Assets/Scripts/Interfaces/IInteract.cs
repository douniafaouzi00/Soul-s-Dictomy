using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract 
{
    public void Interact();
    public bool CanPlayerInteract();
    public bool CanSoulInteract();
    public bool CanActivateOnTriggerEnter();
}
