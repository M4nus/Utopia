using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredLightScript : MonoBehaviour
{
    Texture2D cursorTexturePointer;
    Texture2D cursorTextureHand;
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpotPointer = Vector2.zero;
    Vector2 hotSpotHand = Vector2.zero;
    public Sprite clickedButtonSprite;
    public Sprite notClickedButtonSprite;
    public int ColorNumber;
   
    // Start is called before the first frame update
    void Start()
    {

        cursorTexturePointer = this.transform.parent.GetComponent<ColoredLightsCollectionScript>().cursorTexturePointer;
        cursorTextureHand = this.transform.parent.GetComponent<ColoredLightsCollectionScript>().cursorTextureHand;
        hotSpotPointer = this.transform.parent.GetComponent<ColoredLightsCollectionScript>().hotSpotPointer;
        hotSpotHand = this.transform.parent.GetComponent<ColoredLightsCollectionScript>().hotSpotHand;
        ColorNumber = ((int)this.name.ToCharArray()[15] - 48) * 100 + ((int)this.name.ToCharArray()[16] - 48) * 10 + ((int)this.name.ToCharArray()[17] - 48); //Derive your index from your name
    }
               
    void OnMouseEnter()
    {
        if (transform.parent.GetComponent<ColoredLightsCollectionScript>().AllowColorClickage)
        {
            Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
        }    
    }

    void OnMouseExit()
    {
        if (transform.parent.GetComponent<ColoredLightsCollectionScript>().AllowColorClickage)
        {
            Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
        }    
    }

    void OnMouseDown()
    {
        if (transform.parent.GetComponent<ColoredLightsCollectionScript>().AllowColorClickage)
        {
            Debug.Log("Color" + ColorNumber + " got clicked on! :D");
            this.transform.parent.SendMessage("ClickedLight", ColorNumber); //Tell collection that you were clicked   
            FindObjectOfType<AudioManager>().Play("click1");
        }
    }

    void setThisToClicked()
    {
        this.GetComponent<SpriteRenderer>().sprite = clickedButtonSprite; //Put orange circle around the light
    }

    void setThisToNotClicked()
    {
        this.GetComponent<SpriteRenderer>().sprite = notClickedButtonSprite; //Remove orange circle from the light
    }
}
