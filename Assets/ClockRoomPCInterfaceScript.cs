using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ClockRoomPCInterfaceScript : MonoBehaviour
{
    public int hours = 0;
    public int minutes = 0;
    public int seconds = 0;
    public Boolean clockRunning = false;
    public DateTime roomTime = new DateTime(1997, 5, 12, 3, 3, 3);
    public DateTime targetClockTime;
    public Text clockText;
    public GameObject HourArrow;
    public GameObject MinuteArrow;
    public Boolean tamperedWithTime = false;
    // Start is called before the first frame update
    void Start()
    {
        StartClock();

    }

    // Update is called once per frame
    void Update()
    {
        updateClockTime();
    }

    public void ToogleInterfaceActive()
    {
        this.gameObject.SetActive(!this.gameObject.active);
    }

    public void StartClock()
    {
        roomTime = roomTime.AddHours(UnityEngine.Random.Range(0, 23));
        roomTime = roomTime.AddMinutes(UnityEngine.Random.Range(0, 59));
        roomTime = roomTime.AddSeconds(UnityEngine.Random.Range(0, 59));
        targetClockTime = roomTime.AddMinutes(2);
        clockText.text = roomTime.Hour + ":" + roomTime.Minute + ":" + roomTime.Second;
        StartCoroutine(clockTimeIncrease());
    }
    public IEnumerator clockTimeIncrease()
    {
        clockRunning = true;
        while (true)
        {
            roomTime = roomTime.AddSeconds(1);
            setArrowsRotation();
            checkForRoomCompletion();
            yield return new WaitForSeconds(1f);
        }
    }
    public void updateClockTime()
    {
        clockText.text = "";
        if (roomTime.Hour < 10) { clockText.text += "0" + roomTime.Hour; } else { clockText.text += roomTime.Hour; }
        clockText.text += ":";
        if (roomTime.Minute < 10) { clockText.text += "0" + roomTime.Minute; } else { clockText.text += roomTime.Minute; }
        clockText.text += ":";
        if (roomTime.Second < 10) { clockText.text += "0" + roomTime.Second; } else { clockText.text += roomTime.Second; }
    }
    public void setArrowsRotation()
    {
        HourArrow.transform.eulerAngles = new Vector3(0, 0, -30.0f * ((float)targetClockTime.Hour + (float)targetClockTime.Minute / 60.0f));
        MinuteArrow.transform.eulerAngles= new Vector3(0, 0, -6 * (targetClockTime.Hour + UnityEngine.Random.Range(0,2) + targetClockTime.Second / 60));
    }

    public void checkForRoomCompletion()
    {
        if (roomTime >= targetClockTime)
        {
            if (tamperedWithTime)
            {
                Debug.Log("Room completed - DID NOT comply with the Rules");
            }
            else
            {
                {
                    Debug.Log("Room completed - DID comply with the Rules");
                }
            }
        }
    }

    public void increaseTime()
    {
        tamperedWithTime = true;
        roomTime = roomTime.AddSeconds(15);
    }
    public void decreaseTime()
    {
        tamperedWithTime = true;
        roomTime = roomTime.AddSeconds(-15);
    }

}
