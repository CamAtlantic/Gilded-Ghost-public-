using UnityEngine;
using System.Collections;

public class Pillow : Interactable {

    SleepingAndWaking sleepingScript;

   public override void Awake()
    {
        base.Awake();

        sleepingScript = player.GetComponent<SleepingAndWaking>();
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();

        sleepingScript.GoToSleep();
    }
}
