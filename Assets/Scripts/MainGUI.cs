using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour {

    Interaction interactionScript;

    GameObject selector;
    RectTransform selector_Rect;
    RawImage selector_Image;

    GameObject selectRing;
    RectTransform selectRing_Rect;
    RawImage selectRing_Image;

    GameObject topEyelid;
    RectTransform topEyelid_Rect;
    GameObject bottomEyelid;
    RectTransform bottomEyelid_Rect;

    float normal_Y_TopEyelid;
    float normal_Y_BottomEyelid;
    float max_Y_TopEyelid = 1000;
    float max_Y_BottomEyelid = -1000;

    float progress_TopEyelid;
    float progress_BottomEyelid;

    public float eyeOpenSpeed = 10;
    [Space(10)]
    [Header("Reticle Controls")]
    private float normalScale_SelectRing = 0.05f;
    public float bigScale_SelectRing = 0.15f;
    public float speed_SelectRing = 0.2f;

    private Color normal_Color = Color.white;
    public Color item_Color;
    public Color location_Color;
    public Color interactable_Color;

    private LookingAt reticule = LookingAt.none;

    private bool hasTakenFirstMousePos = false;
    private Vector2 oldMousePosition;

    public AnimationCurve eyeBlinkCurve;

	// Use this for initialization
	void Start () {
        interactionScript = GameObject.Find("Player").GetComponent<Interaction>();

        selectRing = GameObject.Find("SelectRing");
        selectRing_Rect = selectRing.GetComponent<RectTransform>();
        selectRing_Image = selectRing.GetComponent<RawImage>();
        selector = GameObject.Find("Selector");
        selector_Rect = selector.GetComponent<RectTransform>();
        selector_Image = selector.GetComponent<RawImage>();

        topEyelid = GameObject.Find("TopEyelid");
        topEyelid_Rect = topEyelid.GetComponent<RectTransform>();
        bottomEyelid = GameObject.Find("BottomEyelid");
        bottomEyelid_Rect = bottomEyelid.GetComponent<RectTransform>();

        normal_Y_TopEyelid = topEyelid_Rect.anchoredPosition.y;
        normal_Y_BottomEyelid= bottomEyelid_Rect.anchoredPosition.y;

        print(normal_Y_BottomEyelid + " " + normal_Y_TopEyelid);
    }
	
	// Update is called once per frame
	void Update () {
        reticule = interactionScript.reticule;

        
        //janked this need to fix
        if (Time.timeSinceLevelLoad > 0.1f)
        OpenEyelids();
        
	
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
        selectRing_Rect.localScale = Vector3.Lerp(selectRing_Rect.localScale, new Vector3(newScale, newScale, newScale), speed_SelectRing);

        selector_Image.color = Color.Lerp(selector_Image.color, newColor, speed_SelectRing);
        selectRing_Image.color = Color.Lerp(selectRing_Image.color, newColor, speed_SelectRing);
    }

    void OpenEyelids()
    {
        if(!hasTakenFirstMousePos)
        {
            oldMousePosition = Input.mousePosition;
            hasTakenFirstMousePos = true;
        }

        float delta = 0;//Vector2.Distance(oldMousePosition, Input.mousePosition);
        delta = eyeBlinkCurve.Evaluate(Time.timeSinceLevelLoad);

        topEyelid_Rect.anchoredPosition = new Vector2(topEyelid_Rect.anchoredPosition.x, normal_Y_TopEyelid + delta * 2);
        bottomEyelid_Rect.anchoredPosition = new Vector2(bottomEyelid_Rect.anchoredPosition.x, normal_Y_BottomEyelid - delta);

        oldMousePosition = Input.mousePosition;

    }
}
