using UnityEngine;
using System.Collections;

public class Thrones : DreamTrigger {

    GameObject fireStandingTransform;

    public override void Awake()
    {
        base.Awake();
        fireStandingTransform = GameObject.Find("FireStandingTransform");
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
        TriggerLieDown();
    }
}
