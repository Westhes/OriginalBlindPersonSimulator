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
    public AudioClip walkingSound;
    public AudioClip crouchingSound;
    private AudioSource audio;
    private DoubleAudioSource doubleAudio;
    public AudioClip safetyBGM;
    public AudioClip chaseBGM;

    private float startTime;

    private void Awake()
    {
        movementInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        doubleAudio = GetComponent<DoubleAudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        PlaySafeBGM(true);
        targetSize = normalScale;
    }

    float distanceMovedSinceLastTime = 0f;
    // Update is called once per frame
    void Update()
    {
        #region BasicMovement
        if (RespawnScript.canMove)
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                move += transform.forward * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move -= transform.right * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move -= transform.forward * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move += transform.right * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;

                walkSpeed += runSpeed;
            }

            transform.position += move;
            distanceMovedSinceLastTime += move.magnitude;
            if (distanceMovedSinceLastTime > 2f)
            {
                distanceMovedSinceLastTime = 0f;
                if (!isCrouched)
                    audio.PlayOneShot(walkingSound, Random.Range(0.3f, .5f));
                else
                    audio.PlayOneShot(crouchingSound, Random.Range(0.2f, .3f));
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
                //scalingFramesLeft = 25;
                //currentSize = crouchScale;
                targetSize = crouchScale;
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                isCrouched = false;
                //transform.localScale = normalScale;
                //scalingFramesLeft = 25;
                //currentSize = normalScale;
                targetSize = normalScale;
            }
            if (transform.localScale != targetSize)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 5f);
                //scalingFramesLeft--;
            }
            Rotate();
        }
        #endregion
    }

    public bool IsSafeSoundPlaying { get; private set; } = true;
    public void PlaySafeBGM(bool safe)
    {
        IsSafeSoundPlaying = safe;
        if (safe)
            doubleAudio.CrossFade(safetyBGM, 0.1f, 1f, 1f);
        else
            doubleAudio.CrossFade(chaseBGM, 0.5f, 0.2f, 0f);
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
