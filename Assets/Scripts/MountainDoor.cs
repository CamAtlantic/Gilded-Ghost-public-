using UnityEngine;
using System.Collections;

public class MountainDoor : MonoBehaviour {

    DoorPlate[] all;
    DoorPlate one;
    DoorPlate two;
    DoorPlate three;

    bool open;

    void Awake()
    {
        all = GetComponentsInChildren<DoorPlate>();
        one = all[0];
        two = all[1];
        three = all[2];
    }

	// Use this for initialization
	void Start () {
        foreach (DoorPlate ring in all)
        {
            int rot = Random.Range(1, 8) * 45;
            ring.gameObject.transform.localRotation *= Quaternion.Euler(0, 0, rot); 
        }

        if(open)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-90, 0, -90), 0.3f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	if(one.relative == two.relative && two.relative == three.relative)
        {
            Open();
        }
	}

    void Open()
    {
        print(true);
        open = true;
    }
}
