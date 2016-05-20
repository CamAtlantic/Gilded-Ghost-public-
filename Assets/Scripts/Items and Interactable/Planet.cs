﻿using UnityEngine;
using System.Collections;

public class Planet : DreamTrigger
{
    public override void Update()
    {
        base.Update();
        if (spinFinished)
            TriggerLieDown();
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        Spin();
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_dreamController.mountain_door = true;
    }
}