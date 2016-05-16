using UnityEngine;
using System.Collections;

public class SinkButton : Button {
    GameObject water;
    Material waterMaterial;

    Color fadeOutColor;
    Color fadeInColor;

    public override void Awake()
    {
        base.Awake();
        water = GameObject.Find("Water");
        waterMaterial = water.GetComponent<MeshRenderer>().material;
    }
    public override void Start()
    {
        base.Start();
        fadeInColor = waterMaterial.color;
        fadeInColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0.75f);
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
        
    }
}
