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
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        animatior = GetComponent<Animator>();
        AntoniMovementController = Antoni.GetComponent<MovementController>();
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
        yield return new WaitForSeconds(1.0f);
        ElevatorOpen = true;
    }

    IEnumerator GoIntoElevator(float x)
    {
        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at the elevator");
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Antoni wants to ride the elevator");
        //animatior.SetTrigger("OpenElevator");
        //yield return new WaitForSeconds(3.0f);
        
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.002f);

        animatior.SetTrigger("CloseElevator");

        yield return new WaitForSeconds(3.0f);

        mainCamera.GetComponent<MainCameraScript>().MoveCameraAndAntoniBy(new Vector3(0, 174, 0));

        while (AntoniMovementController.AntoniArrivedAtNewFloor == false)
        {
            yield return null;
        }
        AntoniMovementController.AntoniArrivedAtNewFloor = false;
        Antoni.transform.position = new Vector3(Antoni.transform.position.x, Antoni.transform.position.y + 174, Antoni.transform.position.z);
        if (NextElevator != null)
        {
            NextElevator.SendMessage("AntoniArrived");
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.002f);
        yield return null;
    }

    IEnumerator AntoniArrived()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.002f);
        animatior.SetTrigger("OpenElevator");
        yield return new WaitForSeconds(1.5f);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.002f);
        Debug.Log("Let Antoni walk freely");
        yield return new WaitForSeconds(1.5f);
        animatior.SetTrigger("CloseElevator");
    }
}