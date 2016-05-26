using UnityEngine;
using System.Collections;

public class ToiletButton : Button {
    DreamController r_dreamController;
    ItemLocation inToilet;
    GameObject toiletWater;

    Vector3 waterUp;
    Vector3 waterDown;

    Vector3 floodPosition = new Vector3(0,0.06f,0.14f);
    Vector3 floodScale = new Vector3(1.5f,1.5f,1.5f);
    Vector3 normalScale;

    int flushCount = 0;
    Vector3 flushVector = new Vector3(0, -0.05f, 0);

    public override void Awake()
    {
        base.Awake();
        toiletWater = GameObject.Find("ToiletWater");
        inToilet = GameObject.Find("InToilet").GetComponent<ItemLocation>();
        r_dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
    }

    public override void Start()
    {
        base.Start();
        waterUp = Vector3.zero;
        waterDown = Vector3.forward * -0.07f;
        normalScale = Vector3.one;
    }

    public override void Update()
    {
        base.Update();
        if (pressed)
        {
            if (r_dreamController.columns_water)
            {
                toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, waterUp, 0.4f);
                toiletWater.transform.localScale = Vector3.Lerp(toiletWater.transform.localScale, normalScale, 0.4f);
            }
            else
            {
                toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, waterDown, 0.4f);
            }
            if (inToilet.itemAtLocation)
            {
                Vector3 toiletFlushLocation = flushVector * (flushCount + 1);

                if (flushCount >= 3)
                {
                    toiletFlushLocation = new Vector3(0, -0.4f, 0);
                }
                inToilet.itemAtLocation.transform.localPosition = Vector3.Lerp(inToilet.itemAtLocation.transform.localPosition, toiletFlushLocation, 0.3f);
            }
        }
        else
        {
            if (r_dreamController.columns_water)
            {
                toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, floodPosition, 0.4f);
                toiletWater.transform.localScale = Vector3.Lerp(toiletWater.transform.localScale, floodScale, 0.4f);
            }
            else
            {
                toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, waterUp, 0.3f);
            }

            if(flushCount >= 3)
            {
                flushCount = 0;
                Destroy(inToilet.itemAtLocation);
                inToilet.Reset();
            }
            if(!inToilet.itemAtLocation)
            {
                flushCount = 0;
            }
        }
    }
    
    public override void InteractTrigger()
    {
        base.InteractTrigger();
        flushCount++;
    }
}
