﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInterfaceExitNoButtonScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public Sprite clickedButtonSprite;
    public Sprite notClickedButtonSprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
        this.transform.parent.GetComponent<SpriteRenderer>().sprite = clickedButtonSprite;
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
        this.transform.parent.GetComponent<SpriteRenderer>().sprite = notClickedButtonSprite;
    }

    void OnMouseDown()
    {
        Debug.Log("Exit GAME NO got clicked on! :D");
        FindObjectOfType<AudioManager>().Play("click2");
        this.transform.parent.GetComponent<SpriteRenderer>().sprite = notClickedButtonSprite;
        this.transform.parent.parent.parent.parent.transform.GetComponent<PCInterfaceExitFromGameButtonScript>().SendMessage("ToogleButton");
        
    }
}
