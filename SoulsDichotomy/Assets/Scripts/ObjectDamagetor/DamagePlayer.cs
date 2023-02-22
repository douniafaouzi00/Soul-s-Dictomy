using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : Damage
{
    
    void Start()
    {
        CanDamagePlayer = true;
        CanDamageSoul = false;
    }

}
