using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
    Camera cam;

    public GameObject heldItem;
    public Item heldItemScript;

    float holdDistance = 1;

    public float speed = 10;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Physics.Raycast(ray, out hit, 5);

        if (Input.GetMouseButtonDown(0))
        {
            // Do something with the object that was hit by the raycast.
            Transform objectHit = hit.transform;
            print(objectHit.name);

            if (objectHit.CompareTag("Item"))
            {
                PickUp(objectHit.gameObject);
            }

            if (heldItem && objectHit.CompareTag("ItemLocation"))
            {
                PutDownOn(objectHit.gameObject);
            }
        }

        if (heldItem)
        {

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
