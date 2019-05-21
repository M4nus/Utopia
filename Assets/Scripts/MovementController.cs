using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;

    float _moveSpeed;

    void Start()
    {
        _moveSpeed = 0f;
    }

    void Update ()
    {
        animator.SetFloat("Speed", Mathf.Abs(_moveSpeed));
        _moveSpeed = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            _moveSpeed = 1f;
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
            Move();
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            _moveSpeed = 1f;
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped      
            Move();
        } 

    }

    void Move()
    {
        gameObject.transform.Translate(new Vector2(-_moveSpeed * Time.deltaTime, 0));       
    }
}
