using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBullet : MonoBehaviour
{

    [SerializeField] private int moveSpeed;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    public LayerMask layerIDestory;
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = _transform.right * -moveSpeed;
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, -_transform.right,0.55f, layerIDestory);
        Debug.DrawRay(_transform.position, -_transform.right , Color.red);
        
        if (hit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    
    public void AfterApplyDamage()
    {
        Destroy(this.gameObject);
    }

}
