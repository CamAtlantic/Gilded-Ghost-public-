using UnityEngine;
using System.Collections;

public class ShiftIcon : Icon {

    public float holdTimer = 0;
    float holdTimerMax = 3;

	// Use this for initialization
	void Start () {
	
	}

    public override void Update()
    {
        base.Update();
        if (!hasBeenCleared)
        {
            if (DreamController.loadedScene != Scenes.Cell)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    ShowIcon();
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    holdTimer += Time.deltaTime;

                    if (holdTimer > holdTimerMax)
                    {
                        Clear();
                    }
                }
            }
            else
            {
                HideIcon();
            }
        }
    }
}
