using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Icon : MonoBehaviour {

    GameObject player;
    RawImage image;

    Color hiddenColor;
    Color shownColor;

    public bool isIconShown;
    public bool hasBeenCleared;

    public virtual void Awake()
    {
        player = GameObject.Find("Player");
        image = GetComponent<RawImage>();

        hiddenColor = image.color;
        shownColor = new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 1);
    }

    public virtual void Update()
    {
        if (hasBeenCleared)
            HideIcon();

        if(isIconShown)
            image.color = Color.Lerp(image.color, shownColor, 0.2f);
        else
            image.color = Color.Lerp(image.color, hiddenColor, 0.2f);
    }

    public virtual void ShowIcon()
    {
        if (hasBeenCleared)
            return;
        isIconShown = true;
    }

    public virtual void HideIcon()
    {
        isIconShown = false;
    }

    public virtual void Clear()
    {
        hasBeenCleared = true;
    }

}
