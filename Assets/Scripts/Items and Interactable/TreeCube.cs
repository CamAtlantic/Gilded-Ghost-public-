using UnityEngine;
using System.Collections;

public class TreeCube : DreamTrigger {

    GameObject geoTree;
    Vector3 lerpSize;

    public override void Awake()
    {
        base.Awake();
        geoTree = GameObject.Find("GeoTree");
        lerpSize = geoTree.transform.localScale * 1.25f;
    }

    public override void Update()
    {
        base.Update();

        if (triggered)
        {
            foreach (TreeCubeMouseTrigger trig in GetComponentsInChildren<TreeCubeMouseTrigger>())
            {
                trig.gameObject.tag = "Untagged";
            }
            geoTree.transform.localScale = Vector3.Lerp(geoTree.transform.localScale, lerpSize, 0.2f);

            if (spinFinished)
            {
                TriggerLieDown();
            }
        }
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        Spin();
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_cellManager.SpawnPlant();
        r_dreamController.mountain_plant = true;
        r_dreamText.SetDreamText(r_dreamText.mountain_plant);
    }
}
