using UnityEngine;
using System.Collections;

public class CamBackgroundColor : MonoBehaviour {

    public Gradient gradient;

    private Camera cam;


    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    
	// Update is called once per frame
	void Update () {
        //cam.backgroundColor = gradient.Evaluate(_dayManager.percentageOfDay);
	}

    public void SetBackgroundColor(Color newColor)
    {
        cam.clearFlags = CameraClearFlags.Color;
        cam.backgroundColor = newColor;
    }

    public void SetSkyBox()
    {
        cam.clearFlags = CameraClearFlags.Skybox;
    }
}
