using System.Collections;
using System.Collections.Generic;
using UnityEngine;        
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] GameObject _player;

    int xSign;
    int ySign;
    public int actScene;   
    public int sceneOffset;
    public int collumns;
    public int rows;
    static public Vector2 lastPos = new Vector2(0, -0.89f);
                                                  

    void Start ()
    {
        xSign = 1;
        ySign = 1;
        collumns = 3;
        rows = 3;
        actScene = SceneManager.GetActiveScene().buildIndex;
        _player.transform.position = lastPos;
	}   

    public IEnumerator MoveCamera(string axis, float distToMove)
    {   
        float pos = 0; // Camera move increaser  
        SetValues(axis);      
                                           
        while(distToMove > Mathf.Abs(pos))
        {
            Vector3 move = (axis == "x") ? new Vector3(xSign * pos, 0f, -5f) : new Vector3(0f, ySign * pos, -5f);
            _camera.position = move;
            pos += Time.deltaTime;                                                                      
            yield return null;
        }
        ChangeScene(actScene + sceneOffset);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
        _player.SetActive(true);
    }

    public void CheckBoundaries()
    {
        if((actScene % rows == 1) && (xSign == -1))        // Left boundary
            xSign = 1;
        if((actScene % rows == 0) && (xSign == 1))    // Right boundary
            xSign = -1;
        if((actScene < 4) && (ySign == -1))        // Bottom boundary
            ySign = 1;
        if((actScene > 6) && (xSign == 1))         // Top boundary
            ySign = -1;
    }

    public void SetValues(string axis)
    {
        _player.SetActive(false);                                       // Disable player on scene change     
        xSign = (Random.Range(0, 2) % 2 == 0) ? -1 : 1;
        ySign = (Random.Range(0, 2) % 2 == 0) ? -1 : 1;
        CheckBoundaries();                                              // Checking boundaries
        sceneOffset = (axis == "x") ? 1 * xSign : 3 * ySign;    // If left/right then scene by 1 
        // Setting next pos whether I used eleator or corridor
        lastPos = (axis == "x") ? new Vector2(-2.16f, _player.transform.position.y)
                                : new Vector2(-1f, _player.transform.position.y);
    }    
}
                                                                                                        