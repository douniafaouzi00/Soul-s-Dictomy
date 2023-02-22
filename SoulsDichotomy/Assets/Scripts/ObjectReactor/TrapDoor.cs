using System;
using System.Collections;

using TMPro.Examples;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TrapDoor: Door
{
    [Header("Timer hav the time to re-open it")]
    private Timer timer;

    public override void Awake()
    {
        timer = gameObject.GetComponent<Timer>();
        base.Awake();
    }

    public override void React()
    {
        base.React();
            
    }

    protected override void DoAfterOpen()
    {
        base.DoAfterOpen();
        timer.timeExpire += React;
        timer.StartTimer();
    }


}