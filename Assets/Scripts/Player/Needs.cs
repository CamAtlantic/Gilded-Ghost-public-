using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public enum Hunger { Starving, Hungry, Peckish, Full}

public class Needs : MonoBehaviour {
    [HideInInspector]
    new public AudioSource audio;

    GameObject _camObject;
    VignetteAndChromaticAberration _chromAb;
    NoiseAndGrain _noiseAndGrain;

    public static Hunger hungerState;

    public float hungryThreshold = 150;
    public float starvingThreshold;
    
    public float digestionSpeed = 3;

    public static float hunger;
    public float hungerMax;

    public static int hungerInt;

    public float noiseModifier = 20;
    public float chromAbModifier = 75;
    public float lerpSpeed = 0.1f;

    void Awake()
    {
        _camObject = Camera.main.gameObject;
        _chromAb = _camObject.GetComponent<VignetteAndChromaticAberration>();
        _noiseAndGrain = _camObject.GetComponent<NoiseAndGrain>();
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        hungerMax = DayManager.dayLengthSeconds;  
    }

    void Update()
    {
        if (DreamController.loadedScene == Scenes.Cell)
        {
            hunger += Time.deltaTime;
            hungerInt = (int)(hunger / (hungerMax / 3));

            float noiseIntensity= 0;
            float chromAbIntensity = 0;

            if (hungerInt >= 2)
                noiseIntensity = 1;
            if (hungerInt >= 3)
                chromAbIntensity = 1;

            _noiseAndGrain.intensityMultiplier = Mathf.Lerp(_noiseAndGrain.intensityMultiplier, noiseIntensity * noiseModifier, lerpSpeed);
            _noiseAndGrain.generalIntensity = Mathf.Lerp(_noiseAndGrain.intensityMultiplier, noiseIntensity,lerpSpeed);

            _chromAb.chromaticAberration = Mathf.Lerp(_chromAb.chromaticAberration, chromAbIntensity * chromAbModifier, lerpSpeed);

            #region oldHunger;
            /*
            
            if (valueOfEatenFood > 0)
            {
                float amount = Time.deltaTime * digestionSpeed;
                valueOfEatenFood -= amount;
                hunger -= amount;
            }

            float amountPastHungerThreshold = (hunger - hungryThreshold) / (starvingThreshold - hungryThreshold);
            if (amountPastHungerThreshold > 0)
            {
                hungry = true;
                
            }
            else hungry = false;

            float amountPastStarvingThreshold = (hunger - starvingThreshold) / (hungerMax - starvingThreshold);
            if (amountPastStarvingThreshold > 0)
            {
                starving = true;
                
            }
            else
            {
                starving = false;
                _chromAb.chromaticAberration = Mathf.Lerp(_chromAb.chromaticAberration, 0, 0.2f);
            }
            

            #region Hunger Pulse
            if (hungry || starving)
            {
                float rnd = Random.value;
                if (rnd < 0.001f)
                    if (!starving)
                        isHungerPulse = true;
                    else
                        isStarvingPulse = true;
            }

            if (isHungerPulse)
                HungerPulse(amountPastHungerThreshold, 10);
            if (isStarvingPulse)
                HungerPulse(amountPastStarvingThreshold, 5);
            #endregion
    */
            #endregion

        }
    }

    public float valueOfEatenFood = 0;

    public void EatFood(float value)
    {
        hunger -= DayManager.oneThirdDaySeconds;
        audio.PlayOneShot(audio.clip);

    }

    public void UpdateHungerWhileSleeping()
    {
        hunger += DayManager.oneThirdDaySeconds;
    }


    //Hunger Pulse----------------------------------------

//    bool isHungerPulse = false;
  //  bool isStarvingPulse = false;
    public AnimationCurve hungerPulseCurve;
    float pulseTimer = 0;

    void HungerPulse(float baseAmount, float pulseTimerMax)
    {
        float curveEvaluation = hungerPulseCurve.Evaluate(pulseTimer / pulseTimerMax);
        if (curveEvaluation < baseAmount)
            curveEvaluation = baseAmount;

        //if (!starving)
        {
            _noiseAndGrain.intensityMultiplier = curveEvaluation * 10;
            _noiseAndGrain.generalIntensity = curveEvaluation;
        }
        //else
            //this is why there are hunger pulse bugs, because its lowest number is 50x the base amount
            _chromAb.chromaticAberration = curveEvaluation * 50;

        if (pulseTimer > pulseTimerMax)
        {
            pulseTimer = 0;
//            isHungerPulse = false;
  //          isStarvingPulse = false;
        }

        pulseTimer += Time.deltaTime;
    }

    public void ToggleHungerEffects(bool onOff)
    {
        _chromAb.enabled = _noiseAndGrain.enabled = onOff;
    }

}
