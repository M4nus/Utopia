﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInterfaceMenuBackButtonScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
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
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
    }

    void OnMouseDown()
    {
        Debug.Log("BackButton got clicked on! :D");
        FindObjectOfType<AudioManager>().Play("click2");
        this.transform.parent.parent.transform.gameObject.SetActive(false);
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
    }
}
