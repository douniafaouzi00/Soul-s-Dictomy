using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSoul : Damage
{
    void Start()
    {
        CanDamagePlayer = false;
        CanDamageSoul = true;
    }
}
