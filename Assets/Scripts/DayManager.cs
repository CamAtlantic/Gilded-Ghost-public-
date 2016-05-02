using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour {

    Door doorScript;
    Tray trayScript;

    int dayCount;
    [Space(10)]
    public float secondsInDay = 60;
    public float currentSeconds = 0;
    public float percentageOfDay;
    [Space(10)]
    [Header("Event Trigger Times")]
    public float feedingTime = 0.15f;

    bool hasBeenFed = false;

    float feedingTimer = 0;
    float maxFeedingTimer = 10;
    float trayPushTime = 1;

    public bool daytimeDebug = false;

    // Use this for initialization
    void Start ()
    {
        doorScript = GameObject.Find("Door").GetComponent<Door>();
        trayScript = GameObject.Find("Tray").GetComponent<Tray>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckTime();

        //INPUT
        if (Input.GetKeyDown(KeyCode.Minus)) secondsInDay *= 2;
        if (Input.GetKeyDown(KeyCode.Equals)) secondsInDay *= 0.5f;
        
        if(percentageOfDay > feedingTime && !hasBeenFed)
        {
            FeedingTime();
        }

        if(daytimeDebug)
        {
            if (percentageOfDay > 0.5f)
                secondsInDay = 10;
            else
                secondsInDay = 300;
        }
    }

    void FeedingTime()
    {
        feedingTimer += Time.deltaTime;

        doorScript.OpenEyeSlot();
        doorScript.OpenTraySlot();

        trayScript.PushInside();

        if (feedingTimer > maxFeedingTimer)
        {
            feedingTimer = 0;
            hasBeenFed = true;
            doorScript.CloseEyeSlot();
        }
    }

    void CheckTime()
    {
        currentSeconds += Time.deltaTime;
        percentageOfDay = currentSeconds / secondsInDay;
        if (currentSeconds > secondsInDay)
        {
            EndOfDay();
        }
    }

    void EndOfDay()
    {
        dayCount++;
        currentSeconds = 0;
        hasBeenFed = false;
    }
}
