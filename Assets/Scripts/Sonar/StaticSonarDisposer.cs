using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SonarSettings))]
public class StaticSonarDisposer : MonoBehaviour
{
    static SimpleSonarShader_Parent sonarParent;
    SonarSettings settings;

    [Range(0f, 30f)]
    public float SonarInterval = 1f;

    void Start()
    {
        if (!sonarParent) sonarParent = FindObjectOfType<SimpleSonarShader_Parent>();
        settings = GetComponent<SonarSettings>();
        settings.PhysicsBased = false;
        StartCoroutine(IntervalUpdate());
    }

    IEnumerator IntervalUpdate()
    {
        yield return new WaitForFixedUpdate();
        for (; ; )
        {
            sonarParent.StartSonarRing(transform.position, settings.impactIntensity, settings.SonarColor);
            yield return new WaitForSeconds(SonarInterval);
        }
    }


}
