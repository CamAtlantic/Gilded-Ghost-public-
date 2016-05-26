using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    public AnimationCurve peck;
    float peckTimer;
    public float peckTimerMax;

    public AnimationCurve bob;
    float bobTimer;
    public float bobTimerMax;

    bool wait;
    float waitTimer;
    float waitTimerMax;

    GameObject body;
    Quaternion defaultRot;
    void Awake()
    {
        body = transform.GetChild(0).gameObject;
        defaultRot = body.transform.localRotation;
    }

	// Update is called once per frame
	void Update () {
        //need to add the value from the curve to the current rotation

        if (!wait)
        {
            Quaternion newRot = Quaternion.Euler(peck.Evaluate(peckTimer / peckTimerMax), 0, 0) * defaultRot;
            body.transform.localRotation = newRot;

            peckTimer += Time.deltaTime;
            if (peckTimer > peckTimerMax)
            {
                peckTimer = 0;
                wait = true;
                waitTimerMax = (int)Random.Range(0, 10) / 2;
                print(waitTimerMax);
                bobTimer = 0;
            }
        }

        else
        {
            waitTimer += Time.deltaTime;
            if(waitTimer> waitTimerMax)
            {
                waitTimer = 0;
                wait = false;
            }

          /*  Quaternion newRot = Quaternion.Euler(bob.Evaluate(bobTimer / bobTimer), 0, 0) * defaultRot;
            body.transform.localRotation = newRot;
            bobTimer += Time.deltaTime;
            if(bobTimer> bobTimerMax)
            {
                bobTimer = 0;
            }*/
        }
    }
}
