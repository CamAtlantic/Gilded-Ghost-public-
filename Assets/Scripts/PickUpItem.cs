using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpItem : MonoBehaviour {
    Camera cam;
    FirstPersonController playerFPS;

    public GameObject heldItem;
    public Item heldItemScript;


    float holdDistance = 1;
    public float speed = 10;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        playerFPS = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerFPS.mouseLookSlow = false;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, 5))
        {
            Transform objectHit = hit.transform;
            bool lookingAtItem = false;
            bool lookingAtLocation = false;

            if (!heldItem && objectHit.CompareTag("Item"))
            {
                playerFPS.mouseLookSlow = true;
                lookingAtItem = true;
            }

            if (heldItem && objectHit.CompareTag("ItemLocation"))
            {
                playerFPS.mouseLookSlow = true;
                lookingAtLocation = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Do something with the object that was hit by the raycast.
                print(objectHit.name);

                if (lookingAtItem)
                {
                    PickUp(objectHit.gameObject);
                }

                if (lookingAtLocation)
                {
                    PutDownOn(objectHit.gameObject);
                }
            }
        }

    }

    void PickUp(GameObject item)
    {
        heldItem = item;
        heldItemScript = heldItem.GetComponent<Item>();
        heldItemScript.PickUp();
    }

    void PutDownOn(GameObject itemLocation)
    {
        heldItemScript.PutDownOn(itemLocation);
        heldItem.transform.parent = itemLocation.transform;
        heldItem = null;
        heldItemScript = null;
    }
}
