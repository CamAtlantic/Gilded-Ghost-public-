using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpItem : MonoBehaviour {

    //---------------------------------
    //VARIABLES------------------------
    //---------------------------------

    public GameObject heldItem;
    private Item heldItemScript;

    public float speed = 10;
    
    //---------------------------------
    //FUNCTIONS------------------------
    //---------------------------------

    public void PickUp(GameObject item)
    {
        heldItem = item;
        heldItemScript = heldItem.GetComponent<Item>();
        heldItemScript.PickUp();

        
    }

    public void PutDownOn(GameObject itemLocation)
    {
        heldItemScript.PutDownOn(itemLocation);
        heldItem.transform.parent = itemLocation.transform;
        itemLocation.GetComponent<ItemLocation>().ReceiveItem(heldItem);

        Reset();
    }

    public void EatFood()
    {
        if (heldItem)
        {
            Destroy(heldItem);
            Reset();
        }
    }

    void Reset()
    {
        heldItem = null;
        heldItemScript = null;
    }
}
