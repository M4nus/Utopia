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
<<<<<<< HEAD
    public string sentence;
    [Range(0,2)]
    public int correction = 0;
    private TextMesh BoardWritingField;
    private int lines = 0;
    private bool isInteractable = false;
    private bool boardInteraction = false;   
    private string correctAnswer;
     
=======
    private TextMesh BoardWritingField;
    bool isInteractable = false;
    // Start is called before the first frame update
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a

    void Start()
    {
        BoardWritingField = transform.GetChild(1).GetComponent<TextMesh>();
<<<<<<< HEAD
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
=======
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a

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
<<<<<<< HEAD
=======
        boardInteraction = true;
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a
        Debug.Log("Chalk got clicked on! :D");
        if(isInteractable)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(MoveWriteOnBoard(this.transform.position.x));
<<<<<<< HEAD
        }                        
=======
        }                  
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a
    }

    IEnumerator MoveWriteOnBoard(float x)
    {
        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at Chalk");
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", true);
<<<<<<< HEAD
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
        CheckCorrection();
    }
=======
        yield return new WaitForSeconds(2.0f);
        BoardWritingField.text = "Zawsze będę posłuszny...";

    }

    void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0]) && boardInteraction)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            BoardWritingField.text += e.keyCode;
            if(Input.GetKeyDown(KeyCode.Return))
                boardInteraction = false;
        }
    }           
>>>>>>> 2e217b23170fec496182ff8e0c7107f9715fea7a
}
