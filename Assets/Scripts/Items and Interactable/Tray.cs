using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FoodMenu))]
public class Tray : Interactable {
    Door door;
    FoodMenu _foodMenu;

    Vector3 corridorPosition;
    Vector3 insidePosition;

    public bool inside = false;
    public float pushSpeed = 0.3f;

    ItemLocation[] foodSpaces;

    bool hasItemsOnTray = false;

    public override void Awake()
    {
        base.Awake();
        _foodMenu = GetComponent<FoodMenu>();

        foodSpaces = transform.GetComponentsInChildren<ItemLocation>();
        door = GameObject.Find("Door").GetComponent<Door>();
    }

	// Use this for initialization
	void Start () {
        
        corridorPosition = transform.localPosition;
        insidePosition = new Vector3(corridorPosition.x, corridorPosition.y, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        hasItemsOnTray = false;
        for (int i = 0; i < foodSpaces.Length; i++)
        {
            if (foodSpaces[i].itemAtLocation)
                hasItemsOnTray = true;
        }

        if (hasItemsOnTray)
            gameObject.tag = "Untagged";
        else
            gameObject.tag = "Interactable";

        if (inside)
            transform.localPosition = Vector3.Lerp(transform.localPosition, insidePosition, pushSpeed);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, corridorPosition, pushSpeed);
	}

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        PushOutside();
    }

    //need different audio
    public void PushInside()
    {
        if (inside == false)
        {
            inside = true;
            audio.PlayOneShot(audio.clip);

            for (int i = 0; i < foodSpaces.Length; i++)
            {
                if (foodSpaces[i].locationSize == Size.large)
                    SpawnFood(foodSpaces[i], _foodMenu.mains);
                else
                    SpawnFood(foodSpaces[i], _foodMenu.sides);
            }
        }
    }

    public void PushOutside()
    {
        inside = false;
        audio.PlayOneShot(audio.clip);
        door.CloseTraySlot();
    }

    void SpawnFood(ItemLocation location, GameObject[] mainsOrSides)
    {
        int x = (int)(Random.value * _foodMenu.sides.Length);

        GameObject temp = Instantiate(mainsOrSides[x]);
        location.ReceiveItem(temp);
        temp.transform.localPosition = location.placedPosition;
        location.itemAtLocation = temp;
    }
}
