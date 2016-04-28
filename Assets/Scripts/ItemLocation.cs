using UnityEngine;
using System.Collections;

public class ItemLocation : MonoBehaviour {

    public Vector3 placedPosition;
    public GameObject itemAtLocation;

    void Awake ()
    {
        tag = "ItemLocation";
        gameObject.layer = 8;

        if (transform.childCount == 1)
        {
            ReceiveItem(transform.GetChild(0).gameObject);
        }
    }
    
    public void ReceiveItem(GameObject item)
    {
        itemAtLocation = item;
        gameObject.layer = 2; //ignore raycast layer
    }

    public void Reset()
    {
        itemAtLocation = null;
        gameObject.layer = 8;
    }
}