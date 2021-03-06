﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public enum Lid { open,closed, opening, closing, wakeBlink, sleepBlink};

public class Eyelids : MonoBehaviour {
    GameObject player;
    GameObject topEyelid;
    RectTransform topEyelid_Rect;
    GameObject bottomEyelid;
    RectTransform bottomEyelid_Rect;

    Camera cam;
    BlurOptimized blur;
    SleepingAndWaking r_sleepingScript;

    public Lid eye = Lid.opening;
    private SleepState sleepState;

    float normal_Y_TopEyelid;
    float normal_Y_BottomEyelid;
    float max_Y_TopEyelid = 1000;
    float max_Y_BottomEyelid = -1400;

    float progress_TopEyelid;
    float progress_BottomEyelid;
    public static float progress = 0;
    float negProgress = 1;

    public float eyeOpenSpeed = 1;
    public float eyeOpenAmount = 10;
    
    public AnimationCurve blinkCurve;

    float blinkTimer = 0;
    public float blinkLength = 0.5f;

    public float blurSpeed;

    bool blurIsOn = true;

    public MouseLook r_mouseLook;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        topEyelid = GameObject.Find("TopEyelid");
        topEyelid_Rect = topEyelid.GetComponent<RectTransform>();
        bottomEyelid = GameObject.Find("BottomEyelid");
        bottomEyelid_Rect = bottomEyelid.GetComponent<RectTransform>();

        r_mouseLook = player.GetComponent<FirstPersonController>().m_MouseLook;

        cam = Camera.main;
        blur = cam.GetComponent<BlurOptimized>();

        r_sleepingScript = GameObject.Find("Player").GetComponent<SleepingAndWaking>();

        normal_Y_TopEyelid = topEyelid_Rect.anchoredPosition.y;
        normal_Y_BottomEyelid = bottomEyelid_Rect.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update () {
        sleepState = r_sleepingScript.sleepState;

        switch (eye)
        {
            case Lid.opening:
                if (progress < 1)
                {
                    MoveEyelid(topEyelid_Rect, eyeOpenSpeed * 2);
                    MoveEyelid(bottomEyelid_Rect, -eyeOpenSpeed);

                    blur.blurSize = negProgress * blurSpeed;
                    r_mouseLook.EyelidSlow(false, 1);
                }
                else
                {
                    eye = Lid.wakeBlink;
                }
                break;

            case Lid.wakeBlink:
                if (Blink(false))
                    eye = Lid.open;
                break;

            case Lid.open:
                if (sleepState == SleepState.lyingGoingToSleep && !blurIsOn)
                    eye = Lid.sleepBlink;
                break;

            case Lid.sleepBlink:
                if (Blink(true))
                    eye = Lid.closing;
                break;

            case Lid.closing:
                if (sleepState != SleepState.lyingGoingToSleep)
                {
                    eye = Lid.opening;
                    r_mouseLook.EyelidSlow(false, 1);
                }

                if (progress > 0.2f)
                {
                    MoveEyelid(topEyelid_Rect, -eyeOpenSpeed * 1.5f);
                    MoveEyelid(bottomEyelid_Rect, eyeOpenSpeed);

                    blur.blurSize = negProgress * blurSpeed;
                    r_mouseLook.EyelidSlow(true, progress);
                }
                else
                {
                    eye = Lid.closed;
                    r_mouseLook.EyelidSlow(false, 1);
                    //CLOSED EYELIDS POINT
                    //the PC is asleep. I need to send a message out.

                    r_sleepingScript.Sleep();
                }
                break;

            case Lid.closed:
                //this causes it to wait for sleeping script
                if (sleepState == SleepState.lyingAwake)
                    eye = Lid.opening;
                break;

        }
        progress_TopEyelid = topEyelid_Rect.anchoredPosition.y / (max_Y_TopEyelid - normal_Y_TopEyelid);
        progress_BottomEyelid = bottomEyelid_Rect.anchoredPosition.y / (max_Y_BottomEyelid - normal_Y_BottomEyelid);
        progress = (progress_BottomEyelid + progress_TopEyelid) / 2;

        negProgress = Mathf.Abs( progress - 1);
    }

    public void OpenEyelids()
    {
        eye = Lid.opening;
    }

    /// <param name="blurOnOff">
    /// Values of true/false turn blur on/off, respectively.
    /// </param>
    bool Blink(bool blurOnOff)
    {
        float delta = 0;
        delta = blinkCurve.Evaluate((blinkTimer/blinkLength));

        MoveEyelidScale(topEyelid_Rect, normal_Y_TopEyelid, max_Y_TopEyelid, delta);
        MoveEyelidScale(bottomEyelid_Rect, normal_Y_BottomEyelid, max_Y_BottomEyelid, delta);

        if(blinkTimer/blinkLength> 0.5f)
        {
            blur.enabled = blurOnOff;
            blurIsOn = blurOnOff;
        }
        
        if (blinkTimer / blinkLength > 1)
        {
            blinkTimer = 0;
            return true;
        }

        blinkTimer += Time.deltaTime;

        return false;
    }
    
    public void CloseEyelids()
    {
        eye = Lid.sleepBlink;
    }

    void MoveEyelid(RectTransform eyelid_Rect, float moveSpeed)
    {
        eyelid_Rect.anchoredPosition = new Vector2(eyelid_Rect.anchoredPosition.x, eyelid_Rect.anchoredPosition.y + moveSpeed);
    }

    void MoveEyelidScale(RectTransform eyelid_Rect, float norm_Y, float max_Y, float scale)
    {
        eyelid_Rect.anchoredPosition = new Vector2(eyelid_Rect.anchoredPosition.x, (max_Y - norm_Y) * scale);
    }
}
