using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour {

    Interaction interactionScript;

    GameObject selector;
    RectTransform rect_Selector;
    RawImage image_Selector;

    GameObject selectRing;
    RectTransform rect_SelectRing;
    RawImage image_SelectRing;

    private float normalScale_SelectRing = 0.05f;
    public float bigScale_SelectRing = 0.15f;
    public float speed_SelectRing = 0.2f;

    private Color normal_Color = Color.white;
    public Color item_Color;
    public Color location_Color;
    public Color interactable_Color;

    private LookingAt reticule = LookingAt.none;

	// Use this for initialization
	void Start () {
        interactionScript = GameObject.Find("Player").GetComponent<Interaction>();
        selectRing = GameObject.Find("SelectRing");
        rect_SelectRing = selectRing.GetComponent<RectTransform>();
        image_SelectRing = selectRing.GetComponent<RawImage>();

        selector = GameObject.Find("Selector");
        rect_Selector = selector.GetComponent<RectTransform>();
        image_Selector = selector.GetComponent<RawImage>();

    }
	
	// Update is called once per frame
	void Update () {
        reticule = interactionScript.reticule;
	
        switch (reticule)
        {
            case LookingAt.item:
                SelectRingLerp(bigScale_SelectRing, item_Color);
                break;

            case LookingAt.itemLocation:
                SelectRingLerp(bigScale_SelectRing, location_Color);
                break;

            case LookingAt.interactable:
                SelectRingLerp(bigScale_SelectRing, interactable_Color);
                break;

            case LookingAt.none:
                SelectRingLerp(normalScale_SelectRing, normal_Color);
                break;
        }
	}

    void SelectRingLerp(float newScale, Color newColor)
    {
        rect_SelectRing.localScale = Vector3.Lerp(rect_SelectRing.localScale, new Vector3(newScale, newScale, newScale), speed_SelectRing);

        image_Selector.color = Color.Lerp(image_Selector.color, newColor, speed_SelectRing);
        image_SelectRing.color = Color.Lerp(image_SelectRing.color, newColor, speed_SelectRing);
    }
}
