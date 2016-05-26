using UnityEngine;
using System.Collections;

public class Planet : DreamTrigger
{
    Door door;

    public override void Awake()
    {
        base.Awake();
        door = GameObject.Find("Door").GetComponent<Door>();
    }

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
        r_dreamText.SetDreamText(r_dreamText.mountain_door);
        door.DramaOpenDoor();
    }
}
