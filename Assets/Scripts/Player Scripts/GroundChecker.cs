using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public static bool IsGrounded;
    private void Start()
    {
        IsGrounded = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6)//if layer = ground layer
            IsGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 6)//if layer = ground layer
            IsGrounded = false;
    }
}
