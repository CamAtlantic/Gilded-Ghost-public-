using UnityEngine;
using System.Collections;

public class Trunk : Interactable {

    TrunkLid _trunkLidScript;
    Vector3 underBedPosition;
    Vector3 pulledOutPosition;

    public bool pulledOut = false;
    public float pushSpeed = 0.3f;
    
    public override void Awake()
    {
        base.Awake();
        _trunkLidScript = GetComponentInChildren<TrunkLid>();
    }

    // Use this for initialization
    void Start()
    {
        underBedPosition = transform.localPosition;
        pulledOutPosition = new Vector3(underBedPosition.x - 0.7f, underBedPosition.y, underBedPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (pulledOut)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pulledOutPosition, pushSpeed);

            if (_trunkLidScript.isOpen)
                tag = "Untagged";
            else
                tag = "Interactable";
        }
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, underBedPosition, pushSpeed);
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();

        if (pulledOut && !_trunkLidScript.isOpen)
            PushUnder();
        else
            PullOut();
    }

    public void PullOut()
    {
        if (pulledOut == false)
        {
            pulledOut = true;
        }
    }

    public void PushUnder()
    {
        pulledOut = false;
    }

}
