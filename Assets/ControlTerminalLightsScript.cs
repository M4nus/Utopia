using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTerminalLightsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setOrangeActive()
    {
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }
    public void setBlueActive()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
}
