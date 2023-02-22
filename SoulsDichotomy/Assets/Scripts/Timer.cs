using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;

    private float timeCount;
    private bool timerStarted;

    public delegate void OnTimeExpire();
    public OnTimeExpire timeExpire;

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            if (timeCount > 0)
            {
                timeCount -= Time.deltaTime;
            }
            else
            {
                timerStarted = false;
                //print("expire");
                timeExpire();
            }
        }
    }

    public void StartTimer()
    {
        timeCount = time;
        timerStarted = true;
    }

    public float GetTime()
    {
        return time;
    }

    public void MakeTimerExpire()
    {
        timeExpire();
    }

}
