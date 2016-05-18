using UnityEngine;
using System.Collections;

public class TreeCube : DreamTrigger {

    float spinTimer = 0;
    float spinTimerMax = 3;

    GameObject geoTree;
    CellManager r_cellManager;
    bool triggerLieDownOnce = false;

    Vector3 lerpSize;

    public override void Awake()
    {
        base.Awake();
        geoTree = GameObject.Find("GeoTree");
        r_cellManager = GameObject.Find("CellManager").GetComponent<CellManager>();
    }
    public void Start()
    {
        lerpSize = geoTree.transform.localScale * 1.25f;
    }

    public override void Update()
    {
        base.Update();

        if (triggered)
        {
            tag = "Untagged";

            foreach (TreeCubeMouseTrigger trig in GetComponentsInChildren<TreeCubeMouseTrigger>())
            {
                trig.gameObject.tag = "Untagged";
            }

            if (spinTimer < spinTimerMax)
            {
                spinTimer += Time.deltaTime;
                r_rotate.speed += Time.deltaTime;
                geoTree.transform.localScale = Vector3.Lerp(geoTree.transform.localScale, lerpSize, 0.3f);
            }
            else if (!triggerLieDownOnce)
            {
                triggerLieDownOnce = true;
                r_sleepingAndWaking.LieDown();
            }
        }
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        DreamTriggerEffect();
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_cellManager.SpawnPlant();
    }
}
