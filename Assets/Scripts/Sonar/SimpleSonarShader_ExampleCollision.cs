// SimpleSonarShader scripts and shaders were written by Drew Okenfuss.

using System.Collections;
using UnityEngine;


public class SimpleSonarShader_ExampleCollision : MonoBehaviour
{
    static SimpleSonarShader_Parent parent;
    private void Awake()
    {
        if (parent == null)
            parent = GetComponentInParent<SimpleSonarShader_Parent>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Start sonar ring from the contact point
        if (collision.transform.TryGetComponent(out SonarSettings settings))
        {
            if (settings.PhysicsBased)
                parent.StartSonarRing(collision.contacts[0].point, collision.impulse.magnitude / 10.0f * settings.impactMultiplier, settings.SonarColor);
            else
                parent.StartSonarRing(collision.contacts[0].point, settings.impactIntensity, settings.SonarColor);
        }
    }

    private void OnDestroy()
    {
        parent = null;
    }
}
