using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rigidbody2D;
    float _moveSpeed = 10;
    public float _targetMoveSpeed = 10;

    void Start()
    {
        //_moveSpeed = 0f;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        animator.SetFloat("Speed", Mathf.Abs(_moveSpeed));
        _moveSpeed = 0f;
        rigidbody2D.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            _moveSpeed = -_targetMoveSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
            Move();
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            _moveSpeed = _targetMoveSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped      
            Move();
        } 

    }

    void Move()
    {
        rigidbody2D.velocity = new Vector3(_moveSpeed, 0.0f, 0.0f);
        //gameObject.transform.Translate(new Vector2(-_moveSpeed * Time.deltaTime, 0));       
    }
}
