
using Assets.HeroEditor4D.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour, IHittable
{
    //reference for player
    private GameObject _player;
    [Header("SoulStat")]
    public Health soulHealth;
    [Range(0, 100)]
    public int healthDecreasePerSec;
    [Range(0, 100)]
    public int healtIncreasePerSec;


    [Header("Effect")]
    public ParticlesPlayer particles;

    [Header("Chase player stats")]
    public Vector3 offset;
    public float radiusNotToChase;

    [Header("Movement")]
    public Character4D Character;
    public bool InitDirection;
    public int movementSpeed;
    private bool moveFromInput;
    private bool _moving;
    private bool isInRange;

    private bool gameOver;
    public bool GameOver { set { gameOver = value; } get { return gameOver; } }

    //input soul
    private KeyCode up;
    private KeyCode down;
    private KeyCode right;
    private KeyCode left;

    private KeyCode interact;

    //soul interaction
    private IInteract interactObj;
    public IInteract InteractObj { set { interactObj = value; } }
    
    //references
    private Transform _transform;


    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        soulHealth.SetUpHealth();
        moveFromInput = false;
        _transform = transform;
        isInRange = true;
        gameOver = false;
    }

    public void Start()
    {
        GameManager.changeCharacter += SwitchCharacter;
        Character.AnimationManager.SetState(CharacterState.Idle);
        if (InitDirection)
        {
            Character.SetDirection(Vector2.left);
        }
        CustomizeInput.ChangeInput += ChangeCustomizeInput;
    }

    public void Update()
    {
        if (gameOver || InGameMenu.instance.isPaused)
            return;
        if (moveFromInput)
        {
            SetDirection();
            MoveFromInput();
            Interaction();
        }
        else
        {
            Vector2 distance = _player.transform.position - _transform.position;
            Vector2 pointToReach = distance - (Vector2) offset;
            Vector2 direction = (pointToReach).normalized;
            if (distance.x > 0)
            {
                FaceRightWay(Vector2.right);
                offset.x = 1;
            }
            else
            {
                FaceRightWay(Vector2.left);
                offset.x = -1;
            }
            if (_moving == true)
            {
                if(Mathf.Abs(pointToReach.x)<0.15f && Mathf.Abs(pointToReach.y)<0.15f)
                {
                    Move(Vector2.zero);
                }
                else{
                    Move(direction);
                }
            }
            else
            {
                if (Mathf.Abs(distance.x) > radiusNotToChase || Mathf.Abs(distance.y) > radiusNotToChase)
                {
                    Move(direction);
                }
            }
        }
        
    }

    private void SwitchCharacter()
    {
        moveFromInput = !moveFromInput;
        CancelInvoke();
        if (isInRange)
        {
            StartInvokeHeal();
        }
        else if(moveFromInput)
        {
            StartInvokeDamage();
        }
    }

    private void SetDirection()
    {
        Vector2 direction;

        if (Input.GetKeyDown(left))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(right))
        {
            direction = Vector2.right;
        }
        else return;

        Character.SetDirection(direction);
    }

    private void FaceRightWay(Vector2 direction)
    {
        Character.SetDirection(direction);
    }

    private void MoveFromInput()
    {
        if (movementSpeed == 0) return;

        var direction = Vector2.zero;

        if (Input.GetKey(left))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(right))
        {
            direction += Vector2.right;
        }

        if (Input.GetKey(up))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(down))
        {
            direction += Vector2.down;
        }
        Move(direction);
    }

    private void Move(Vector2 direction)
    {

        if (direction == Vector2.zero)
        {
            if (_moving)
            {
                Character.AnimationManager.SetState(CharacterState.Idle);
                _moving = false;
            }
        }
        else
        {
            Character.AnimationManager.SetState(CharacterState.Run);
            Character.transform.position += (Vector3)direction.normalized * movementSpeed * Time.deltaTime;
            _moving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AreaSoul")
        {
            isInRange = false;
            if (!moveFromInput)
                return;
            CancelInvoke();
            StartInvokeDamage();
            
        }

        IInteract interact = collision.gameObject.GetComponent<IInteract>();
        if (interact != null)
        {
            interactObj = null;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AreaSoul")
        {
            CancelInvoke();
            StartInvokeHeal();
            isInRange = true;
        }
        
        //soul interaction
        IInteract interact = collision.gameObject.GetComponent<IInteract>();
        if (interact != null)
        {
            if(interact.CanSoulInteract())
                interactObj = interact;
        }
    }


    private void StartInvokeDamage()
    {
        InvokeRepeating("Damage", 0.5f, 1f);
    }

    private void StartInvokeHeal()
    {
        InvokeRepeating("Heal", 0.5f, 1f);
    }

    private void Heal()
    {
        if (soulHealth.isFullHp())
        {
            CancelInvoke();
            return;
        }
        soulHealth.AddHp(healtIncreasePerSec);
    }

    //called from invokerepeated
    private void Damage()
    {
        Hit(healthDecreasePerSec);
    }

    public void HealExtras()
    {
        particles.HealEffect(_transform);
        UIManager.instance.SliderSoul(soulHealth.Percentage());
    }

    public void DamageExtras()
    {
        AudioManager.instance.PlaySound("SoulDamage", 1.0f - soulHealth.Percentage());

        particles.DamageEffect(_transform);
        Character.AnimationManager.Hit();
        UIManager.instance.SliderSoul(soulHealth.Percentage());
    }

    public void DeathExtra()
    {
        AudioManager.instance.PlaySound("Death");

        Character.AnimationManager.Die();
        GameManager.instance.SomeoneDie();

    }
    
    public void Hit(int amount)
    {
        soulHealth.SubtractHp(amount);
    }

    
    
    //soul interaction
    
    private void Interaction()
    {
        if (interactObj!=null && Input.GetKeyDown(interact))
        {
            interactObj.Interact();
        }
    }


    private void OnDestroy()
    {
        GameManager.changeCharacter -= SwitchCharacter;
        CustomizeInput.changeInput -= ChangeCustomizeInput;

    }


    public void ResetSoul()
    {
        soulHealth.SetUpHealth();
        Character.AnimationManager.SetState(CharacterState.Idle);
        moveFromInput = false;
        GameOver = false;
        interactObj = null;
        //todo remove active pickup

    }

    private void ChangeCustomizeInput(KeyCode up, /*KeyCode down,*/ KeyCode right, KeyCode left, KeyCode interact, KeyCode switchChar, KeyCode upS, KeyCode downS, KeyCode rightS, KeyCode leftS)
    {
        this.up = upS;
        this.down = downS;
        this.right = rightS;
        this.left = leftS;
        this.interact = interact;
    }
}
