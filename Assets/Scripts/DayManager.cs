using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour {

    Door _door;
    Tray _tray;

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
    
    // Use this for initialization
    void Start ()
    {
        //going to need to make sure these don't break when cell is unloaded
        _door = GameObject.Find("Door").GetComponent<Door>();
        _tray = GameObject.Find("Tray").GetComponent<Tray>();
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
    }
    bool foodBeenServed = false;
    void FeedingTime()
    {
        //this script is a bit shit, it shoudl just bbe a trigger and then it's handled by everything else
        feedingTimer += Time.deltaTime;

        _door.OpenEyeSlot();
        _door.OpenTraySlot();

        if (feedingTimer > trayPushTime && !foodBeenServed)
        {
            foodBeenServed = true;
            _tray.PushInside();
        }

        if (feedingTimer > maxFeedingTimer)
        {
            feedingTimer = 0;
            hasBeenFed = true;
            foodBeenServed = false;
            _door.CloseEyeSlot();
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
