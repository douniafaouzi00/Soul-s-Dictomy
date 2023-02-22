using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private Sprite[] startSprite;
    [SerializeField] private Sprite travelSprite;
    [SerializeField] private Sprite[] despawnSprite;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float timeFrameAnim;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private SpriteRenderer _spriteRender;
    private BoxCollider2D _boxCollider;
    public LayerMask layerIDestory;
    private bool canMove;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
        _transform = gameObject.GetComponent<Transform>();
        _spriteRender = gameObject.GetComponent<SpriteRenderer>();
        _spriteRender.sprite = startSprite[0];
        canMove = true;
        StartCoroutine("StartBullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
            
        _rigidbody.velocity = _transform.right * moveSpeed;
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, _transform.right, 0.75f, layerIDestory);
        Debug.DrawRay(_transform.position, _transform.right, Color.red);
        
        if (hit)
        {
            _rigidbody.velocity = Vector2.zero;
            StartCoroutine("DestroyBullet");
        }
    }

    private IEnumerator StartBullet()
    {
        for(int i=1; i<startSprite.Length; i++)
        {
            yield return new WaitForSeconds(timeFrameAnim);
            _spriteRender.sprite = startSprite[i];
        }
        yield return new WaitForSeconds(timeFrameAnim);
        _spriteRender.sprite = travelSprite;
    }

    private IEnumerator DestroyBullet()
    {
        _boxCollider.enabled = false;
        canMove = false;
        for (int i = 0; i < despawnSprite.Length; i++)
        {
            _spriteRender.sprite = despawnSprite[i];
            yield return new WaitForSeconds(timeFrameAnim);
        }
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void AfterApplyDamage()
    {
        StartCoroutine("DestroyBullet");
    }
}