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
    public bool canWrite = false;
    public string fullText;            
    public GameObject note;
    public int index;
    [Range(0,3)]
    public int room = 1;
    private TextMesh _text;
    private bool _firstLetter = false;

    private void Start()
    {
        _text = note.GetComponent<TextMesh>();
        _text.text = "Press any buttons...";
        if(room == 0)
        {
            fullText = "... notes. To może coś napiszę. \nNie do końca wiem czego ode mnie chcą, \nmoże po prostu ich słuchać." 
                     + "\n\nPS Nie ma tu żadnych roślin, \nbrakuje mi Archiwum Botaniki.";
        }
        if(room == 1)
        {
            fullText = "To miejsce jest przygnębiające. \nW klasie rośliny zawsze były zaniedbane \nprzez panie sprzątaczki, " +
                       "\na ja nigdy nie mogłem się skupić przy tablicy. \nNotorycznie popełniałem " +
                       "jakiś błąd pod tą presją. \nDobrze, że tutaj nikogo nie ma.";
        }
        if(room == 2)
        {
            fullText = "Najbliższe są mi rośliny i ta ich piękna zieleń! \nChociaż nigdy " +
                       "nie podobało się to mym wychowawcom. \nOni woleli pomarańczowy. ";
        }
        if(room == 3)
        {
            fullText = "Obiad, w sumie to zgłodniałem. \nZazwyczaj jadam kaszę z fasolą, " +
                       "\nale ten makaron z sosem pomidorowym wygląda bardzo kusząco. " +
                       "\nMoże moja kochana Matylda mi taki \nprzygotuję jak coś zaoszczędze. ";
        }                                 
    }

    private void Update()
    {
        if(canWrite)
        {
            if(Input.anyKeyDown && !Input.GetMouseButtonDown(0))
            {
                ClearFirstText();
                if(_firstLetter)
                WriteText();
                WriteText();
                WriteText();                       
            }               
        }
    }

    void ClearFirstText()
    {
        if(!_firstLetter)
            _text.text = "";
        _firstLetter = true;
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
        FindObjectOfType<AudioManager>().Play("click2");    
        Debug.Log("NoteButton got clicked on! :D");
        this.transform.GetChild(0).gameObject.SetActive(true);
        canWrite = true;
        Debug.Log("Can write? " + canWrite);
    }

    private void OnDisable()
    {
        canWrite = false;
    }

    void WriteText()
    {
        if(isFinished())
        {
            index = _text.text.Length;
            _text.text += fullText[index];         
            index++;
            //CheckLineLength();                            
        }    
    }

    bool isFinished()
    {
        return _text.text.Length < fullText.Length;
    }

    void CheckLineLength()
    {
        if(_text.text.Length % 50 == 0)
        {                       
            _text.text += "\n";
        }
    }        
}
