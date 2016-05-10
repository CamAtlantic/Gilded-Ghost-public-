using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpItem : MonoBehaviour {

    //---------------------------------
    //VARIABLES------------------------
    //---------------------------------

    public GameObject heldItem;
    private Item heldItemScript;
    Needs _needs;

    public float speed = 10;


    /// <summary>
    /// this really should be coming from the Item script
    /// </summary>
    public float foodHungerValue = 75;
    
    //---------------------------------
    //FUNCTIONS------------------------
    //---------------------------------

    public void PickUp(GameObject item)
    {
        heldItem = item;
        heldItemScript = heldItem.GetComponent<Item>();
        heldItemScript.PickUp();
        _needs = GetComponent<Needs>();
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
            _needs.EatFood(foodHungerValue);
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
