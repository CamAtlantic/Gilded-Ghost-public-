using UnityEngine;
using System.Collections;

public class HungerIconControl : MonoBehaviour {

    Icon[] childIcons;

    Icon one;
    Icon two;
    Icon three;
    Icon ring;

//    private bool displaying = false;
    private float displayTimer;
    public float displayTimerMax = 5;

    void Awake()
    {
        childIcons = GetComponentsInChildren<Icon>();

        one = childIcons[0];
        two = childIcons[1];
        three = childIcons[2];
        ring = childIcons[3];
    }

    // Use this for initialization
    void Start() {
        ShowAllIcons();
        ring.HideIcon();
    }

    // Update is called once per frame
    void Update()
    {
        if (DreamController.loadedScene == Scenes.Cell)
        {
            ShowAllIcons();
            if (Needs.hungerInt < 3)
                ring.HideIcon();
            if (Needs.hungerInt >= 1)
                three.HideIcon();
            if (Needs.hungerInt >= 2)
                two.HideIcon();
            if (Needs.hungerInt >= 3)
                one.HideIcon();
        }
        else
            HideAllIcons();
    }
       
    private void HideAllIcons()
    {
        foreach (Icon icon in childIcons)
            icon.HideIcon();
    }

    private void ShowAllIcons()
    {
        foreach (Icon icon in childIcons)
            icon.ShowIcon();
    }

}
