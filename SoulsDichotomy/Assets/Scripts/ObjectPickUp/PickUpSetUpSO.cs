using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickUpOption", menuName = "PickUp/PickUpOption", order = 1)]
public class PickUpSetUpSO : ScriptableObject
{
    public Color onlyPlayerCanPick;
    public Color onlySoulCanPick;
    public Color bothCanPick;
}
