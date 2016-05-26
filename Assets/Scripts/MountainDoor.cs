using UnityEngine;
using System.Collections;

public class MountainDoor : MonoBehaviour {

    Interactable[] all;
    Interactable one;
    Interactable two;
    Interactable three;

    void Awake()
    {
        all = GetComponentsInChildren<Interactable>();
    }

	// Use this for initialization
	void Start () {
	foreach (Interactable ring in all)
        {
            int rot = Random.Range(1, 8) * 45;
            print (rot);
            ring.gameObject.transform.localRotation *= Quaternion.Euler(0, 0, rot);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
