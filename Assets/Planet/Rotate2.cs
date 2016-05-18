using UnityEngine;
using System.Collections;

public class Rotate2 : MonoBehaviour {

    public Vector3 rotateAngle = new Vector3(0.5f, 0.5f, 0);
    public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, rotateAngle, speed);
	}
}