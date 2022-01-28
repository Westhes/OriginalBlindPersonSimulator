using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject Head;
    public GameObject Body;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    float jumpSpeed = 1f;
    bool isCrouched;
    bool isHolding;

    Rigidbody rb;
    Vector3 direction;

    [Header("Rotation")]
    public float LookSens = 2f;
    float XRotation;
    float YRotation;
    float RotationClamp = 90f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region BasicMovement
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * walkSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed += runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkSpeed -= runSpeed;
        }
        if (Input.GetKey(KeyCode.Space) && GroundChecker.IsGrounded)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.C))
        {
            //transform.position -= transform.up * crouchSpeed * Time.deltaTime;
            if (isCrouched)
            {
                isCrouched = false;
                //Stay up
            }
            else
            {
                isCrouched = true;
                //Crouch
            }
        }
        #endregion

        Rotate();
    }
    void Rotate()
    {
        //assign the rotation axes
        XRotation -= Input.GetAxis("Mouse Y") * LookSens;
        YRotation += Input.GetAxis("Mouse X") * LookSens;

        //setting the rotation
        Head.transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);

        //clamping the rotation
        XRotation = Mathf.Clamp(XRotation, -RotationClamp, RotationClamp);

        Vector3 BodyRotation = new Vector3
            (Body.transform.eulerAngles.x, Head.transform.eulerAngles.y, Body.transform.eulerAngles.z);
        Body.transform.rotation = Quaternion.Euler(BodyRotation);

        transform.rotation = Quaternion.Euler(BodyRotation);
    }
}
