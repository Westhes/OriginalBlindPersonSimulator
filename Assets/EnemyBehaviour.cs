using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Uninitialized = 0,
    Idle = 1,
    Exploring = 2,
    Chasing = 3,
    LostChase = 4,
}
public class EnemyBehaviour : MonoBehaviour
{
    public string CurrentState;

    delegate void CallState();
    CallState callState;
    EnemyState _state;
    public EnemyState State
    {
        get => _state;
        private set
        {
            _state = value;
            animator.SetFloat("AnimationSpeed", walkAnimationSpeedMultiplier);
            agent.speed = WalkSpeed;
            switch(value)
            {
                case EnemyState.Uninitialized:
                    animator.SetFloat("AnimationSpeed", 0f);
                    callState = null;
                    break;
                case EnemyState.Idle:
                    if (!playerMovement.IsSafeSoundPlaying)
                        playerMovement.PlaySafeBGM(true);
                    randomIdleTime = Time.time + Random.Range(2f, 4f);
                    animator.SetFloat("AnimationSpeed", 0f);
                    agent.speed = 0f;

                    callState = Idle;
                    break;
                case EnemyState.Exploring:
                    if (RandomExplorationPoint)
                        ExploringPointsIndex = Random.Range(0, ExploringPoints.Length);
                    else
                        ExploringPointsIndex = (ExploringPointsIndex + 1) % ExploringPoints.Length;
                    agent.SetDestination(ExploringPoints[ExploringPointsIndex].position);
                    callState = Exploring;
                    break;
                case EnemyState.Chasing:
                    animator.SetFloat("AnimationSpeed", runAnimationSpeedMultiplier);
                    if (playerMovement.IsSafeSoundPlaying)
                        playerMovement.PlaySafeBGM(false);
                    agent.speed = runSpeed;
                    callState = Chase;
                    break;
                case EnemyState.LostChase:
                    animator.SetFloat("AnimationSpeed", runAnimationSpeedMultiplier);
                    agent.speed = runSpeed;
                    callState = LostChase;
                    break;
            }
            CurrentState = value.ToString();
        }
    }
    NavMeshAgent agent;
    Animator animator;
    PlayerMovement playerMovement;
    public Transform EnemyEyes;
    [Range(1f, 5f)]
    public float walkAnimationSpeedMultiplier = 2f;
    [Range(1f, 5f)]
    public float WalkSpeed = 2f;
    [Range(2f, 5f)]
    public float runAnimationSpeedMultiplier = 3f;
    [Range(1f, 5f)]
    public float runSpeed = 4f;

    [Tooltip("Points to which the AI navigates to over time")]
    public Transform[] ExploringPoints = new Transform[0];
    [Tooltip("If disabled: pick the next explorationPoint in order, otherwise it's random.")]
    public bool RandomExplorationPoint = false;
    int ExploringPointsIndex = 0;

    [Range(0.5f, 5f)]
    public float CapturePlayerRange = 3f;

    float randomIdleTime;

    [Range(3f, 20f)]
    public float maxSightDistance = 10f;

    public GameObject GameoverObject;

    public AudioClip GameoverSound;
    private AudioSource audio;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        playerMovement = FindObjectOfType<PlayerMovement>();
        State = EnemyState.Idle;
    }

    private void Update()
    {
        //callState();
        callState?.Invoke();
    }

    void Idle()
    {
        if (SeesPlayer()) State = EnemyState.Chasing;

        if (randomIdleTime < Time.time)
        {
            State = EnemyState.Exploring;
        }
    }

    void Exploring()
    {
        if (SeesPlayer()) State = EnemyState.Chasing;

        if (agent.remainingDistance > 1f)
        {
            return;
        }
        else if (agent.remainingDistance < 1f)
        {
            State = EnemyState.Idle;
        }
    }
    
    void Chase()
    {
        // TODO: check for player logic
        if (!SeesPlayer()) State = EnemyState.LostChase;
        agent.SetDestination(playerMovement.transform.position);

        if (Vector3.Distance(transform.position, playerMovement.transform.position) < CapturePlayerRange)
        {
            Debug.Log("Caught player! Execute killing player logic");
            audio.PlayOneShot(GameoverSound, 0.3f);
            GameoverObject.SetActive(true);
            State = EnemyState.Uninitialized;
        }
    }

    void LostChase()
    {
        if (SeesPlayer()) State = EnemyState.Chasing;

        if (agent.remainingDistance < 1f)
        {
            Debug.Log("Lost the chase.. guess ill idle");
            State = EnemyState.Idle;
        }
    }

    bool SeesPlayer()
    {
        Ray ray = new Ray(EnemyEyes.position, playerMovement.Body.transform.position - EnemyEyes.position);

        Vector3 dir = ray.direction;
        float dot = Vector3.Dot(dir, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, (dot > 0.3f) ? Color.green : Color.red);

        // If the ai looks in the general direction and manages to spot the player at a max range of 10.
        if (dot > 0.3f && Physics.Raycast(ray, out RaycastHit hitInfo, maxSightDistance))
        {
            
            if (hitInfo.transform.GetComponent<PlayerMovement>())
            {
                return true;
            }
        }
        return false;
    }
}
