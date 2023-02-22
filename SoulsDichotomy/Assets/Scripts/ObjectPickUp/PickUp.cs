using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class PickUp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    [Header("Prefab to instantiate when picked")]
    [SerializeField] GameObject iconPickUp;
    [SerializeField] PickUpSetUpSO option;

    [Header("Reference to particolar effect")]
    [SerializeField] private ParticleSystem ps;

    [Header("Who can pick up?")]
    [SerializeField] private bool canPlayerPick;
    [SerializeField] private bool canSoulPick;

    [Header("Who will affect?")]
    [SerializeField] private bool affectPlayer;
    [SerializeField] private bool affectSoul;

    [Header("Timer attributess")]
    [SerializeField] private bool hasTimer;
    [SerializeField] private Timer timer;
    [SerializeField] private bool canRespawn;

    protected GameObject player;
    protected GameObject soul;

    private GameObject myIconInstantiate;
    private void OnValidate()
    {
        timer = gameObject.GetComponent<Timer>();
        if ( hasTimer && timer.GetTime() <= 0)
        {
            Debug.LogWarning("You are using a timer on " + gameObject.name + " but has an invalid time to count!");
        }
        if(hasTimer && iconPickUp == null)
        {
            Debug.LogWarning("You can't have a pick up based over time witout a prefab to instantiate!");
        }
    }

    private void Awake()
    {
        timer = gameObject.GetComponent<Timer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetUpColorParticolarSystem();
    }

    private void Start()
    {
        timer = gameObject.GetComponent<Timer>();
    }

    private void SetUpColorParticolarSystem()
    {
        if(canPlayerPick && !canSoulPick)
        {
            ps.startColor = option.onlyPlayerCanPick;
            //player
        }
        else if(!canPlayerPick && canSoulPick)
        {
            ps.startColor = option.onlySoulCanPick;
            //soul
        }
        else
        {
            ps.startColor = option.bothCanPick;
            //both
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (canPlayerPick && collision.CompareTag("Player"))
        {
            SetPlayerAndSoul();
            DispatchAffect();
            ManageEndPowerUp();
        }
            
        if(canSoulPick && collision.CompareTag("Soul"))
        {
            SetPlayerAndSoul();
            DispatchAffect();
            ManageEndPowerUp();
        }
    }

    public void SetPlayerAndSoul()
    {
        soul = GameObject.FindGameObjectWithTag("Soul");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void DispatchAffect()
    {
        AudioManager.instance.PlaySound("Pickup");

        if (affectPlayer)
        {
            ApplyPlayer();
        }
        if (affectSoul)
        {
            ApplySoul();
        }
        if(hasTimer)
            myIconInstantiate = UIManager.instance.NewPickUp(iconPickUp, timer.GetTime());
    }

    private void RemoveAffect()
    {
        if (affectPlayer)
        {
            timer.timeExpire += RemovePlayer;
        }

        if(affectSoul)
        {
            timer.timeExpire += RemoveSoul;
        }
    }

    public void ManageEndPowerUp()
    {
        if (!hasTimer)
        {
            Destroy();
        }
        else
        {
            RemoveAffect();
            DisableVisibilityAndInteraction();
            if (canRespawn)
            {
                timer.timeExpire += Respawn;
            }
            else
            {
                timer.timeExpire += Destroy;
            }
            LevelManager.changeScene += DetachAndDestory;
            timer.StartTimer();
        }
        
    }

    private void DisableVisibilityAndInteraction()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        ps.gameObject.SetActive(false);
    }

    public abstract void ApplyPlayer();
    public abstract void ApplySoul();
    public abstract void RemovePlayer();
    public abstract void RemoveSoul();

    private void Respawn()
    {
        if (this != null)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
            ps.gameObject.SetActive(true);
        }
    }

    private void Destroy()
    {
        LevelManager.changeScene -= DetachAndDestory;
        if(this!=null)
            Destroy(this.gameObject);
    }

    private void DetachAndDestory()
    {
        Destroy(myIconInstantiate);
        timer.MakeTimerExpire();
    }

    protected bool PlayerExist()
    {
        return player != null;
    }
    protected bool SoulExist(){
        return soul != null;
    }
}
