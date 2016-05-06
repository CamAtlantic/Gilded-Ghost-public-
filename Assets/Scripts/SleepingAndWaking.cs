using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public enum SleepState { sleeping, lyingAwake, goingUp, standing, goingDown, dream };

[Serializable]
public class SleepingAndWaking : MonoBehaviour{
    DreamController _sceneControl;
    CharacterController _controller;
    Eyelids _eyelids;
    DreamText _dreamText;
    MainGUI _mainGui;

    public SleepState sleepState;

    public int facingTriggerDegree = 70;
    public float standingSleepingSpeed = 0.2f;

    Vector3 standUpPosition = new Vector3(-0.2f, 0.921f, -0.65f);
    Vector3 standUpRotation = new Vector3(0, -90, 0);

    Vector3 sleepingPosition = new Vector3(0.82f, 0.75f, -0.67f);
    Vector3 sleepingRotation = new Vector3(275, 0, 0);

    void Awake()
    {
        _sceneControl = GameObject.Find("MainController").GetComponent<DreamController>();
        _controller = GetComponent<CharacterController>();
        _eyelids = GameObject.Find("Eyelids").GetComponent<Eyelids>();
        _dreamText = GameObject.Find("Dream").GetComponent<DreamText>();
        _mainGui = GameObject.Find("Main GUI").GetComponent<MainGUI>();
    }

    public bool UpdateSleepState(Transform player, FirstPersonController fpsController)
    {
        switch (sleepState)
        {
            case SleepState.sleeping:

                if (CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.lyingAwake:
                _sceneControl.SetActiveScene();

                if (CheckPlayerFacing(player))
                {
                    sleepState = SleepState.goingUp;
                }
                break;

            case SleepState.goingUp:

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Mountain Dream"))
                    standUpPosition = transform.position;
                

                    if (PlayerLerp(player,fpsController,standUpPosition,standUpRotation))
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
                    sleepState = SleepState.sleeping;
                    fpsController.ResetMouseLook();
                }
                return true;

            case SleepState.dream:
                Dream();
                break;
        }

        return false;
    }
    bool dreamHasLoaded = false;

    //should probably be its own class as it will need to also return player to sleep
    private void Dream()
    {
        if (!dreamHasLoaded)
        {
            _sceneControl.LoadMountainDream();

            dreamHasLoaded = true;
        }
        //else
        {
          //  _sceneControl.LoadCell();
           // dreamHasLoaded = false;
        }

        //needed for mountain dream at least
        QualitySettings.shadowDistance = 50;

        if (_dreamText.DisplayText())
            sleepState = SleepState.lyingAwake;
    }

    /// <summary>
    /// public function for triggering SleepState.goingDown.
    /// </summary>
    public void GoToSleep()
    {
        sleepState = SleepState.goingDown;
    }

    /// <summary>
    /// public function for triggering SleepState.dream.
    /// </summary>
    public void StartDream()
    {
        sleepState = SleepState.dream;
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