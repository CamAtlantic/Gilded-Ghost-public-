using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public enum SleepState { lyingGoingToSleep, lyingAwake, goingUp, standing, goingDown, asleep };

[Serializable]
public class SleepingAndWaking : MonoBehaviour{
    DreamController _dreamController;
    CharacterController _controller;
//    Eyelids _eyelids;
    
//    MainGUI _mainGui;

    public SleepState sleepState;

    public int facingTriggerDegree = 70;
    public float standingSleepingSpeed = 0.2f;


    Vector3 CellStandUpPosition = new Vector3(-0.2f, 0.921f, -0.65f);
    Vector3 standUpPosition = new Vector3(-0.2f, 0.921f, -0.65f);
    Vector3 standUpRotation = new Vector3(0, -90, 0);

    Vector3 sleepingPosition = new Vector3(0.82f, 0.75f, -0.67f);
    Vector3 sleepingRotation = new Vector3(275, 0, 0);

    void Awake()
    {
        _dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        _controller = GetComponent<CharacterController>();
       // _eyelids = GameObject.Find("Eyelids").GetComponent<Eyelids>();
       // _mainGui = GameObject.Find("Main GUI").GetComponent<MainGUI>();
    }

    public bool UpdateSleepState(Transform player, FirstPersonController fpsController)
    {
        switch (sleepState)
        {
            case SleepState.lyingGoingToSleep:

                if (CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.lyingAwake:
                _dreamController.SetActiveScene();

                if (CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.goingUp:

                if (_dreamController.loadedScene != Scenes.Cell)
                    standUpPosition = transform.position;
                else
                    standUpPosition = CellStandUpPosition;

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

    //should probably be its own class as it will need to also return player to sleep
    
    /// <summary>
    /// public function for sending the player to sleep.
    /// </summary>
    public void LieDown()
    {
        sleepState = SleepState.goingDown;
    }

    /// <summary>
    /// public function for triggering dreams on eye close.
    /// </summary>
    public void Sleep()
    {
        sleepState = SleepState.asleep;

        if (_dreamController.loadedScene == Scenes.Cell)
            _dreamController.StartDream();
        else
            _dreamController.EndDream();
    }

    public void WakeUp()
    {
        sleepState = SleepState.lyingAwake;
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