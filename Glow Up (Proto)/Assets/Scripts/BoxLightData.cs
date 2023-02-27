using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxLightData", menuName = "Data/Light Data/ Box Light Data")]
public class BoxLightData : ScriptableObject
{
    public AnimationCurve lightIntensity;
    public float maxIntensity = 1f;
    public float pulseTime = 1.2f;
    public float stayTime = .8f;
}
