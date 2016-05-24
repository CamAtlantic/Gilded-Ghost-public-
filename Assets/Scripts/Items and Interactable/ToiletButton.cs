﻿using UnityEngine;
using System.Collections;

public class ToiletButton : Button {

    ItemLocation inToilet;
    GameObject toiletWater;
    Vector3 waterUp;
    Vector3 waterDown;

    int flushCount = 0;
    Vector3 flushVector = new Vector3(0, -0.05f, 0);

    public override void Awake()
    {
        base.Awake();
        toiletWater = GameObject.Find("ToiletWater");
        inToilet = GameObject.Find("InToilet").GetComponent<ItemLocation>();
    }

    public override void Start()
    {
        base.Start();
        waterUp = Vector3.zero;
        waterDown = Vector3.forward * -0.07f;
    }

    public override void Update()
    {
        base.Update();
        if (pressed)
        {
            print(pressed + " " + toiletWater.transform.localPosition.z);
            toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, waterDown, 0.4f);

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
            toiletWater.transform.localPosition = Vector3.Lerp(toiletWater.transform.localPosition, waterUp, 0.3f);

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
