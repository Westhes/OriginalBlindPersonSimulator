using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarSettings : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color SonarColor = Color.white;
    public bool PhysicsBased = true;
    [Tooltip("Incase PhysicsBased is enabled this value is ignored.")]
    public float impactIntensity = 1f;
    public float impactMultiplier = 1f;

    public float colorIntensity = 1f;
    public float ringspeed = 1f;
    public float ringWidth = 0.5f;
    public float ringIntensityScale = 3f;
}
