using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SonarSettings))]
public class StaticSonarDisposer : MonoBehaviour
{
    static SimpleSonarShader_Parent sonarParent;
    SonarSettings settings;
    Vector3 SonarStartPosition;

    [Range(0f, 30f)]
    public float SonarInterval = 1f;

    void Start()
    {
        if (!sonarParent) sonarParent = FindObjectOfType<SimpleSonarShader_Parent>();
        settings = GetComponent<SonarSettings>();
        settings.PhysicsBased = false;
        SonarStartPosition = transform.position;
        StartCoroutine(IntervalUpdate());
    }

    IEnumerator IntervalUpdate()
    {
        yield return new WaitForFixedUpdate();
        for (; ; )
        {
            sonarParent.StartSonarRing(SonarStartPosition, settings.impactIntensity, settings.SonarColor);
            yield return new WaitForSeconds(SonarInterval);
        }
    }


}
