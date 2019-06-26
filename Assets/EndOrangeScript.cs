using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOrangeScript : MonoBehaviour
{
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotHand = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
    }

  

    void OnMouseDown()
    {
        Debug.Log("PlayerHasQuitTheGame");
        Application.Quit();
    }
}
