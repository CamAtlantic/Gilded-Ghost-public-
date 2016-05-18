using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rotate))]
public class DreamTrigger : Interactable
{
    [HideInInspector]
    public SleepingAndWaking r_sleepingAndWaking;
    [HideInInspector]
    public DreamController r_dreamController;
    [HideInInspector]
    public Rotate r_rotate;

    public bool triggered;

    public override void Awake()
    {
        base.Awake();
        r_sleepingAndWaking = player.GetComponent<SleepingAndWaking>();
        r_dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        r_rotate = GetComponent<Rotate>();
        if (r_rotate == null)
            Debug.LogError("DreamTrigger has no Rotate?!!!!");
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
    }

    public virtual void DreamTriggerEffect()
    {
        triggered = true;
    }
}
