using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleScript : MonoBehaviour {

    public GameObject handleTarget;

    Text text;
    GameObject dot;
    public RectTransform dotRect;
    Image dotImage;

    // Use this for initialization
    void Start () {
        text = transform.FindChild("Text").GetComponent<Text>();

        dot = transform.FindChild("Dot").gameObject;
        dotRect = dot.GetComponent<RectTransform>();
        dotImage = dot.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	 
        if(text == null)
        {
            
        }
	}

    /// <summary>
    /// The new alpha value between 0 and 1 of the handle.
    /// </summary>
    /// <param name="newAlpha"></param>
    public void SetAlpha(float newAlpha)
    {
        if (text != null && dot != null)
        {
            //print(alpha2);
            print(text);
            Color newColor = text.color;
            newColor.a = newAlpha;
            text.color = newColor;
            dotImage.color = new Color(dotImage.color.r, dotImage.color.g, dotImage.color.b, newAlpha);
        
        }
        else
        {
            print("is null");
        }
    }

    
}
