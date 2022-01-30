using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarEchoBounces : MonoBehaviour
{
    static SimpleSonarShader_Parent parent;
    private SonarSettings settings;

    public int echoRepititions = 2;
    public float echoDelayBetweenRepititions = 2f;

    AudioSource audio;

    // Start is called before the first frame update
    private void Start()
    {
        if (parent == null) parent = FindObjectOfType<SimpleSonarShader_Parent>();
        settings = GetComponent<SonarSettings>();
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audio) audio.PlayOneShot(audio.clip, collision.impulse.magnitude / 3f);

        if (collision.transform.TryGetComponent(out SimpleSonarShader_ExampleCollision _))
        {
            
            StartCoroutine(Echo(collision.contacts[0].point, collision.impulse.magnitude / 10.0f * settings.impactMultiplier, echoRepititions));
        }
    }

    IEnumerator Echo(Vector3 position, float magnitude, int repititions)
    {
        yield return new WaitForSeconds(echoDelayBetweenRepititions);

        for (int i = 0; i < repititions; i++)
        {
            parent.StartSonarRing(position, magnitude, settings);
            yield return new WaitForSeconds(echoDelayBetweenRepititions);
        }
    }
}
