using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRoomChalkScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public GameObject Antoni;
    public GameObject Elevator;
    public string sentence;
    [Range(0,2)]
    public int correction = 0;
    private TextMesh BoardWritingField;

    private int lines = 0;
    private bool isInteractable = false;
    private bool boardInteraction = false;   
    private string correctAnswer;


    void Start()
    {
        BoardWritingField = transform.GetChild(1).GetComponent<TextMesh>();
        sentence = "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA";
        correctAnswer = "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA";/* +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n" +
                        "BŁĄD, AKCEPTACJA, DEKLARACJA, ADAPTACJA, REFORMACJA\n";   */
    }        

    void OnMouseEnter()
    {
        if (isInteractable)
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
        Debug.Log("Chalk got clicked on! :D");
        if(isInteractable)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(MoveWriteOnBoard(this.transform.position.x));
        }                        
    }

    IEnumerator MoveWriteOnBoard(float x)
    {

        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at Chalk");
        Antoni.SendMessage("AllowPlayerToClick", false);
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", true);
        BoardWritingField.text = sentence + " ";
        boardInteraction = true;    
    }                                             


    void OnGUI()
    {
        if(boardInteraction)
        {
            CheckLineLength();
            Event e = Event.current;
            if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.A)
                BoardWritingField.text += "Ą";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.C)
                BoardWritingField.text += "ć";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.E)
                BoardWritingField.text += "Ę";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.L)
                BoardWritingField.text += "Ł";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.O)
                BoardWritingField.text += "Ó";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.S)
                BoardWritingField.text += "Ś";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Z)
                BoardWritingField.text += "Ż";
            else if(e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.X)
                BoardWritingField.text += "Ź";
            else if(e.type == EventType.KeyDown && e.keyCode.ToString().Length <= 1)
            {
                BoardWritingField.text += e.keyCode;
            }
            if(e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
                BoardWritingField.text += " ";
            if(e.type == EventType.KeyDown && e.keyCode == KeyCode.Comma)
                BoardWritingField.text += ",";
            if(e.type == EventType.KeyDown && e.keyCode == KeyCode.Backspace)
            {
                BoardWritingField.text = BoardWritingField.text.Remove(BoardWritingField.text.Length - 1);     
            }
            if(e.type == EventType.KeyDown && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.Escape))
            {
                DisableInteraction();
            }    
        }     
    }

    void CheckLineLength()
    {         
        if(BoardWritingField.text.Length % (sentence.Length + 1) == 0)
        {
            Debug.Log(lines);       
            BoardWritingField.text += "\n";
            lines++;
            if(lines == 2)
                DisableInteraction();    
        }
    }

    void CheckCorrection()
    {
        if(string.Compare(BoardWritingField.text, correctAnswer) == 0)
        {
            correction = 1;
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Incorrect");
            Debug.Log("Correct: \n" + correctAnswer);
            Debug.Log("Your: \n" + BoardWritingField.text);
        }
        
    }

    public void ActivateChalk()
    {
        isInteractable = true;
        this.transform.GetChild(0).gameObject.SetActive(true);
    }


    public void DisableInteraction()
    {
        isInteractable = false;
        boardInteraction = false;
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", false);
        Antoni.SendMessage("AllowPlayerToClick", true); // <--- Add this line when player fininished writing on board and is free to walk and interact with other objects
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", false);
        Antoni.SendMessage("AllowPlayerToMove", true);
        Elevator.SendMessage("OpenElevator");
        CheckCorrection();
    }

}
