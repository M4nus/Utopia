using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInterfaceNoteButtonScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public bool IsClicked = false;
    public Sprite buttonClickedON;
    public Sprite buttonClickedOFF;
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
        if (!IsClicked)
        {
            Debug.Log("NoteButton got clicked on! :D");
            IsClicked = true;
            UpdateSprite();
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("NoteButton got clicked off! :C");
            IsClicked = false;
            UpdateSprite();
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    void UpdateSprite()
    {
        if (IsClicked)
        {
            this.GetComponent<SpriteRenderer>().sprite = buttonClickedON;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = buttonClickedOFF;
        }
    }
}
