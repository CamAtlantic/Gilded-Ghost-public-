using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum SleepState { lyingGoingToSleep, lyingAwake, goingUp, standing, goingDown, asleep };

[Serializable]
public class SleepingAndWaking : MonoBehaviour{
    DreamController r_dreamController;
    CharacterController _controller;

    public SleepState sleepState;

    public int facingTriggerDegree = 70;
    //public float standingSleepingSpeed = 0.03f;

    public Transform cellSleepTransform;
    Transform cellStandingTransform;

    Transform dreamTransform;

    public Vector3 sleepingPosition;
    public Vector3 sleepingRotation;

    void Awake()
    {
        r_dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        cellSleepTransform = GameObject.Find("CellSleepTransform").transform;
        cellStandingTransform = GameObject.Find("CellStandingTransform").transform;
    }

    public bool UpdateSleepState(Transform player, FirstPersonController fpsController)
    {
        switch (sleepState)
        {
            case SleepState.lyingGoingToSleep:

                if (r_dreamController.loadedScene == Scenes.Cell && CheckPlayerFacing(player) )
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.lyingAwake:
                r_dreamController.SetActiveScene();

                if (CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.goingUp:
                Vector3 standUpPosition;
                Vector3 standUpRotation;

                if (r_dreamController.loadedScene == Scenes.Cell)
                {
                    standUpPosition = cellStandingTransform.position;
                    standUpRotation = cellStandingTransform.rotation.eulerAngles;
                }
                else if (r_dreamController.loadedScene != Scenes.Cell)
                {
                    standUpPosition = dreamTransform.position;
                    standUpRotation = dreamTransform.rotation.eulerAngles;
                }
                else
                {
                    standUpPosition = transform.position;
                    standUpRotation = Vector3.zero;
                }

                if (PlayerLerp(player, fpsController, standUpPosition, standUpRotation))
                {
                    sleepState = SleepState.standing;
                    fpsController.ResetMouseLook();
                }
                return true;
                

            case SleepState.standing:
                _controller.enabled = true;
                break;

            case SleepState.goingDown:
                
                if (r_dreamController.loadedScene == Scenes.Cell)
                {
                    sleepingPosition = cellSleepTransform.position;
                    sleepingRotation = cellSleepTransform.rotation.eulerAngles;
                }
                
                if (PlayerLerp(player, fpsController, sleepingPosition, sleepingRotation))
                {
                    sleepState = SleepState.lyingGoingToSleep;
                    fpsController.ResetMouseLook();
                }
                return true;

            case SleepState.asleep:
                //wait for a trigger from another script, such as text finishing on DreamController
                break;
        }

        return false;
    }

    public void LieDown()
    {
        LieDown(transform.position, Vector3.right * 275);
    }

    //Vector3 zero = new Vector3(0, 0, 0);
    /// <summary>
    /// public function for sending the player to sleep.
    /// </summary>
    public void LieDown(Vector3 position, Vector3 rotation)
    {
        sleepState = SleepState.goingDown;
        sleepingPosition = position;
        sleepingRotation = rotation;
    }

    /// <summary>
    /// public function for triggering dreams on eye close.
    /// </summary>
    public void Sleep()
    {
        sleepState = SleepState.asleep;

        if (r_dreamController.loadedScene == Scenes.Cell)
            r_dreamController.StartDream();
        else
            r_dreamController.EndDream();
    }

    public void WakeUp()
    {
        sleepState = SleepState.lyingAwake;

        if (r_dreamController.loadedScene == Scenes.Mountain)
        {
           dreamTransform = GameObject.Find("MountainStandingTransform").transform;
           SetPosition(dreamTransform);
        }
        else if(r_dreamController.loadedScene == Scenes.Columns)
        {
            dreamTransform = GameObject.Find("ColumnStandingTransform").transform;
            SetPosition(dreamTransform);
        }
        else if (r_dreamController.loadedScene == Scenes.Fire)
        {
            dreamTransform = GameObject.Find("FireStandingTransform").transform;
            SetPosition(dreamTransform);
        }
    }

    public void SetPosition(Transform newTransform)
    {
        transform.position = newTransform.position;
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
        
        if ( leftAngle > facingTriggerDegree ||
             forwardAngle < -facingTriggerDegree)
            return true;
        else
            return false;
    }

    public bool PlayerLerp(Transform player, FirstPersonController fpsController, Vector3 targetPosition, Vector3 targetRotation, float speed = 0.03f)
    {
        _controller.enabled = false;
        player.rotation = Quaternion.Lerp(player.rotation, Quaternion.Euler(targetRotation), speed);
        player.position = Vector3.Lerp(player.position, targetPosition, speed);

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