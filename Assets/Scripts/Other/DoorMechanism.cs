using UnityEngine;    


public class DoorMechanism : MonoBehaviour
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
                StartCoroutine(_sm.MoveCamera("x", 5.5f));             
            }
        }
    }

}
