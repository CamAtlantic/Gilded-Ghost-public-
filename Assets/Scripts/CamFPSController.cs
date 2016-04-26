using UnityEngine;
using System.Collections;


public class CamFPSController : MonoBehaviour {

    public float speed;

    [SerializeField]
    private CamMouseLook mouseLook;
    private Camera camera;

    [SerializeField]
    private SleepingAndWaking sleepingAndWaking;

    private CharacterController charactercontroller;
    // Use this for initialization
    void Start () {
        camera = Camera.main;
        mouseLook.Init(transform, camera.transform);
        charactercontroller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (sleepingAndWaking.UpdateSleepState(transform, this))
        {
            //lock mouse rotation
        }
        //else
        {
            RotateView();
        }

        //-----------------------------------
        //--MOVEMENT-------------
        //-----------------------------------
        if (sleepingAndWaking.sleepState == SleepState.standing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                charactercontroller.Move(Vector3.forward);
            }
            if (Input.GetKey(KeyCode.S))
            {
                charactercontroller.Move(Vector3.back);
            }
            if (Input.GetKey(KeyCode.A))
            {
                charactercontroller.Move(Vector3.left);
            }
            if (Input.GetKey(KeyCode.D))
            {
                charactercontroller.Move(Vector3.right);
            }
        }
    }

    void Move(Vector3 vector)
    {
        transform.Translate(vector * Time.deltaTime * speed);
    }

    private void RotateView()
    {
        //if (mouseLookSlow)
        //{
          //  mouseLook.slow = true;
        //}
        //else
        //{
          //  mouseLook.slow = false;
        //}
        mouseLook.LookRotation(transform, camera.transform);

    }

    public void ResetMouseLook()
    {
        mouseLook.Init(transform, camera.transform);
    }
}