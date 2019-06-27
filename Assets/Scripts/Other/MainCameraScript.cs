using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetCameraPosition;
    Vector3 smoothDampVelocity;
    public GameObject Antioni;
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }
    public void MoveCameraAndAntoniBy(Vector3 changeInPositioin)
    {
        smoothDampVelocity = new Vector3(0, 0, 0);
        targetCameraPosition = (transform.position + changeInPositioin);
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while(targetCameraPosition.y > transform.position.y + 0.1)
        {
            //FindObjectOfType<AudioManager>().Play("elevatorNoise");
            transform.position = Vector3.SmoothDamp(transform.position, targetCameraPosition, ref smoothDampVelocity, 0.5f, 20.0f);
            yield return null;
        }
        Antioni.GetComponent<MovementController>().AntoniArrivedAtNewFloor = true;
        Debug.Log("Antoni finished riding the elevator");

    }
}
