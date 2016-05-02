using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Needs : MonoBehaviour {

    DayManager _dayManager;
    GameObject _camObject;
    VignetteAndChromaticAberration _chromAb;
    NoiseAndGrain _noiseAndGrain;

    public float hunger = 0;
    public float hungerThreshold = 60;
    public float starvingThreshold;
    public float hungerMax;

    public bool hungry;
    public bool starving;

    void Start()
    {
        _dayManager = GameObject.Find("MainController").GetComponent<DayManager>();
        _camObject = Camera.main.gameObject;
        _chromAb = _camObject.GetComponent<VignetteAndChromaticAberration>();
        _noiseAndGrain = _camObject.GetComponent<NoiseAndGrain>();
    }

    void Update()
    {
        hunger += Time.deltaTime;
        hungerThreshold = _dayManager.secondsInDay * 0.5f;
        hungerMax = hungerThreshold * 10;
        starvingThreshold = hungerThreshold * 2;

        float amountPastHungerThreshold = (hunger - hungerThreshold) / (starvingThreshold - hungerThreshold);
        if (amountPastHungerThreshold > 0)
        {
            hungry = true;
            _noiseAndGrain.intensityMultiplier = amountPastHungerThreshold * 10;
            _noiseAndGrain.generalIntensity = amountPastHungerThreshold;

            float rnd = Random.value;
            if (rnd < 0.001f)
                if (!starving)
                    isHungerPulse = true;
                else
                    isStarvingPulse = true;
        }

        float amountPastStarvingThreshold = (hunger - starvingThreshold) / (hungerMax - starvingThreshold);
        if (amountPastStarvingThreshold > 0)
        {
            starving = true;
            _chromAb.chromaticAberration = amountPastStarvingThreshold * 150;
        }
        
        if(isHungerPulse)
            HungerPulse(amountPastHungerThreshold,10);
        if (isStarvingPulse)
            HungerPulse(amountPastStarvingThreshold,5);
    }

    bool isHungerPulse = false;
    bool isStarvingPulse = false;

    public AnimationCurve hungerPulseCurve;
    float pulseTimer = 0;

    void HungerPulse(float baseAmount, float pulseTimerMax)
    {
        float curveEvaluation = hungerPulseCurve.Evaluate(pulseTimer / pulseTimerMax);
        if (curveEvaluation < baseAmount)
            curveEvaluation = baseAmount;

        _noiseAndGrain.intensityMultiplier = curveEvaluation * 10;
        _noiseAndGrain.generalIntensity = curveEvaluation;

        if(starving)
            _chromAb.chromaticAberration = curveEvaluation * 50;

        if (pulseTimer > pulseTimerMax)
        {
            pulseTimer = 0;
            isHungerPulse = false;
            isStarvingPulse = false;
        }
        pulseTimer += Time.deltaTime;
    }
}
