using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour {

    GameObject selector;
    RectTransform rect_Selector;
    RawImage image_Selector;

    GameObject selectRing;
    RectTransform rect_SelectRing;
    RawImage image_SelectRing;

    bool lookingAtInteractable = false;
    private float normalScale_SelectRing = 0.05f;
    public float bigScale_SelectRing = 0.15f;
    public float speed_SelectRing = 0.2f;

    public Color color_Select;

	// Use this for initialization
	void Start () {
        selectRing = GameObject.Find("SelectRing");
        rect_SelectRing = selectRing.GetComponent<RectTransform>();
        image_SelectRing = selectRing.GetComponent<RawImage>();

        selector = GameObject.Find("Selector");
        rect_Selector = selector.GetComponent<RectTransform>();
        image_Selector = selector.GetComponent<RawImage>();


    }
	
	// Update is called once per frame
	void Update () {
	
        if(lookingAtInteractable)
        {
            rect_SelectRing.localScale = Vector3.Lerp(rect_SelectRing.localScale, new Vector3(bigScale_SelectRing, bigScale_SelectRing, bigScale_SelectRing), speed_SelectRing);

            image_Selector.color = Color.Lerp(image_Selector.color, color_Select, speed_SelectRing);
            image_SelectRing.color = Color.Lerp(image_SelectRing.color, color_Select, speed_SelectRing);
        }
        else
        {
            rect_SelectRing.localScale = Vector3.Lerp(rect_SelectRing.localScale, new Vector3(normalScale_SelectRing, normalScale_SelectRing, normalScale_SelectRing), speed_SelectRing);

            image_Selector.color = Color.Lerp(image_Selector.color, Color.white, speed_SelectRing);
            image_SelectRing.color = Color.Lerp(image_SelectRing.color, Color.white, speed_SelectRing);
        }
	}

    public void ShowSelectRing()
    {
        if (!lookingAtInteractable)
        {
            lookingAtInteractable = true;
        }
    }
    public void HideSelectRing()
    {
        if (lookingAtInteractable)
        {
            lookingAtInteractable = false;
        }
    }
}
