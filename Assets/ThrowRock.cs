using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    public GameObject projectile;

    [Range(0.1f, 4f)]
    public float reloadInterval = 0.5f;
    public float forceMultiplier = 100f;
    float cooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldown)
        {
            GameObject obj = GameObject.Instantiate(projectile);
            obj.transform.position = Camera.main.transform.position - Camera.main.transform.up * 0.3f + Camera.main.transform.forward * 0.3f + Camera.main.transform.right * 0.4f;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * forceMultiplier, ForceMode.Force);
            Destroy(obj, 10f);

            cooldown = Time.time + reloadInterval;
        }
    }
}
