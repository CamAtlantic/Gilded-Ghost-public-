using UnityEngine;
using System.Collections;

public class Planet : Interactable {

    SleepingAndWaking _sleepingAndWaking;

    public override void Awake()
    {
        base.Awake();
        _sleepingAndWaking = player.GetComponent<SleepingAndWaking>();
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        _sleepingAndWaking.LieDown();
    }
}
