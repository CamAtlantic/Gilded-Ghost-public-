using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Needs : MonoBehaviour {

    FirstPersonController FPScontroller;
    [SerializeField]
    private SleepingAndWaking n_sleepingAndWaking = new SleepingAndWaking();
    
	// Use this for initialization
	void Start () {
        FPScontroller = GetComponent<FirstPersonController>();
	}

    // Update is called once per frame
    void Update()
    {

    }
}
