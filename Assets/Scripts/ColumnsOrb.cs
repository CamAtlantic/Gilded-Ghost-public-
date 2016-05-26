using UnityEngine;
using System.Collections;

public class ColumnsOrb : DreamTrigger
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
        r_dreamController.columns_idol = true;
        r_dreamText.SetDreamText(r_dreamText.columns_idol);
    }
}
