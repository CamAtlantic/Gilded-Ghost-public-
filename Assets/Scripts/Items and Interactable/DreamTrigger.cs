using UnityEngine;
using System.Collections;

public class DreamTrigger : Interactable
{
    [HideInInspector]
    public CellManager r_cellManager;
    [HideInInspector]
    public SleepingAndWaking r_sleepingAndWaking;
    [HideInInspector]
    public DreamController r_dreamController;
    [HideInInspector]
    public Rotate r_rotate;

    public bool triggered;

    float spinTimer = 0;
    float spinTimerMax = 3;
    bool isSpinning;
    public bool spinFinished = false;

    bool triggerLieDownOnce = false;

    public override void Awake()
    {
        base.Awake();
        r_sleepingAndWaking = player.GetComponent<SleepingAndWaking>();
        r_dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        r_cellManager = GameObject.Find("CellManager").GetComponent<CellManager>();
        r_rotate = GetComponent<Rotate>();
        if (r_rotate == null)
            Debug.Log("DreamTrigger has no Rotate?!!!!");  
    }

    public virtual void Update()
    {
        if (triggered)
        {
            tag = "Untagged";

            if (isSpinning)
            {
                if (spinTimer < spinTimerMax)
                {
                    spinTimer += Time.deltaTime;
                    r_rotate.speed += Time.deltaTime;
                }
                else
                    spinFinished = true;
            }
        }
    }

    public void TriggerLieDown()
    {

        if (!triggerLieDownOnce)
        {
            triggerLieDownOnce = true;
            r_sleepingAndWaking.LieDown();
        }
    }

    public void Spin()
    {
        isSpinning = true;
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        DreamTriggerEffect();
    }

    public virtual void DreamTriggerEffect()
    {
        triggered = true;
    }
}
