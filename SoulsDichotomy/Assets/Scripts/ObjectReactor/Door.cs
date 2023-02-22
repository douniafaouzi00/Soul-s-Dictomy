using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IReact
{
    [Header("Sprites - from close to open")]
    [SerializeField] private Sprite[] aminationSprite;
    [SerializeField] private float timeAnimation = 0.5f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    [Header("Initial State")]
    public bool isClosed=true;
    public float timeBeforReact = 0;

    public virtual void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    public void Start()
    {
        if (isClosed)
        {
            spriteRenderer.sprite = aminationSprite[0];

            boxCollider.enabled = true;
        }
        else
        {
            spriteRenderer.sprite = aminationSprite[aminationSprite.Length - 1];

            boxCollider.enabled = false;
        }
    }

    public virtual void React()
    {
        StopAllCoroutines();
        StartCoroutine("ReactAfterTime");

    }

    private IEnumerator ReactAfterTime()
    {
        yield return new WaitForSeconds(timeBeforReact);
        if (isClosed)
        {
            StartCoroutine("OpenDoor");
        }
        else
        {
            StartCoroutine("CloseDoor");
        }
        isClosed = !isClosed;
    }


    private IEnumerator OpenDoor()
    {
        boxCollider.enabled = false;
        for (int i=1; i< aminationSprite.Length; i++)
        {
            spriteRenderer.sprite = aminationSprite[i];
            yield return new WaitForSeconds(timeAnimation);
        }
        DoAfterOpen();
    }

    protected virtual void DoAfterOpen()
    {

    }

    private IEnumerator CloseDoor()
    {
        boxCollider.enabled = true;
        for (int i = aminationSprite.Length-2; i >=0; i--)
        {
            spriteRenderer.sprite = aminationSprite[i];
            yield return new WaitForSeconds(timeAnimation);
        }
    }
}
