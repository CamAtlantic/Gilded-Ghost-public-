﻿using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{
    DayManager _dayManager;
    DreamController _dreamController;

    public Gradient nightDayColor;

    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;

    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;

    public Gradient nightDayFogColor;
    public AnimationCurve fogDensityCurve;
    public float fogScale = 1f;

    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;
    
    float turnSpeed;

    Light mainLight;
    Skybox sky;
    Material skyMat;

    void Start()
    {
        _dayManager = GameObject.Find("MainController").GetComponent<DayManager>();
        _dreamController = GameObject.Find("MainController").GetComponent<DreamController>();
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
    }

    void Update()
    {
        if (_dreamController.loadedScene == Scenes.Cell)
        {
            turnSpeed = 360 / _dayManager.dayLengthSeconds * Time.deltaTime;
            transform.RotateAround(transform.position, transform.right, turnSpeed);

            ColorAndIntensity();
        }


    }

    public void UpdateSunWhileSleeping()
    {
        transform.RotateAround(transform.position, transform.right, 360 / _dayManager.dayLengthSeconds * _dayManager.sleepLengthSeconds);
    }

    void ColorAndIntensity()
    {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;
        
        mainLight.intensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = i;

        mainLight.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = mainLight.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skyMat.SetFloat("_AtmosphereThickness", i);
    }
}