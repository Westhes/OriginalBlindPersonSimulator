using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    static SimpleSonarShader_Parent sonarParent;
    public SonarSettings left;
    public SonarSettings right;

    public void Start()
    {
        sonarParent = FindObjectOfType<SimpleSonarShader_Parent>();
    }

    public void Trigger(int value)
    {
        //Debug.Log("Got triggered " + value);
        if (value == 1)
            sonarParent.StartSonarRing(left.transform.position, left.impactIntensity, left);
        else if (value == 2)
            sonarParent.StartSonarRing(right.transform.position, right.impactIntensity, right);
    }
}
