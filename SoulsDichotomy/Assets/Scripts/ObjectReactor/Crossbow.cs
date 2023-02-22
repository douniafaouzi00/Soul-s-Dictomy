using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour, IReact
{
    [Header("From close to open")]
    public Sprite[] sprites;

    [SerializeField] private float timeBetweenBullets;
    [SerializeField] private float timeBeforReact;
    [SerializeField] private bool amIActive;
    [SerializeField] private bool whenActiveAlwaysShot;
    [SerializeField] private int numOfBullet;
    public GameObject bullet;
    public GameObject firePoint;

    private float toWaitAnim;
    private SpriteRenderer spriteRenderer;
    private void OnValidate()
    {
        if (whenActiveAlwaysShot)
        {
            numOfBullet = 0;
        }
    }

    private void Start()
    {
        toWaitAnim = timeBetweenBullets / (2 * (sprites.Length - 1));
        spriteRenderer= this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        if (amIActive)
        {
            StartCoroutine("StartToReact");
        }
    }

    public void React()
    {
        amIActive = !amIActive;
        if (amIActive)
        {
            StartCoroutine("StartToReact");
        }
        else
        {
            StopAllCoroutines();
            spriteRenderer.sprite = sprites[0];
        }
    }

    private IEnumerator StartToReact()
    {
        yield return new WaitForSeconds(timeBeforReact);
        StartCoroutine("StartShot");
    }

    private IEnumerator StartShot()
    {
        if (whenActiveAlwaysShot)
        {
            while (true)
            {
                StartCoroutine("OpenCross");
                yield return new WaitForSeconds(timeBetweenBullets);
            }
        }
        else
        {
            int cont = 0;
            while (cont < numOfBullet)
            {
                StartCoroutine("OpenCross"); 
                yield return new WaitForSeconds(timeBetweenBullets);
                cont++;
            }
            amIActive = false;
        }
        
    }

    private IEnumerator OpenCross()
    {
        
        for(int i=1; i<sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(toWaitAnim);
        }
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        StartCoroutine("CloseCross");
    }

    private IEnumerator CloseCross()
    {
        for (int i = sprites.Length-1; i >= 0; i--)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(toWaitAnim);
        }
    }

}
