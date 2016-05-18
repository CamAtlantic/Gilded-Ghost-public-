using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public GameObject player;
    public Color guiColor;

    public virtual void Awake()
    {
       tag = "Interactable";

       player = GameObject.Find("Player");
    }

    public virtual void InteractTrigger()
    {
        //do something when clicked on
    }

    public virtual void Update()
    {

    }

}