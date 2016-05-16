using UnityEngine;
using System.Collections;

public class Button : Interactable {

    public bool pressed = false;

    Vector3 startPos;
    Vector3 endPos;

    private float lerpSpeed = 0.4f;
    private float timer = 0;
    private float timerMax = 3;

	// Use this for initialization
	public virtual void Start () {
        startPos = Vector3.forward;
        endPos = new Vector3(startPos.x, startPos.y, startPos.z - 2.5f);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (pressed)
            tag = "Untagged";
        timer += Time.deltaTime;

        if (pressed)
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, lerpSpeed);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, lerpSpeed);

        
        if (timer > timerMax)
        {
            tag = "Interactable";
            timer = 0;
            pressed = false;
        }
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();

        if (!pressed)
            pressed = true;
    }
}
