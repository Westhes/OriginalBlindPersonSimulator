using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSound : MonoBehaviour
{
    public AudioClip gaterattle;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = gaterattle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)  //Plays Sound Whenever collision detected
    {
        if (col.gameObject.name == "Capsule")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
