using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLights : MonoBehaviour
{
    public bool enabled = true;
    private void Awake()
    {
        if (!enabled) return;

        Light[] lights = FindObjectsOfType<Light>();
        foreach(var light in lights)
        {
            if (light.enabled && light.type == LightType.Directional)
                light.enabled = false;
        }

        Debug.Log("Disabled all directional lights!");
    }
}
