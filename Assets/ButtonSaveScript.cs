using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonSaveScript : MonoBehaviour
{
    public Sprite SpriteNormal;
    public Sprite SpriteClicked;
    public bool isClicked = false;
    Button ButtonComponent;
    // Start is called before the first frame update
    void Start()
    {
        ButtonComponent = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClicked()
    {
        isClicked = !isClicked;
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (isClicked)
        {
            ButtonComponent.image.sprite = SpriteClicked;
        }
        else
        {
            ButtonComponent.image.sprite = SpriteNormal;
        }
        
    }
}
