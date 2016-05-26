using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DreamController))]
public class DayManager : MonoBehaviour {

    Door _door;
    Tray _tray;

    public int dayCount;
    [Space(10)]
    public bool fast = false;
    [SerializeField]
    public static float dayLengthSeconds = 300;
    public float currentSeconds = 0;
    public float percentageOfDay;
    public static float oneThirdDaySeconds;
    [Space(10)]
    [Header("Event Trigger Times")]
    public float feedingTime = 0.15f;

    bool hasBeenFed = false;

    float feedingTimer = 0;
    float maxFeedingTimer = 10;
    float trayPushTime = 1;
    
    void Awake()
    {
        if (fast)
            dayLengthSeconds = 30;
    }
    
    // Use this for initialization
    void Start ()
    {
        _door = GameObject.Find("Door").GetComponent<Door>();
        _tray = GameObject.Find("Tray").GetComponent<Tray>();

        oneThirdDaySeconds = dayLengthSeconds / 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (DreamController.loadedScene == Scenes.Cell)
            UpdateTime();
        
        if(percentageOfDay > feedingTime && !hasBeenFed)
        {
            FeedingTime();
        }

        if (currentSeconds > dayLengthSeconds)
        {
            EndOfDay();
        }

        //HERE IS INPUT
        #region input
        if (Input.GetKeyDown(KeyCode.Minus)) dayLengthSeconds *= 2;
        if (Input.GetKeyDown(KeyCode.Equals)) dayLengthSeconds *= 0.5f;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Application.CaptureScreenshot("Screenshot" + Time.time + ".png", 5);
        }
        #endregion
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

    void UpdateTime()
    {
        currentSeconds += Time.deltaTime;
        percentageOfDay = currentSeconds / dayLengthSeconds;
    }


    public void UpdateTimeWhileSleeping()
    {
        currentSeconds += oneThirdDaySeconds;
    }

    void EndOfDay()
    {
        dayCount++;
        currentSeconds = 0;
        hasBeenFed = false;
    }
}
