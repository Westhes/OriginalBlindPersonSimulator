using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public BoxCollider objectCollider;
    public BoxCollider objectBlockerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(objectCollider, objectBlockerCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
