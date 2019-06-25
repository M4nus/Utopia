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
            fullText = "Wprowadzenie chyba";
        }
        if(room == 1)
        {
            fullText = "Wciąż pamiętam ten dzień jak w szkole zostałem zrugany przy popełnieniu literówki. Nigdy więcej…";
        }
        if(room == 2)
        {
            fullText = "Nigdy nie lubiłem pomarańczowego, zawsze był dla mnie jak odcień szarości.";
        }
        if(room == 3)
        {
            fullText = "To chyba pokój ze sklepem";
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
            CheckLineLength();                            
        }    
    }

    bool isFinished()
    {
        return _text.text.Length < fullText.Length;
    }

    void CheckLineLength()
    {
        if(_text.text.Length % 49 == 0)
        {                       
            _text.text += "\n";
        }
    }        
}
