using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredLightsCollectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public int colorIndex = -1;
    public bool AllowColorClickage= false;
    public bool clickedCorrectWithNRE = false;
    public bool clickedTheSameColor = false;
    public GameObject Elevator;
    public GameObject ControlLights;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedLight(int index) //This is called two times : first choose your color and shuffle ; second check if picked the same color and open elevator
    {
        if(colorIndex < 0) //first
        {
            colorIndex = index;
            if (colorIndex == 18)
            {
                clickedCorrectWithNRE = true;
            }
            this.transform.GetChild(colorIndex).SendMessage("setThisToNotClicked"); //Remove circle from child
            ShuffleLightCollection();
        }
        else //second
        {
            this.transform.GetChild(index).SendMessage("setThisToClicked");
            if (colorIndex == index)
            {
                clickedTheSameColor = true;
            }
            if(clickedTheSameColor&& clickedCorrectWithNRE)
            {
                ControlLights.SendMessage("setOrangeActive");
            }
            else
            {
                ControlLights.SendMessage("setBlueActive");
            }
            Debug.Log("PLayer finished room " + "same-" + clickedTheSameColor + " correct-" + clickedCorrectWithNRE);
            Elevator.SendMessage("OpenElevator");
            AllowColorClickage = false;
        }
        
    }
    public void ShuffleLightCollection()
    {
        int tempSwitchSelector;
        Vector3 tempPositionHolder;
        for(int i=0; i<this.transform.childCount; i++)
        {
            tempSwitchSelector = (int)Random.Range(0, 156);
            tempPositionHolder = this.transform.GetChild(i).transform.position;
            this.transform.GetChild(i).transform.position = this.transform.GetChild(tempSwitchSelector).transform.position;
            this.transform.GetChild(tempSwitchSelector).transform.position = tempPositionHolder;
        }
    }
}
