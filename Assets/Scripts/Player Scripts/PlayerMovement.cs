using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject Head;
    public GameObject Body;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    float jumpSpeed = 0.5f;
    bool isCrouched;
    bool isHolding;
    public bool isRunning;
    int scalingFramesLeft = 0;

    Rigidbody rb;
    Vector3 direction;
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    Vector3 normalScale = new Vector3(1, 1, 1);
    Vector3 currentSize;
    Vector3 targetSize;

    [Header("Rotation")]
    public float LookSens = 2f;
    float XRotation;
    float YRotation;
    float RotationClamp = 90f;

    public static PlayerMovement movementInstance;

    private void Awake()
    {
        movementInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
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
            isRunning = true;
            
            walkSpeed += runSpeed;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            walkSpeed -= runSpeed;
        }
        if (Input.GetKey(KeyCode.Space) && GroundChecker.IsGrounded)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = true;
            //transform.localScale = new Vector3(1, 0.5f, 1);
            scalingFramesLeft = 25;
            currentSize = crouchScale;
            targetSize = normalScale;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouched = false;
            //transform.localScale = normalScale;
            scalingFramesLeft = 25;
            currentSize = normalScale;
            targetSize = crouchScale;
        }
        if (scalingFramesLeft > 0)
        {
            transform.localScale = Vector3.Lerp(currentSize, targetSize, Time.deltaTime * 25);
            scalingFramesLeft--;
        }
        #endregion

        Rotate();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            PlayerStamina.instance.UseStamina(1);
        }
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
