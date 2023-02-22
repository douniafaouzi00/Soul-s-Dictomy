using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ButtonActivator : StandardActivator
{
    private Timer timer;
    [SerializeField] private Sprite[] imagesTimer;
    [SerializeField] private GameObject timerObj;
    private SpriteRenderer timerSpriteRender;
    private float time;
    
    public void Awake()
    {
        base.SetUpActivator();
        timer = this.gameObject.GetComponent<Timer>();
        time=timer.GetTime();
        time = time / (imagesTimer.Length - 1);
        if(time!=0)
            timer.timeExpire += Interact;
        timerSpriteRender = timerObj.GetComponent<SpriteRenderer>();
        timerObj.SetActive(false);
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Switch()
    {
        
        if(amIActive)
        {
            
            spriteRenderer.sprite = nonActiveSprite;
            BoxCollider2DEnable = true;
        }
        else
        {
            AudioManager.instance.PlaySound(sfxEffect);
            spriteRenderer.sprite = activeSprite;
            if (time != 0)
            {
                StartCoroutine("TimerUI");
                BoxCollider2DEnable = false;
                timer.StartTimer();
            }
            
        }
        ReactAll();
    }

    private IEnumerator TimerUI()
    {
        timerObj.SetActive(true);
        
        for (int i=0; i < imagesTimer.Length; i++)
        {
            timerSpriteRender.sprite = imagesTimer[i];
            yield return new WaitForSeconds(time);
        }
        timerObj.SetActive(false);
    }
}
