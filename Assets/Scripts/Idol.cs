using UnityEngine;
using System.Collections;

public class Idol : DreamTrigger {

    public float lerpUpHeight = 1.5f;

    public override void Update()
    {
        base.Update();
        if (triggered)
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, lerpUpHeight), 0.1f);
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
        r_cellManager.SpawnBird();
    }
}
