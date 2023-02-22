using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{

    [SerializeField] private float timeBeforFall;
    [SerializeField] private float timeBeforRespawn;
    [SerializeField] private float speedFall;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private Transform _transform;
    private Vector3 position;

    private bool canTranslate;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        _transform = gameObject.GetComponent<Transform>();
        position = _transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        canTranslate = false;
    }

    private void Update()
    {
        if (canTranslate)
        {
            _transform.Translate(Vector3.down*speedFall*Time.deltaTime);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Vector2 collisionPoint = collision.ClosestPoint(_transform.position);
            Vector3 playerCenter = collision.bounds.center;
            
            bool isBelowPlayer = collisionPoint.y < playerCenter.y;

            if (isBelowPlayer)
            {
                StartCoroutine("StartFall");

            }
        }
    }


    private IEnumerator StartFall()
    {
        //start an animation to make it shake?
        yield return new WaitForSeconds(timeBeforFall);
        canTranslate = true;
        boxCollider.enabled = false;
        StartCoroutine("Respawn");
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(timeBeforRespawn);
        canTranslate = false;
        _transform.position = position;
        boxCollider.enabled = true;
    }
   
}
