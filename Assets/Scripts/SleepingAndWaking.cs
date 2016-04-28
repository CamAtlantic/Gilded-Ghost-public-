using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum SleepState { sleeping, lyingAwake, goingUp, standing, goingDown };

[Serializable]
public class SleepingAndWaking : MonoBehaviour{

    public SleepState sleepState;

    public int facingTriggerDegree = 70;
    public float standingSleepingSpeed = 0.2f;

    Vector3 standUpPosition = new Vector3(-0.2f, 0.921f, -0.65f);
    Vector3 standUpRotation = new Vector3(0, -90, 0);

    Vector3 sleepingPosition = new Vector3(0.82f, 0.75f, -0.67f);
    Vector3 sleepingRotation = new Vector3(275, 0, 0);

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public bool UpdateSleepState(Transform player, FirstPersonController fpsController)
    {
        switch (sleepState)
        {
            case SleepState.sleeping:
                //open eyes, move to lyingAwake
                break;

            case SleepState.lyingAwake:
                if(CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.goingUp:
                if(PlayerLerp(player,fpsController,standUpPosition,standUpRotation))
                {
                    sleepState = SleepState.standing;
                    fpsController.ResetMouseLook();
                }
                return true;
                

            case SleepState.standing:
                controller.enabled = true;
                break;

            case SleepState.goingDown:
                if (PlayerLerp(player, fpsController, sleepingPosition, sleepingRotation))
                {
                    sleepState = SleepState.lyingAwake;
                    fpsController.ResetMouseLook();
                }
                return true;
                
        }

        return false;
    }

    public void GoToSleep()
    {
        sleepState = SleepState.goingDown;
    }

    /// <summary>
    /// Returns true if player is facing away from the bed.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CheckPlayerFacing(Transform player)
    {
        float leftAngle = ArcFunctions.AngleHalf(Vector3.up, player.transform.forward, Vector3.forward);
        float forwardAngle = ArcFunctions.AngleHalf(Vector3.up, Camera.main.transform.forward, Vector3.left);
        //Debug.Log(leftAngle + " " + forwardAngle);

        if ( leftAngle > facingTriggerDegree ||
             forwardAngle < -facingTriggerDegree)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool PlayerLerp(Transform player, FirstPersonController fpsController, Vector3 targetPosition, Vector3 targetRotation)
    {
        player.rotation = Quaternion.Lerp(player.rotation, Quaternion.Euler(targetRotation), standingSleepingSpeed);
        player.position = Vector3.Lerp(player.position, targetPosition, standingSleepingSpeed);

        if (Quaternion.Angle(player.rotation, Quaternion.Euler(targetRotation)) < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}