using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpItem : MonoBehaviour
{
    //---------------------------------
    //VARIABLES------------------------
    //---------------------------------

    public static GameObject heldItem;
    public static Item heldItemScript;
    public static Icon e_Icon;

    public float speed = 10;

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
        e_Icon.ShowIcon();
    }

    public void PutDownOn(GameObject itemLocation)
    {
        heldItemScript.PutDownOn(itemLocation);
        heldItem.transform.parent = itemLocation.transform;
        itemLocation.GetComponent<ItemLocation>().ReceiveItem(heldItem);

        Reset();
    }

    public static void Reset()
    {
        heldItem = null;
        heldItemScript = null;
        e_Icon.HideIcon();
    }
}
