using UnityEngine;
using System.Collections;

public class MountainDoor : MonoBehaviour {

    DoorPlate[] all;
    DoorPlate one;
    DoorPlate two;
    DoorPlate three;

    GameObject plane1;
    //GameObject plane2;

    bool open;

    void Awake()
    {
        all = GetComponentsInChildren<DoorPlate>();
        one = all[0];
        two = all[1];
        three = all[2];

        plane1 = GameObject.Find("Plane 1");
       // plane2 = GameObject.Find("Plane 2");

        plane1.SetActive(false);
    }

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if (one.currentSlot == two.currentSlot && two.currentSlot == three.currentSlot)
        {
            Open();
        }

        if (open)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-90, 0, -90), 0.05f);
        }
    }

    void Open()
    {
        print(true);
        open = true;
        plane1.SetActive(true);
    }
}
