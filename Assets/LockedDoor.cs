using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockDoor()
    {
        GameObject Door = this.transform.GetChild(0).gameObject;
        Door.layer = 0;
        Door.transform.GetChild(0).gameObject.SetActive(false);
    }
}
