using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
    public enum State{ Held, LerpToLocation, AtLocation }
    public State itemState;

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
    public static Icon e_Icon;

    public virtual void Awake()
    {
        tag = "Item";
        player = GameObject.Find("Player");
        e_Icon = GameObject.Find("E_Icon").GetComponent<Icon>();

        cam = Camera.main;
        collide = GetComponent<MeshCollider>();

        itemState = State.AtLocation;
    }

	// Use this for initialization
	void Start () {
        speed = player.GetComponent<PickUpItem>().speed;
    }

    // Update is called once per frame
    public virtual void Update () {
	    if(itemState == State.Held)
        {
            LerpTo(heldPosition, heldRotation);

            if (Input.GetKeyDown(KeyCode.E))
                Use();
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
        if (transform.parent && transform.parent.GetComponent<ItemLocation>())
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

    public virtual void Use()
    {
        if (!e_Icon.hasBeenCleared)
            e_Icon.Clear();
    }

}
