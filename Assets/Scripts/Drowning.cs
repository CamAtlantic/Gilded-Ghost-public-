using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Drowning : MonoBehaviour {
    RectTransform rect;
    Image image;    
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update ()
    {
	if(DreamController.loadedScene == Scenes.Columns)
        {
            image.enabled = true;
            rect.anchoredPosition = new Vector2(0, Mathf.Lerp(-1000, 0, Water.playerDrownAmount));
        }
    else
        {
            image.enabled = false;
        }
	}
}
