using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Thrones : DreamTrigger {

    Transform location;

    public override void Awake()
    {
        base.Awake();
        location = transform.FindChild("Location");
    }

    public override void Update()
    {
        base.Update();
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
        r_dreamText.SetDreamText(r_dreamText.fire_thrones);
        TriggerLieDown(location.transform.position, (transform.localRotation * Quaternion.Euler(new Vector3(-90, 180, 0))).eulerAngles);
            r_cellManager.KillLight();
    }
}
