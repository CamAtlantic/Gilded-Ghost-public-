using UnityEngine;
using System.Collections;

public class TreeCubeMouseTrigger : Interactable {

    TreeCube r_treeCubeScript;

    public override void Awake()
    {
        base.Awake();
        r_treeCubeScript = GetComponentInParent<TreeCube>();
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        r_treeCubeScript.InteractTrigger();
        tag = "Untagged";
    }
}
