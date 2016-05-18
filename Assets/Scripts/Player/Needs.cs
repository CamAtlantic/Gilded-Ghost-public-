using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Needs : MonoBehaviour {

    DreamController _dreamController;
    DayManager _dayManager;
    GameObject _camObject;
    VignetteAndChromaticAberration _chromAb;
    NoiseAndGrain _noiseAndGrain;

    public float hunger;
    public float hungryThreshold = 150;
    public float starvingThreshold;
    public float hungerMax;

    public bool hungry;
    public bool starving;

    public float digestionSpeed = 3;

    void Awake()
    {
        _dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        _dayManager = GameObject.Find("MainController").GetComponent<DayManager>();
        _camObject = Camera.main.gameObject;
        _chromAb = _camObject.GetComponent<VignetteAndChromaticAberration>();
        _noiseAndGrain = _camObject.GetComponent<NoiseAndGrain>();
    }

    void Start()
    {
        hungryThreshold = _dayManager.dayLengthSeconds * 0.5f;
        hungerMax = hungryThreshold * 10;
        starvingThreshold = hungryThreshold * 2;

        //hunger = hungryThreshold * 0.5f;
    }

    void Update()
    {
        if (_dreamController.loadedScene == Scenes.Cell)
        {
            hunger += Time.deltaTime;

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
                _noiseAndGrain.intensityMultiplier = amountPastHungerThreshold * 10;
                _noiseAndGrain.generalIntensity = amountPastHungerThreshold;
            }
            else hungry = false;

            float amountPastStarvingThreshold = (hunger - starvingThreshold) / (hungerMax - starvingThreshold);
            if (amountPastStarvingThreshold > 0)
            {
                starving = true;
                _chromAb.chromaticAberration = amountPastStarvingThreshold * 150;
            }
            else
            {
                starving = false;
                _chromAb.chromaticAberration = Mathf.Lerp(_chromAb.chromaticAberration, 0, 0.2f);
            }

            #region Hunger Pulse
            float rnd = Random.value;
            if (rnd < 0.001f)
                if (!starving)
                    isHungerPulse = true;
                else
                    isStarvingPulse = true;

            if (isHungerPulse)
                HungerPulse(amountPastHungerThreshold, 10);
            if (isStarvingPulse)
                HungerPulse(amountPastStarvingThreshold, 5);
            #endregion
        }
    }

    public float valueOfEatenFood = 0;
    public void EatFood(float value)
    {
        valueOfEatenFood += value;
    }

    public void UpdateHungerWhileSleeping()
    {
        hunger += _dayManager.sleepLengthSeconds / 2;
    }

    //Hunger Pulse----------------------------------------

    bool isHungerPulse = false;
    bool isStarvingPulse = false;
    public AnimationCurve hungerPulseCurve;
    float pulseTimer = 0;

    void HungerPulse(float baseAmount, float pulseTimerMax)
    {
        float curveEvaluation = hungerPulseCurve.Evaluate(pulseTimer / pulseTimerMax);
        if (curveEvaluation < baseAmount)
            curveEvaluation = baseAmount;

        if (!starving)
        {
            _noiseAndGrain.intensityMultiplier = curveEvaluation * 10;
            _noiseAndGrain.generalIntensity = curveEvaluation;
        }
        else
            //this is why there are hunger pulse bugs, because its lowest number is 50x the base amount
            _chromAb.chromaticAberration = curveEvaluation * 50;

        if (pulseTimer > pulseTimerMax)
        {
            pulseTimer = 0;
            isHungerPulse = false;
            isStarvingPulse = false;
        }

        pulseTimer += Time.deltaTime;
    }

    public void ToggleHungerEffects(bool onOff)
    {
        _chromAb.enabled = _noiseAndGrain.enabled = onOff;
    }

}
