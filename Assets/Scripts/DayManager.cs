﻿using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour {

    Door doorScript;
    Tray trayScript;

    public bool isFeedingTime = false;

    float feedingTimer = 0;
    public float maxFeedingTimer = 10;
    public float trayPushTime = 1;

    int dayCount;
    float dayTimer;
    /// <summary>
    /// Length of one day, in seconds.
    /// </summary>
    float dayLength = 300;

	// Use this for initialization
	void Start ()
    {
        doorScript = GameObject.Find("Door").GetComponent<Door>();
        trayScript = GameObject.Find("Tray").GetComponent<Tray>();
    }
	
	// Update is called once per frame
	void Update () {
        dayTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FeedingTime();
        }

        if(isFeedingTime)
        {
            feedingTimer += Time.deltaTime;

            doorScript.OpenEyeSlot();
            doorScript.OpenTraySlot();

            //should mean this triggers for a quarter second
            if (feedingTimer > trayPushTime && feedingTimer < trayPushTime + 0.25f)
            {
                trayScript.PushInside();
            }

            if(feedingTimer > maxFeedingTimer)
            {
                feedingTimer = 0;
                isFeedingTime = false;
            }
        }
        else
        {
            doorScript.CloseEyeSlot();
        }

        if (dayTimer > dayLength)
            EndOfDay();
    }

    void EndOfDay()
    {
        dayTimer = 0;
        dayCount++;
    }

    void FeedingTime()
    {
        isFeedingTime = true;
    }
}
