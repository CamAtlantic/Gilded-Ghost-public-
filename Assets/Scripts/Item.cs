using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
    public enum State{ Held, LerpToLocation, AtLocation }
    State itemState;

    GameObject player;
    public Camera cam;
    Collider collide;

    public Vector3 heldPosition;
    public Vector3 heldRotation;

    public Vector3 placedRotation;

    public bool held;
    public bool atLocation;

    public GameObject itemLocation;
    public ItemLocation itemLocationScript;

    public float speed = 10;

    
    void Awake()
    {
        tag = "Item";
        player = GameObject.Find("Player");
        
        cam = Camera.main;
        collide = GetComponent<MeshCollider>();

        itemState = State.AtLocation;
    }

	// Use this for initialization
	void Start () {
        //if (player != null)
            speed = player.GetComponent<PickUpItem>().speed;
    }

    // Update is called once per frame
    void Update () {
	    if(itemState == State.Held)
        {
            LerpTo(heldPosition, heldRotation);
        }
        if (itemLocationScript != null && itemState == State.LerpToLocation)
        {
            LerpTo(itemLocationScript.placedPosition, placedRotation);

            if (Vector3.Distance(transform.localPosition, itemLocationScript.placedPosition) < 0.01f)
            {
                transform.localPosition = itemLocationScript.placedPosition;
                itemState = State.AtLocation;
                collide.enabled = true;
            }
        }
        
	}

    public void PickUp()
    {
        transform.parent.GetComponent<ItemLocation>().Reset();
        transform.parent = cam.transform;
        itemState = State.Held;
        collide.enabled = false;
    }

    public void PutDownOn(GameObject itemLocation)
    {
        transform.parent = itemLocation.transform;
        itemState = State.LerpToLocation;
        itemLocationScript = itemLocation.GetComponent<ItemLocation>();
    }
    
    public void LerpTo(Vector3 position, Vector3 rotation)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, position, speed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation), speed * Time.deltaTime);
    }

}
