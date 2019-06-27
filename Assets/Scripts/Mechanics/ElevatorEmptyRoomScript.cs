using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class ElevatorEmptyRoomScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public GameObject Antoni;
    Animator animatior;
    Camera mainCamera;
    public GameObject NextElevator;
    MovementController AntoniMovementController;
    public bool ElevatorOpen = false;
    public int ElevatorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        animatior = GetComponent<Animator>();
        AntoniMovementController = Antoni.GetComponent<MovementController>();
    }                                          

    void OnMouseEnter()
    {
        if (ElevatorOpen)
        {
            Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
        }
        
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
    }

    void OnMouseDown()
    {
        Debug.Log("Elevator got clicked on! :D");
        if (ElevatorOpen)
        {
            ElevatorOpen = false;
            StartCoroutine(GoIntoElevator(this.transform.position.x));
        }

    }

    public IEnumerator OpenElevator() 
    {
        animatior.SetTrigger("OpenElevator");
        FindObjectOfType<AudioManager>().Play("elevatorOpen");
        yield return new WaitForSeconds(1.0f);
        ElevatorOpen = true;
    }

    IEnumerator GoIntoElevator(float x)
    {
        Antoni.GetComponent<MovementController>().MovementEnabled = false;
        Antoni.SendMessage("AllowPlayerToClick", false); // Prevent from clicking while Atoni rides the elevators
        Antoni.SendMessage("MoveToPosition", x); //Move Antoni to the elevator
        while (Antoni.transform.position.x != x)
        {
            yield return null;                  //Wait for Antoni to move to elevator
        }
        Debug.Log("Antoni arrived at the elevator");
        Antoni.SendMessage("AllowPlayerToClick", false);//When Antoni arrives at his destination he player is allowed to click again so we need to disable clicking again
        yield return new WaitForSeconds(1.0f); // Wait a second for no reason
        
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.002f); //Move elevator door in front of Antoni

        animatior.SetTrigger("CloseElevator"); //Start closing the elevator 
        FindObjectOfType<AudioManager>().Play("elevatorClose");

        yield return new WaitForSeconds(3.0f);  //Wait till elevator closes

        mainCamera.GetComponent<MainCameraScript>().MoveCameraAndAntoniBy(new Vector3(0, 174, 0)); //Start moving camera to next floor
        if (ElevatorNumber == 3)
        {
            GameObject.Find("GameSettings").SendMessage("GoToEndScreen"); //If it's the last room Go to end Screen
        }
        
        if (NextElevator==null)
        {
            Debug.Log("GameIsFinished-play outro"); //If there is no next elevator that means that it is the last floor and we need to finish the game
        }

        while (AntoniMovementController.AntoniArrivedAtNewFloor == false)
        {
            yield return null; //waint until camera arrived at next floor
        }
        AntoniMovementController.AntoniArrivedAtNewFloor = false; //Clear flag that sets when camera arrives at next floot
        Antoni.transform.position = new Vector3(Antoni.transform.position.x, Antoni.transform.position.y + 174, Antoni.transform.position.z); //Move Antoni to next floor
        if (NextElevator != null)
        {
            NextElevator.SendMessage("AntoniArrived"); //Tell next elevator to close 
            FindObjectOfType<AudioManager>().Play("elevatorClose");
            NextElevator.GetComponent<ElevatorEmptyRoomScript>().ElevatorNumber = ElevatorNumber + 1;
        }
        
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.002f); //Move elevator door behing Antoni (no need to do that if antoni will never return to floor that is previously visited)
        yield return null;
    }

    IEnumerator AntoniArrived()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.002f); //Move door in front of antoni
        animatior.SetTrigger("OpenElevator"); //Start opening the elevator     
        FindObjectOfType<AudioManager>().Play("elevatorDing");
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<AudioManager>().Play("elevatorOpen");
        yield return new WaitForSeconds(1.5f); //Wait for elevator to open
        Antoni.GetComponent<MovementController>().MovementEnabled = true;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.002f); //Move elevator door behind Antoni
        Debug.Log("Let Antoni walk freely"); //Allow Player to move antoni
        mainCamera.transform.GetChild(0).transform.gameObject.SetActive(false); //Allow player to click again
        yield return new WaitForSeconds(1.5f); //
        animatior.SetTrigger("CloseElevator"); //Close Elebator door after short delay  
        FindObjectOfType<AudioManager>().Play("elevatorClose");
    }


 
}  