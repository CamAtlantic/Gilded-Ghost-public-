using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    SleepingAndWaking r_sleepScript;

    Tray _tray;

    Transform traySlot;
    Transform eyeSlot;

    Vector3 trayClosedPosition;
    Vector3 trayOpenPosition;

    Vector3 eyeClosedPosition;
    Vector3 eyeOpenPosition;

    GameObject dramaLight;

    bool doorOpen;
    bool traySlotOpen;
    bool eyeSlotOpen;

    public AudioClip eyeSlotSound;
    public AudioClip traySlotSound;

    public float openCloseSpeed = 0.2f;
    [HideInInspector]
    new public AudioSource audio;
    //EVERYTHING IN THIS SCRIPT USES LOCALPOSITION

    void Awake()
    {
        traySlot = transform.FindChild("TraySlot");
        eyeSlot = transform.FindChild("EyeSlot");
        _tray = GameObject.Find("Tray").GetComponent<Tray>();
        dramaLight = GameObject.Find("DramaLight");
        r_sleepScript = GameObject.Find("Player").GetComponent<SleepingAndWaking>();
        audio = GetComponent<AudioSource>();

    }

    void Start()
    {
        dramaLight.SetActive(false);

        trayClosedPosition = traySlot.localPosition;
        trayOpenPosition = new Vector3(trayClosedPosition.x, trayClosedPosition.y, trayClosedPosition.z + 0.25f);

        eyeClosedPosition = eyeSlot.localPosition;
        eyeOpenPosition = new Vector3(eyeClosedPosition.x - 0.5f, eyeClosedPosition.y, eyeClosedPosition.z);
    }

    void Update()
    {
        //At the moment these lerps will run all the time
        if (traySlotOpen)
        {
            traySlot.localPosition = Vector3.Lerp(traySlot.localPosition, trayOpenPosition, openCloseSpeed);

        }
        else
            traySlot.localPosition = Vector3.Lerp(traySlot.localPosition, trayClosedPosition, openCloseSpeed);
        if(eyeSlotOpen)
            eyeSlot.localPosition = Vector3.Lerp(eyeSlot.localPosition, eyeOpenPosition, openCloseSpeed);
        else
            eyeSlot.localPosition = Vector3.Lerp(eyeSlot.localPosition, eyeClosedPosition, openCloseSpeed);

        if(doorOpen && r_sleepScript.sleepState == SleepState.goingUp)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rot, Time.deltaTime * 2);
            Quaternion relative = Quaternion.Inverse(transform.localRotation) * rot;
            if (relative.w > 0.95f)
            {
                dramaLight.SetActive(false);
                if (relative.w == 1)
                    doorOpen = false;
            }
        }
    }

    Quaternion rot;
    public void DramaOpenDoor()
    {
        transform.localRotation *= Quaternion.Euler(0, 0, 20);
        rot = transform.localRotation * Quaternion.Euler(0, 0, -20);
        dramaLight.SetActive(true);
        doorOpen = true;
    }

	public void OpenTraySlot()
    {
        
        if(traySlotOpen == false)
        {
            traySlotOpen = true;
            audio.PlayOneShot(traySlotSound);
            
        }
        
    }

    public void CloseTraySlot()
    {
        if (traySlotOpen == true)
        {
            traySlotOpen = false;
            audio.PlayOneShot(traySlotSound);
        }
    }

    public void OpenEyeSlot()
    {
        if (eyeSlotOpen == false)
        {
            eyeSlotOpen = true;
            audio.PlayOneShot(eyeSlotSound);
        }
    }

    public void CloseEyeSlot()
    {
        if (eyeSlotOpen == true)
        {
            eyeSlotOpen = false;
            audio.PlayOneShot(eyeSlotSound);
        }
    }
}
