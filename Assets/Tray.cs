using UnityEngine;
using System.Collections;

public class Tray : MonoBehaviour {

    Vector3 corridorPosition;
    Vector3 insidePosition;

    bool inside = false;
    public float pushSpeed = 0.3f;
	// Use this for initialization
	void Start () {
        corridorPosition = transform.localPosition;
        insidePosition = new Vector3(corridorPosition.x, corridorPosition.y, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (inside)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, insidePosition, pushSpeed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, corridorPosition, pushSpeed);
        }

	}

    public void PushInside()
    {
        inside = true;
    }

    public void PushOutside()
    {
        inside = false;
    }
}
