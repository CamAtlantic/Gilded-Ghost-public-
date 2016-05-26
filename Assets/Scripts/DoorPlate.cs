using UnityEngine;
using System.Collections;

public class DoorPlate : Interactable {

    bool move = false;
    Quaternion oldRot;
    Quaternion newRot;

    public Quaternion relative;
    void Update()
    {
        if(move)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, 0.3f);
            relative = Quaternion.Inverse(transform.localRotation) * newRot;

            if(relative.w == 1)
            {
                move = false;
            }
        }
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        move = true;
        oldRot = transform.localRotation;
        newRot = transform.localRotation * Quaternion.Euler(0, 0, 45);
    }
}
