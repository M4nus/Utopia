using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{                      
    float _moveSpeed;       

    void Start()
    {
        _moveSpeed = 1f;
    }

    void Update ()
    {        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
            Move();
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped      
            Move();
        } 
    }

    void Move()
    {
        gameObject.transform.Translate(new Vector2(-_moveSpeed * Time.deltaTime, 0));   
    }
}
