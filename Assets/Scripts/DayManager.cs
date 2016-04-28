using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour {

    Door doorScript;
    Tray trayScript;

    public bool isFeedingTime = false;

    float feedingTimer = 0;
    public float maxFeedingTimer = 10;

    public float trayPushTime = 1;

	// Use this for initialization
	void Start ()
    {
        doorScript = GameObject.Find("Door").GetComponent<Door>();
        trayScript = GameObject.Find("Tray").GetComponent<Tray>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FeedingTime();
        }

        if(isFeedingTime)
        {
            feedingTimer += Time.deltaTime;

            doorScript.OpenEyeSlot();
            doorScript.OpenTraySlot();

            if (feedingTimer > trayPushTime)
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
            doorScript.CloseTraySlot();
        }

    }

    void FeedingTime()
    {
        isFeedingTime = true;
    }
}
