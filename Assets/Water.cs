using UnityEngine;
using System.Collections;

public class Water : DreamTrigger {

    public float amplitudeY = 0.5f;
    public float omegaY = 1;
    public float index;

    bool playerInWater = false;
    float playerInWaterTime;
    public float playerInWaterTimeMax = 5;

    SleepingAndWaking r_sleepScript;

    Vector3 movePosition;
    public override void Awake()
    {
        base.Awake();
        tag = "Untagged";
        r_sleepScript = player.GetComponent<SleepingAndWaking>();

        movePosition = transform.position;
    }

   public override void Update()
    {
        index += Time.deltaTime;
        float y = amplitudeY * Mathf.Sin(omegaY * index);

        if (r_sleepScript.sleepState == SleepState.standing)
        {
            float moveAmount = (Time.deltaTime / 24);
            movePosition += new Vector3(0,moveAmount + y,0);

            transform.position = Vector3.Lerp(transform.position, movePosition, 0.9f);
        }

        if(!playerInWater && playerInWaterTime > 0)
            playerInWaterTime -= Time.deltaTime * 2;
        if (playerInWaterTime < 0)
            playerInWaterTime = 0;
    }
    
    void OnTriggerStay()
    {
        playerInWater = true;
        playerInWaterTime += Time.deltaTime;

        if(playerInWaterTime > playerInWaterTimeMax)
        {
            DreamTriggerEffect();
            TriggerLieDown();
        }
    }

    void OnTriggerExit()
    {
        playerInWater = false;
    }

    public override void DreamTriggerEffect()
    {
        base.DreamTriggerEffect();
        r_dreamController.columns_water = true;
        print("Water Triggered");
    }
}
