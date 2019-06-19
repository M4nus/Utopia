using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rigidbody2D;
    float _moveSpeed = 10;
    public float _targetMoveSpeed = 10;
    public bool MovementEnabled = true;

    void Start()
    {
        //_moveSpeed = 0f;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (MovementEnabled)
        {
            animator.SetFloat("Speed", Mathf.Abs(_moveSpeed));
            _moveSpeed = 0f;
            rigidbody2D.velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _moveSpeed = -_targetMoveSpeed;
                transform.eulerAngles = new Vector3(0, 0, 0); // Normal
                Move(_moveSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _moveSpeed = _targetMoveSpeed;
                transform.eulerAngles = new Vector3(0, 180, 0); // Flipped      
                Move(_moveSpeed);
            }
        }
    }

    void Move(float moveSpeed)
    {
        rigidbody2D.velocity = new Vector3(moveSpeed, 0.0f, 0.0f);
        //gameObject.transform.Translate(new Vector2(-_moveSpeed * Time.deltaTime, 0));       
    }


    IEnumerator MoveToPosition(float x)
    {
        Debug.Log("Antoni tries to move to x=" + x);
        if(x > this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        while (Mathf.Abs(this.transform.position.x - x)>1)
        {
            if(this.transform.position.x > x)
            {
                animator.SetFloat("Speed", Mathf.Abs(-70));
                Move(-70);
            }
            else
            {
                animator.SetFloat("Speed", Mathf.Abs(70));
                Move(70);
            }
            yield return null;
        }
        Move(0);
        transform.position = new Vector3(x,transform.position.y, transform.position.z);
        //transform.eulerAngles = new Vector3(0, transform.rotation.y+180, 0);
        
        

    }


}
