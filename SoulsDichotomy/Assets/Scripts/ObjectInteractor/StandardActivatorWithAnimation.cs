using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardActivatorWithAnimation : StandardActivator
{
    [Header("From open to close")]
    [SerializeField] private Sprite[] animationFrame;
    [SerializeField] private float timeFrame;

    
    public override void Switch()
    {
        AudioManager.instance.PlaySound(sfxEffect);

        if (amIActive)
        {
            StartCoroutine("OpenAnim");
        }
        else
        {
            StartCoroutine("CloseAnim");
        }
    }

    private IEnumerator OpenAnim()
    {

        for (int i = 0; i < animationFrame.Length; i++)
        {
            spriteRenderer.sprite = animationFrame[i];
            yield return new WaitForSeconds(timeFrame);
        }
        spriteRenderer.sprite = nonActiveSprite;
        ReactAll();
    }

    private IEnumerator CloseAnim()
    {
        for (int i = animationFrame.Length - 1; i >= 0; i--)
        {
            spriteRenderer.sprite = animationFrame[i];
            yield return new WaitForSeconds(timeFrame);
        }
        spriteRenderer.sprite = activeSprite;
        ReactAll();
    }
}
