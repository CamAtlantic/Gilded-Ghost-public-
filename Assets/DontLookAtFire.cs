﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DontLookAtFire : DreamTrigger {

    Camera cam;

    public float lookAtFireTriggerAngle = 0.05f;
    public float slowViewTriggerAngle = 90;

    public float rotateAmountNormal = 10;
    public float rotateAmountBurst = 100;
    private float rotateAmount = 10;
    
    MouseLook r_mouseLook;

    public float lookAtFireTimer;
    public float lookAtFireTimerMax = 10;

    public float map_AngleXY;
    public float abs_Sum;

    public bool lookingAtFire;

    Rotate[] flameArray;

    public override void Awake()
    {
        base.Awake();
        tag = "Untagged";
        cam = Camera.main;
        r_mouseLook = player.GetComponent<FirstPersonController>().m_MouseLook;
        flameArray = GetComponentsInChildren<Rotate>();
    }

    public override void Update()
    {
        base.Update();
        #region Camera Math
        Vector3 dir = transform.position - player.transform.position;
        float angleY = ArcFunctions.AngleHalf(dir, player.transform.forward, Vector3.up);
        float abs_AngleY = Mathf.Abs(angleY);
        float map_AngleY = map(abs_AngleY, 0, slowViewTriggerAngle, 1, 0);

        float anglePlayerX = ArcFunctions.AngleHalf(dir, cam.transform.forward, player.transform.right);
        float abs_AnglePlayerX = Mathf.Abs(anglePlayerX);
        float map_AnglePlayerX = map(abs_AnglePlayerX, 0, slowViewTriggerAngle, 1, 0);

        map_AngleXY = map_AngleY * map_AnglePlayerX;
        abs_Sum = abs_AnglePlayerX + abs_AnglePlayerX + abs_AngleY;
        #endregion

        #region Annoying Camera
        //player
        if (r_sleepingAndWaking.sleepState == SleepState.standing && abs_AngleY < slowViewTriggerAngle)
        {
            float y_rotate_Amount = map_AngleXY * map_AngleY * rotateAmount * Time.deltaTime;
            if (angleY > 0)
                r_mouseLook.TriggerFirePlayerRotate(y_rotate_Amount);
            else
                r_mouseLook.TriggerFirePlayerRotate(-y_rotate_Amount);

            if (abs_AnglePlayerX < slowViewTriggerAngle)
            {
                float x_rotate_Amount = map_AngleXY * map_AnglePlayerX * rotateAmount * Time.deltaTime;
                if (anglePlayerX > 0)
                    r_mouseLook.TriggerFireCameraRotate(x_rotate_Amount);
                else
                    r_mouseLook.TriggerFireCameraRotate(-x_rotate_Amount);
            }

        }
        #endregion

        //-----------LookAt trigger

        if(abs_Sum < lookAtFireTriggerAngle)
        {
            lookingAtFire = true;
            lookAtFireTimer += Time.deltaTime;
            GetComponentInChildren<Light>().intensity += lookAtFireTimer * lookAtFireTimer;
            foreach(Rotate rot in flameArray)
            {
                rot.speed += lookAtFireTimer / 100;
            }
        }
        else
        {
            lookingAtFire = false;
            lookAtFireTimer -= Time.deltaTime / 2;
            if (lookAtFireTimer < 0)
                lookAtFireTimer = 0;
            foreach (Rotate rot in flameArray)
            {
                rot.speed -= lookAtFireTimer / 100;
            }
        }

        if(lookAtFireTimer > lookAtFireTimerMax)
        {
            DreamTriggerEffect();
        }
      
        rotateAmount = Mathf.Lerp(rotateAmount, 10, 0.4f);
        float newScaleNumber = 0.5f + (lookAtFireTimer / lookAtFireTimerMax);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, newScaleNumber, 1), 0.2f);
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_dreamController.fire_fire = true;
        TriggerLieDown();
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
