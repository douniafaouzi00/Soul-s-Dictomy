using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class DamageReact : MonoBehaviour, IReact
{
    [Header("From turn off to turn on")]
    [SerializeField] private Sprite[] openSprites;
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] closeSprites;
    [SerializeField] private float timeAnimation = 0.5f;
    [Header("Initial State")]
    [SerializeField] private bool amIActive;
    
    private Damage myDamage;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        myDamage = gameObject.GetComponent<Damage>();
    }

    private void Start()
    {
        if (amIActive)
        {
            spriteRenderer.sprite = idleSprites[0];
            StartCoroutine("IdleDamage");
            boxCollider.enabled = true;
            myDamage.enabled = true;
        }
        else
        {
            spriteRenderer.sprite = openSprites[0];
            boxCollider.enabled = false;
            myDamage.enabled = false;
        }
    }

    public void React()
    {
        if (amIActive)
        {
            StopAllCoroutines();
            StartCoroutine("CloseDamage");
        }
        else
        {
            StartCoroutine("OpenDamage");
        }
        amIActive = !amIActive;
    }

    private IEnumerator OpenDamage()
    {
        boxCollider.enabled = true;
        myDamage.enabled = true;
        for (int i = 1; i < openSprites.Length; i++)
        {
            spriteRenderer.sprite = openSprites[i];
            yield return new WaitForSeconds(timeAnimation);
        }
        StartCoroutine("IdleDamage");
    }

    private IEnumerator IdleDamage()
    {
        while (true)
        {
            for(int i=0; i<idleSprites.Length; i++)
            {
                spriteRenderer.sprite = idleSprites[i];
                yield return new WaitForSeconds(timeAnimation);
            }
        }
    }

    private IEnumerator CloseDamage()
    {
        boxCollider.enabled = false;
        myDamage.enabled = false;
        for (int i = 0; i < closeSprites.Length; i++)
        {
            spriteRenderer.sprite = closeSprites[i];
            yield return new WaitForSeconds(timeAnimation);
        }
        spriteRenderer.sprite = openSprites[0];
    }

}
