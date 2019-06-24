﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInterfaceBlackboardRoom : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public GameObject chalk;
<<<<<<< HEAD
    public GameObject Antoni;  
=======
    public GameObject Antoni;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a

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
        Debug.Log("PCTerminal got clicked on! :D");
        StartCoroutine(MoveToPCInterface(this.transform.position.x - 8.0f));
    }


    IEnumerator MoveToPCInterface(float x)
    {
        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at PCInterface");
        yield return new WaitForSeconds(0.4f);
        chalk.SendMessage("ActivateChalk");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
