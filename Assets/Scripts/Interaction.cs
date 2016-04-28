using UnityEngine;
using System.Collections;

public enum LookingAt { none, item,itemLocation,interactable};

public class Interaction : MonoBehaviour
{

    //------------------------------------------------
    //This script should contain all code relevant to
    //looking at objects, triggering GUI responses,
    //and triggering object responses.
    //------------------------------------------------

    Camera cam;
    MainGUI mainGUI;
    PickUpItem pickUpScript;
    Door doorScript;

    Transform objectHit;

    public LookingAt reticule = LookingAt.none;

    void Start()
    {
        cam = Camera.main;
        mainGUI = GameObject.Find("Main GUI").GetComponent<MainGUI>();
        pickUpScript = GetComponent<PickUpItem>();
        doorScript = GameObject.Find("Door").GetComponent<Door>();
    }
    
    void Update()
    {
        //Check if ray hits------------------------------
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        objectHit = null;
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            pickUpScript.EatFood();
        }


        if (Physics.Raycast(ray, out hit, 5))
        {
            objectHit = hit.transform;
            reticule = LookingAt.none;
            
            //Identify type of object------------------------

            //if not holding item, looking at item is valid
            if (!pickUpScript.heldItem && objectHit.CompareTag("Item"))
            {
                //playerFPS.mouseLookSlow = true;
                reticule = LookingAt.item;
            }

            //if holding item, looking at location is valid
            if (pickUpScript.heldItem && objectHit.CompareTag("ItemLocation"))
            {
                //playerFPS.mouseLookSlow = true;
                reticule = LookingAt.itemLocation;
            }

            if(objectHit.CompareTag("Interactable"))
            {
                reticule = LookingAt.interactable;
            }

            //Hide or show GUI response---------------------

            if (reticule != LookingAt.none)
            {
                //print("showing");
                mainGUI.ShowSelectRing();
            }
            else
            {
                mainGUI.HideSelectRing();
            }
            
            GetInput();

        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Do something with the object that was hit by the raycast.

            //I should change item and itemLocation to also have InteractTrigger() functions.

            if (reticule == LookingAt.item)
            {
                pickUpScript.PickUp(objectHit.gameObject);
            }

            if (reticule == LookingAt.itemLocation)
            {
                pickUpScript.PutDownOn(objectHit.gameObject);
            }

            if (reticule == LookingAt.interactable)
            {
                objectHit.GetComponent<Interactable>().InteractTrigger();
            }
        }

        
    }
}
