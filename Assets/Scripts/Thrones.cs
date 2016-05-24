using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Thrones : DreamTrigger {

    Transform location;
//    FirstPersonController fpsController;
    public override void Awake()
    {
        base.Awake();
        location = transform.FindChild("Location");
  //      fpsController = player.GetComponent<FirstPersonController>();
    }

    public override void Update()
    {
        base.Update();
        if(triggered)
        {
            
        }
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        DreamTriggerEffect();
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_dreamController.fire_thrones = true;
        TriggerLieDown(location.transform.position, (transform.localRotation * Quaternion.Euler(new Vector3(-90, 180, 0))).eulerAngles);
    }
}
