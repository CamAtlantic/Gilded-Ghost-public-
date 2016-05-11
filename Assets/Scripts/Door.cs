using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    Tray _tray;

    Transform traySlot;
    Transform eyeSlot;

    Vector3 trayClosedPosition;
    Vector3 trayOpenPosition;

    Vector3 eyeClosedPosition;
    Vector3 eyeOpenPosition;

    bool traySlotOpen;
    bool eyeSlotOpen;

    public float openCloseSpeed = 0.2f;

    //EVERYTHING IN THIS SCRIPT USES LOCALPOSITION

    void Awake()
    {
        traySlot = transform.FindChild("TraySlot");
        eyeSlot = transform.FindChild("EyeSlot");
        _tray = GameObject.Find("Tray").GetComponent<Tray>();
    }

    void Start()
    {
        trayClosedPosition = traySlot.localPosition;
        trayOpenPosition = new Vector3(trayClosedPosition.x, trayClosedPosition.y, trayClosedPosition.z + 15);

        eyeClosedPosition = eyeSlot.localPosition;
        eyeOpenPosition = new Vector3(eyeClosedPosition.x - 50, eyeClosedPosition.y, eyeClosedPosition.z);
    }

    void Update()
    {
        //At the moment these lerps will run all the time
        if (traySlotOpen)
        {
            traySlot.localPosition = Vector3.Lerp(traySlot.localPosition, trayOpenPosition, openCloseSpeed);

            if (!_tray.inside)
                CloseTraySlot();
        }
        else
        {
            traySlot.localPosition = Vector3.Lerp(traySlot.localPosition, trayClosedPosition, openCloseSpeed);
        }

        if(eyeSlotOpen)
        {
            eyeSlot.localPosition = Vector3.Lerp(eyeSlot.localPosition, eyeOpenPosition, openCloseSpeed);
        }
        else
        {
            eyeSlot.localPosition = Vector3.Lerp(eyeSlot.localPosition, eyeClosedPosition, openCloseSpeed);
        }
    }

	public void OpenTraySlot()
    {
        traySlotOpen = true;
    }

    public void CloseTraySlot()
    {
        traySlotOpen = false;
    }

    public void OpenEyeSlot()
    {
        eyeSlotOpen = true;
    }

    public void CloseEyeSlot()
    {
        eyeSlotOpen = false;
    }
}
