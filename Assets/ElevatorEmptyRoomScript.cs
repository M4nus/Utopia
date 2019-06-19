using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEmptyRoomScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public GameObject Antoni;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
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
        StartCoroutine(MoveToPossition(this.transform.position.x));
    }


    IEnumerator MoveToPossition(float x)
    {
        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at the elevator");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Antoni wants to ride the elevator");
        mainCamera.GetComponent<MainCameraScript>().MoveCameraBy(new Vector3(0, 174, 0));
        yield return null;
    }
}