using System.Collections;
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

    public GameObject Antoni;
    Camera mainCamera;
    bool alreadyDone = false;
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
        if (alreadyDone == false)
        {
            chalk.SendMessage("ActivateChalk");
            alreadyDone = true;
        }
        
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


    public void AllowPlayerToClick(bool isAllowed)
    {
        mainCamera.transform.GetChild(0).gameObject.SetActive(!isAllowed); //Enable or disable raycast blocker
    }

    public void AllowPlayerToMove(bool isAllowed)
    {
        Antoni.SendMessage("AllowPlayerToMove", isAllowed); //Enable or disable Player movement
    }
}
