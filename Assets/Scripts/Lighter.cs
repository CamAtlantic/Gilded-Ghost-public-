using UnityEngine;
using System.Collections;

public class Lighter : Item {

    GameObject cap;
    GameObject flame;
    new Light light;

    bool open = false;

    Vector3 largeFlamePos;
    Vector3 smallFlamePos;

    public override void Awake()
    {
        base.Awake();
        cap = transform.FindChild("Cap").gameObject;
        flame = transform.FindChild("Flame").gameObject;
        largeFlamePos = flame.transform.localPosition;
        smallFlamePos = new Vector3(largeFlamePos.x, largeFlamePos.y, 0.02331f);
        light = transform.FindChild("Light").gameObject.GetComponent<Light>();
    }

    public override void Update()
    {
        base.Update();
        if (open)
        {
            cap.transform.localRotation = Quaternion.Lerp(cap.transform.localRotation, Quaternion.Euler(85, 0, 0), 0.3f);

            flame.transform.localPosition = Vector3.Lerp(flame.transform.localPosition, largeFlamePos, 0.3f);
            flame.transform.localScale = Vector3.Lerp(flame.transform.localScale, Vector3.one, 0.3f);
        }
        else
        {
            cap.transform.localRotation = Quaternion.Lerp(cap.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.3f);

            flame.transform.localPosition = Vector3.Lerp(flame.transform.localPosition, smallFlamePos, 0.3f);
            flame.transform.localScale = Vector3.Lerp(flame.transform.localScale, Vector3.one / 10, 0.3f);
        }
        light.enabled = open;
    }

    public override void Use()
    {
        base.Use();
        open = !open;
    }

}
