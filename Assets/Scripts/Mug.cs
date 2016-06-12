using UnityEngine;
using System.Collections;

public class Mug : Item {

    Vector3 upPos = Vector3.zero;
    Vector3 downPos = new Vector3(0, 0, -0.9f);

    GameObject water;
    Renderer waterRend;

    bool full;

    public override void Awake()
    {
        base.Awake();
         water = GameObject.Find("MugWater");
        waterRend = GameObject.Find("MugWater").GetComponent<Renderer>();
        waterRend.enabled = false;
    }

    public void Fill(bool onOff)
    {
        full = onOff;
        if (onOff)
            waterRend.enabled = true;
        print(onOff);
    }

    public override void Use()
    {
        base.Use();
        Fill(false);
    }

    public override void Update()
    {
        base.Update();

        if(transform.GetComponentInParent<ItemLocation>())
        {
            if (transform.parent.name == "InToilet")
            {
                Fill(true);
            }
        }

        if (full)
        {
            water.transform.localPosition = Vector3.Lerp(water.transform.localPosition, upPos, 0.3f);

        }
        else
        {
            water.transform.localPosition = Vector3.Lerp(water.transform.localPosition, downPos, 0.3f);
            if (water.transform.localPosition.z < -0.85f)
                waterRend.enabled = false;
        }
    }

    public override void PickUp()
    {
        base.PickUp();
        audio.PlayOneShot(audio.clip);
    }


    public override void PutDownOn(GameObject itemLocation)
    {
        base.PutDownOn(itemLocation);
        audio.PlayOneShot(audio.clip);
    }
}
