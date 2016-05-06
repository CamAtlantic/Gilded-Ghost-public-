using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    //public float amplitudeX = 10.0f;
    public float amplitudeY = 5.0f;
    //public float omegaX = 1.0f;
    public float omegaY = 5.0f;
    public float index;

    public float speed = 0.1f;

    float startHeight;

	// Use this for initialization
	void Start () {
        startHeight = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, speed, 0);

        index += Time.deltaTime;

        float y = amplitudeY * Mathf.Sin(omegaY * index) + startHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
}