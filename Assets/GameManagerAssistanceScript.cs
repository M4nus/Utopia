using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAssistanceScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager _gm;
    public string axis;
    public float distToMove;
    public int sceneToLoad;
    void Start()
    {
        _gm = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startMoveCamera()
    {
        StartCoroutine(_gm.MoveCamera(axis, distToMove, sceneToLoad));
    }
}
