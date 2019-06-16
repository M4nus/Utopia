using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonExitScript : MonoBehaviour
{
    public Sprite SpriteNormal;
    public Sprite SpriteClicked;
    public bool isClicked = false;
    public bool isClock = false;
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
        if (isClock == false)
        {
            this.gameObject.GetComponentInParent<GenericPCInterfaceScript>().ToogleInterfaceActive();
        }
        else
        {
            this.gameObject.GetComponentInParent<ClockRoomPCInterfaceScript>().ToogleInterfaceActive();

        }


        isClicked = false;
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
