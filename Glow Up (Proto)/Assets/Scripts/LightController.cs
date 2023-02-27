using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public List<Light2D> scoreLights = new List<Light2D>();
    public List<BoxLightData> scoreLightData = new List<BoxLightData>();

    [SerializeField]
    private Light2D mainLight;
    [SerializeField]
    private BoxLightData mainBoxLightData;


    private Timer mainLightPulseTimer;
    private Timer mainLightStayTimer;

    public Timer[] pulseTimers = new Timer[3];
    public Timer[] stayTimers = new Timer[3];

    private void Start()
    {
        mainLightPulseTimer = new Timer(mainBoxLightData.pulseTime);
        mainLightPulseTimer.SetTimer();

        mainLightStayTimer = new Timer(mainBoxLightData.stayTime);
        mainLightStayTimer.SetTimer();

        for (int i = 0; i < pulseTimers.Length; i++)
        {
            pulseTimers[i] = new Timer(scoreLightData[i].pulseTime);
            pulseTimers[i].SetTimer();
        }
        for (int i = 0; i < stayTimers.Length; i++)
        {
            stayTimers[i] = new Timer(scoreLightData[i].stayTime);
            stayTimers[i].SetTimer();
        }
    }

    private void FixedUpdate()
    {
        AnimateLight(mainLight, mainBoxLightData, mainLightPulseTimer, mainLightStayTimer);
    }
    public void AnimateLight(Light2D light, BoxLightData lightData, Timer pulseTimer, Timer stayTimer)
    {
        if (!stayTimer.isTimeUp)
        {
            StayLight(light, stayTimer, pulseTimer, lightData);
        }
        else if (!mainLightPulseTimer.isTimeUp)
        {
            AlterLight(light, pulseTimer, stayTimer, lightData);
        }
    }
    public void StayLight(Light2D light, Timer lightTimer, Timer resetTimer, BoxLightData lightData)
    {
        lightTimer.UpdateTimer();

        if (lightTimer.isTimeUp)
        {
            ResetTimer(resetTimer);
        }
    }
    public void AlterLight(Light2D light, Timer lightTimer, Timer resetTimer, BoxLightData lightData)
    {
        int position;
        if (light.intensity > 0)
            position = 0;
        else
            position = 1;

        light.intensity = Mathf.Lerp(light.intensity, lightData.lightIntensity.Evaluate(position) * lightData.maxIntensity, Time.deltaTime);
        lightTimer.UpdateTimer();

        if (lightTimer.isTimeUp)
        {
            ResetTimer(resetTimer);
        }
    }
    void ResetTimer(Timer timer)
    {
        timer.ResetTimer();
    }
}
