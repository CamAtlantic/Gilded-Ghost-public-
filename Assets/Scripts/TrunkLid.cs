using UnityEngine;
using System.Collections;

public class TrunkLid : Interactable {
    Trunk _trunkScript;

    bool trunk_pulledOut = false;
    [HideInInspector]
    public bool isOpen = false;

    public float openCloseSpeed = 0.3f;

    public override void Awake()
    {
        base.Awake();
        _trunkScript = transform.parent.GetComponent<Trunk>();
    }
    
	// Update is called once per frame
	void Update () {
        trunk_pulledOut = _trunkScript.pulledOut;

        if (isOpen)
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, new Vector3(0, 90, 0), openCloseSpeed));
        else
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles, Vector3.zero, openCloseSpeed));
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();

        if (!trunk_pulledOut)
            _trunkScript.PullOut();
        else
        {
            if (isOpen)
                Close();
            else
                Open();
        }
    }

    void Open()
    {
        isOpen = true;
    }

    void Close()
    {
        isOpen = false;
    }
}
