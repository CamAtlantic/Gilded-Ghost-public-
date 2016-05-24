using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpItem : MonoBehaviour
{

    //---------------------------------
    //VARIABLES------------------------
    //---------------------------------

    public static GameObject heldItem;
    private Item heldItemScript;
    Needs _needs;
    Icon e_Icon;

    public float speed = 10;
    /// <summary>
    /// this really should be coming from the Item script
    /// </summary>
    public float foodHungerValue = 75;

    //---------------------------------
    //FUNCTIONS------------------------
    //---------------------------------

    void Awake()
    {
        e_Icon = GameObject.Find("E_Icon").GetComponent<Icon>();
    }

    public void PickUp(GameObject item)
    {
        heldItem = item;
        heldItemScript = heldItem.GetComponent<Item>();
        heldItemScript.PickUp();
        _needs = GetComponent<Needs>();
        e_Icon.ShowIcon();
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

            if (!e_Icon.hasBeenCleared)
                e_Icon.Clear();

            Reset();
        }
    }

    void Reset()
    {
        heldItem = null;
        heldItemScript = null;
        e_Icon.HideIcon();
    }
}
