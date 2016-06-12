using UnityEngine;
using System.Collections;

public class Water : DreamTrigger {

    GameObject floodWater;
    GameObject toiletWater;
    SleepingAndWaking r_sleepScript;

    public float amplitudeY = 0.5f;
    public float omegaY = 1;
    public float index;

    bool playerInWater = false;
    float playerInWaterTime;
    public float playerInWaterTimeMax = 5;

    public static float playerDrownAmount;

    Vector3 movePosition;
    public override void Awake()
    {
        base.Awake();
        tag = "Untagged";
        floodWater = GameObject.Find("FloodWater");
        toiletWater = GameObject.Find("ToiletWater");
        r_sleepScript = GameObject.Find("Player").GetComponent<SleepingAndWaking>();

        movePosition = transform.position;
    }

   public override void Update()
    {
        index += Time.deltaTime;
        float y = amplitudeY * Mathf.Sin(omegaY * index);
        playerDrownAmount = playerInWaterTime / playerInWaterTimeMax;

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

        if (player.transform.position.y < -50)
            WaterEffect();
    }
    
    void OnTriggerStay()
    {
        WaterEffect();
    }

    void WaterEffect()
    {
        playerInWater = true;
        playerInWaterTime += Time.deltaTime;

        if (playerInWaterTime > playerInWaterTimeMax)
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
        r_dreamText.SetDreamText(r_dreamText.columns_water);

        floodWater.transform.localPosition = new Vector3(floodWater.transform.localPosition.x, 0.015f, floodWater.transform.localPosition.z);

        toiletWater.transform.localPosition = new Vector3(toiletWater.transform.localPosition.x, 0.06f, 0.14f);
        toiletWater.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
