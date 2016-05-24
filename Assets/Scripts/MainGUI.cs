using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour {

    Interaction interactionScript;
    DreamController r_dreamController;

    GameObject selector;
    RawImage selector_Image;

    GameObject selectRing;
    RectTransform selectRing_Rect;
    RawImage selectRing_Image;

    GameObject selectEye;
    RectTransform selectEye_Rect;
    RawImage selectEye_Image;

    [Space(10)]
    [Header("Reticle Controls")]
    private float normalScale_SelectRing = 0.05f;
    public float bigScale_SelectRing = 0.15f;
    public float speed_SelectRing = 0.2f;

    private Color normal_Color = Color.white;
    public Color item_Color;
    public Color location_Color;
    public Color interactable_Color;
    [Space(10)]
    public Color lookingAtFireColor;

    private LookingAt reticule = LookingAt.none;

    DontLookAtFire r_fireScript;
    public float fireReticleSize;

	// Use this for initialization
	void Start () {
        interactionScript = GameObject.Find("Player").GetComponent<Interaction>();
        r_dreamController = GameObject.Find("MainController").GetComponent<DreamController>();

        selectRing = GameObject.Find("SelectRing");
        selectRing_Rect = selectRing.GetComponent<RectTransform>();
        selectRing_Image = selectRing.GetComponent<RawImage>();

        selector = GameObject.Find("Selector");
        selector_Image = selector.GetComponent<RawImage>();

        selectEye = GameObject.Find("SelectEye");
        selectEye_Rect = selectEye.GetComponent<RectTransform>();
        selectEye_Image = selectEye.GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update () {

        switch (Interaction.reticule)
        {
            case LookingAt.item:
                SelectRingLerp(bigScale_SelectRing, item_Color);
                break;

            case LookingAt.itemLocation:
                SelectRingLerp(bigScale_SelectRing, location_Color);
                break;

            case LookingAt.interactable:

                //expandable to any interactable but only for trunk lid right now
                #region chooseColorFromObject
                Color tempColor = interactable_Color;
                if (interactionScript.objectHit)
                {
                    if (interactionScript.objectHit.GetComponent<TrunkLid>())
                        tempColor = interactionScript.objectHit.GetComponent<Interactable>().guiColor;
                }
                #endregion

                SelectRingLerp(bigScale_SelectRing, tempColor);
                
                break;

            case LookingAt.none:
                SelectRingLerp(normalScale_SelectRing, normal_Color);
                IconLerp(selectEye, normalScale_SelectRing, normal_Color);

                break;
        }

        if(DreamController.loadedScene == Scenes.Fire)
        {
            if (r_fireScript == null && GameObject.Find("Fire"))
            {
                r_fireScript = GameObject.Find("Fire").GetComponent<DontLookAtFire>();
            }
            if (r_fireScript != null && reticule != LookingAt.interactable)
            {
                float ringScale = normalScale_SelectRing + (1 / r_fireScript.abs_Sum) * 10;
                ringScale = Mathf.Clamp(ringScale, 0, 1f);
                Color ringColor = normal_Color;
                if (r_fireScript.lookingAtFire)
                    ringColor = lookingAtFireColor;
                IconLerp(selectEye, ringScale, ringColor);
            }
        }
	}

    void SelectRingLerp(float newScale, Color newColor)
    {
        IconLerp(selectRing, newScale, newColor);
    }

    void IconLerp(GameObject icon, float newScale,Color newColor)
    {
        RectTransform rect;
        RawImage image;

        if (icon == selectRing)
        {
            rect = selectRing_Rect;
            image = selectRing_Image;
        }
        else
        {
            //this should be fine as i'm only using two different things with this function
            rect = selectEye_Rect;
            image = selectEye_Image;
        }
        
        rect.localScale = Vector3.Lerp(rect.localScale, new Vector3(newScale, newScale, newScale), speed_SelectRing);

        selector_Image.color = Color.Lerp(selector_Image.color, newColor, speed_SelectRing);
        image.color = Color.Lerp(image.color, newColor, speed_SelectRing);

    }

}
