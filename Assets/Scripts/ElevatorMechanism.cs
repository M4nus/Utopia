using UnityEngine;

public class ElevatorMechanism : MonoBehaviour
{
    GameManager _sm;
    GameObject _game;

    private void Start()
    {
        _game = GameObject.Find("[GAME]");
        _sm = _game.GetComponent<GameManager>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Antoni"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //StartCoroutine(_sm.MoveCamera("y", 3f)); //Disabled because interaction is moved to button from canvas
            }
        }
    }

}
