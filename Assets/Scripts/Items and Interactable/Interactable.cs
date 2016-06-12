using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public GameObject player;
    public Color guiColor;
    [HideInInspector]
    new public AudioSource audio;


    public virtual void Awake()
    {
       tag = "Interactable";

       player = GameObject.Find("Player");
        audio = GetComponent<AudioSource>();

    }

    public virtual void InteractTrigger()
    {
        //do something when clicked on
    }
    
}