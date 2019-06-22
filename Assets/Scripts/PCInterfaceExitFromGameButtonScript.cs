using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInterfaceExitFromGameButtonScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public bool IsClicked = false;
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
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
    }

    void OnMouseDown()
    {
        ToogleButton();
    }

    void ToogleButton()
    {
        if (!IsClicked)
        {
            this.transform.GetChild(0).transform.gameObject.SetActive(true);

            IsClicked = true;
            this.transform.GetComponent<SpriteRenderer>().sprite = clickedButtonSprite;
            Debug.Log("ExitGameButton got clicked on! :D");
        }
        else
        {
            Debug.Log("ExitGameButton got clicked off! :D");
            this.transform.GetComponent<SpriteRenderer>().sprite = notClickedButtonSprite;
            this.transform.GetChild(0).transform.gameObject.SetActive(false);
            IsClicked = false;
        }
    }

}
