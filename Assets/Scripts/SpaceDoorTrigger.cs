using UnityEngine;
using System.Collections;

public class SpaceDoorTrigger : DreamTrigger {

    Door door;
    GameObject lens;
    GameObject mountain;
    GameObject valley2;

    float timer;
    float timerMax = 5;

    bool steppedThrough;

    public override void Awake()
    {
        base.Awake();
        tag = "Untagged";
        lens = GameObject.Find("Lens");
        door = GameObject.Find("Door").GetComponent<Door>();
        mountain = GameObject.Find("Mountain");
        valley2 = GameObject.Find("Valley2");
    }

    public override void Update()
    {
        base.Update();
        if (steppedThrough)
        {
            timer += Time.deltaTime;
            if(timer > timerMax)
            {
                TriggerLieDown();
            }
        }

    }

    void OnTriggerEnter()
    {
        lens.GetComponent<Renderer>().material.SetInt("_StencilMask", 2);
        steppedThrough = true;
        mountain.GetComponent<Collider>().enabled = false;
        valley2.GetComponent<Collider>().enabled = false;
        DreamTriggerEffect();
    }


    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_dreamController.mountain_door = true;
        
        r_dreamText.SetDreamText(r_dreamText.mountain_door);
        door.DramaOpenDoor();
    }

}
