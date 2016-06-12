using UnityEngine;
using System.Collections;

public class SinkButton : Button {
    ItemLocation sinkLocation;
    GameObject water;
    Material waterMaterial;
    Mug mug;

    Color fadeOutColor;
    Color fadeInColor;

    public override void Awake()
    {
        base.Awake();
        water = GameObject.Find("Water");
        waterMaterial = water.GetComponent<MeshRenderer>().material;
        sinkLocation = GameObject.Find("SinkLocation").GetComponent<ItemLocation>();
        mug = GameObject.Find("Mug").GetComponent<Mug>();
    }
    public override void Start()
    {
        base.Start();
        fadeOutColor = waterMaterial.color;
        fadeInColor = new Color(fadeOutColor.r, fadeOutColor.g, fadeOutColor.b, 0.75f);
    }
    public override void Update()
    {
        base.Update();
        if(pressed)
            waterMaterial.color = Color.Lerp(waterMaterial.color, fadeInColor, 0.3f);
        else
           waterMaterial.color = Color.Lerp(waterMaterial.color, fadeOutColor, 0.3f);

    }
    public override void InteractTrigger()
    {
        base.InteractTrigger();
        audio.PlayOneShot(audio.clip);
        
        if (sinkLocation.itemAtLocation && sinkLocation.itemAtLocation.name == "Mug")
        {
            mug.Fill(true);
        }
    }
}
