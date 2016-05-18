using UnityEngine;
using System.Collections;

public class CamBackgroundColor : MonoBehaviour {

    public Gradient gradient;

    private Camera cam;
    DayManager _dayManager;
    DreamController _dreamController;

    void Awake()
    {
        cam = GetComponent<Camera>();
        _dayManager = GameObject.Find("MainController").GetComponent<DayManager>();
        _dreamController = _dayManager.gameObject.GetComponent<DreamController>();
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
