using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTerminalLightsScript : MonoBehaviour
{              
    public void setOrangeActive()
    {
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }
    public void setBlueActive()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
}
