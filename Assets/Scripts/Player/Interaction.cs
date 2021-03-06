﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a tag for what kind of object the player is currently looking at.
/// </summary>
public enum LookingAt { none, item,itemLocation,interactable,Fire};

public class Interaction : MonoBehaviour
{

    //------------------------------------------------
    //This script should contain all code relevant to
    //looking at objects, exposing that data to MainGUI,
    //and triggering object responses.
    //------------------------------------------------

    Camera cam;
    PickUpItem pickUpScript;

    public Transform objectHit;

    public static LookingAt reticule = LookingAt.none;

    public LayerMask noItem_LayerMask;
    public LayerMask item_LayerMask;

    public float interactDist;

    void Start()
    {
        cam = Camera.main;
        pickUpScript = GetComponent<PickUpItem>();
    }
    
    void Update()
    {
        
        //Check if ray hits------------------------------
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        objectHit = null;

        LayerMask layerMask = noItem_LayerMask;
        if(PickUpItem.heldItem)
        {
            layerMask = item_LayerMask;
        }
        

        if (Physics.Raycast(ray, out hit, 100,layerMask))
        {
            objectHit = hit.transform;
            reticule = LookingAt.none;
            
            if (Vector3.Distance(cam.transform.position, objectHit.transform.position) < interactDist)
            {
                //Identify type of object------------------------
                if (objectHit.CompareTag("Interactable"))
                {
                    reticule = LookingAt.interactable;
                }
                
                //if not holding item, looking at item is valid
                if (!PickUpItem.heldItem && objectHit.CompareTag("Item"))
                {
                    reticule = LookingAt.item;
                }

                //if holding item, looking at location is valid
                if (PickUpItem.heldItem && objectHit.CompareTag("ItemLocation"))
                {
                    reticule = LookingAt.itemLocation;
                }
            }

            //full distance for fire, if it's not fire move along
            
            else if (objectHit.CompareTag("Fire"))
            {
                reticule = LookingAt.Fire;
            }

            GetInput();

        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Do something with the object that was hit by the raycast.

            //Should I change item and itemLocation to also have InteractTrigger() functions?

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
