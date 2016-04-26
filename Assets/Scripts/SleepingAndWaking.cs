using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum SleepState { sleeping, lyingAwake, goingUp, standing, goingDown };

[Serializable]
public class SleepingAndWaking {

    public SleepState sleepState;

    public int facingTriggerDegree = 70;
    public float standingSleepingSpeed = 0.2f;

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
                player.rotation = Quaternion.Lerp(player.rotation, Quaternion.Euler(new Vector3(0, -90, 0)), standingSleepingSpeed);
                player.position = Vector3.Lerp(player.position, new Vector3(-0.1f, player.position.y, player.position.z), standingSleepingSpeed);

                if(Quaternion.Angle(player.rotation,Quaternion.Euler(Vector3.back)) < 1)
                {
                    
                    sleepState = SleepState.standing;
                    fpsController.ResetMouseLook();
                }
                return true;
                

            case SleepState.standing:

                break;

            case SleepState.goingDown:
                return true;
                
        }

        return false;
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
        Debug.Log(leftAngle + " " + forwardAngle);

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


}