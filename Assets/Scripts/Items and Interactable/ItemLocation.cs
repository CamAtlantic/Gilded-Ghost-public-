using UnityEngine;
using System.Collections;

public enum Size { large, small};
public class ItemLocation : MonoBehaviour {

    public Vector3 placedPosition;
    public GameObject itemAtLocation;

    public Size locationSize = Size.large;

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