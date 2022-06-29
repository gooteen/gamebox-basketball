using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip catchSound;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip gruntSound;
    [SerializeField] private float chargeSoundVolume;
    [SerializeField] private float catchSoundVolume;
    [SerializeField] private float gruntSoundVolume;
    [SerializeField] private float catchSoundPitch = 1f;
    [SerializeField] private float chargeSoundPitch = 3f;
    [SerializeField] private float gruntSoundPitch = 3f;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] LayerMask springCheckMask;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float g = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float groundedPush = -2f;

    [SerializeField] Transform playerSpawnPoint;

    [SerializeField] private GameObject ball;
    [SerializeField] private Transform gripPoint;
    [SerializeField] private CameraController playerView;
    [SerializeField] private UIController ui;

    [SerializeField] private float springJumpHeight;
    [SerializeField] private float impulseStepValue = 5f;
    [SerializeField] private float impulseValue = 5f;

    [SerializeField] private float chargeTimeLimit = 2.5f;
    [SerializeField] private float chargeLevelStep = 0.5f;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkDistance = 0.4f;

    [SerializeField] private float spawnDelay = 1f;
    private float spawnDelayStart = 0f;

    private ScoreManager scoreManager;

    private float startChargeTime;
  
    private CharacterController controller;
    private Vector3 velocity;
    private Rigidbody ballRb;

    private bool soundPlayed;
    private bool isGrounded;
    private bool isOnTheJumpPlatform;
    private bool isSprinting;
    private bool ballPicked;
    private bool isCharging;
    private bool beingSpawned;

    private float defaultSpeed;


    void Start()
    {
        soundPlayed = false;
        beingSpawned = false;
        defaultSpeed = playerSpeed;
        //impulseValue = defaultImpulseValue;
        isSprinting = false;
        ballPicked = false;
        isCharging = false;
        source = GetComponent<AudioSource>();
        scoreManager = ball.GetComponent<ScoreManager>();
        controller = GetComponent<CharacterController>();
        ballRb = ball.GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, checkDistance, groundCheckMask);
        isOnTheJumpPlatform = Physics.CheckSphere(groundChecker.position, checkDistance, springCheckMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = groundedPush;
        }

        if (isOnTheJumpPlatform && !beingSpawned)
        {
            Debug.Log("Hop");
            velocity.y = Mathf.Sqrt(springJumpHeight * -2f * g);
        }
        
        if (beingSpawned)
        {
            if ((Time.time - spawnDelayStart) > spawnDelay)
            {
                beingSpawned = !beingSpawned;
            }
            transform.position = playerSpawnPoint.position;
            transform.rotation = playerSpawnPoint.rotation;
        }
        else
        {
            float movementX = Input.GetAxis("Horizontal");
            float movementZ = Input.GetAxis("Vertical");
            Vector3 direction = transform.forward * movementZ + transform.right * movementX;
            controller.Move(direction * playerSpeed * Time.deltaTime);

            velocity.y += g * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * g);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Release();
        }

        ManageThrow();

        Sprint();

        if (!isCharging)
        {
            ui.FillScale(0, 1);
        }
        
    }

    public void Grunt()
    {
        source.clip = gruntSound;
        source.volume = gruntSoundVolume;
        source.pitch = gruntSoundPitch;
        source.Play();
    }

    public void SetBeingSpawned()
    {
        beingSpawned = true;
        spawnDelayStart = Time.time;
    }

    public bool CheckBall()
    {
        return ballPicked;
    }

    public void Release()
    {
        ballRb.isKinematic = false;
        ball.transform.SetParent(null);
        startChargeTime = 0;
        ballPicked = false;
        isCharging = false;
        Debug.Log("Supposed to be released");
        source.Stop();
        soundPlayed = false;
    }

    void PickUp()
    {
        if (playerView.CheckIfPickable())
        {
            ballRb.isKinematic = true;
            ball.transform.position = gripPoint.transform.position;
            ball.transform.rotation = gripPoint.transform.rotation;
            ball.transform.SetParent(gripPoint.transform);
            ballPicked = true;
            scoreManager.UnlockScoring();
            source.clip = catchSound;
            source.volume = catchSoundVolume;
            source.pitch = catchSoundPitch;
            source.Play();
        }
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //playerSpeed += sprintSpeed;
            isSprinting = true;
            //Debug.Log("Sprinting");
        }

        else
        {
            //playerSpeed -= sprintSpeed;
            isSprinting = false;
            //Debug.Log("Stopped Sprinting");
        }

        if (isGrounded)
        {
            if (isSprinting)
            {
                playerSpeed = sprintSpeed;
            }
            else
            {
                playerSpeed = defaultSpeed;
            }
        }
    }

    void ManageThrow()
    {
        if (ballPicked)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                startChargeTime = Time.time;
            }

            if (Input.GetButton("Fire2"))
            {
                if (Time.time - startChargeTime <= chargeTimeLimit)
                {
                    isCharging = true;
                    impulseValue = impulseStepValue + impulseStepValue * (float)(System.Math.Truncate((Time.time - startChargeTime) / chargeLevelStep));
                    ui.FillScale((Time.time - startChargeTime), chargeTimeLimit);
                    if(!soundPlayed)
                    {
                        source.clip = chargeSound;
                        source.volume = chargeSoundVolume;
                        source.pitch = chargeSoundPitch;
                        source.Play();
                        soundPlayed = true;
                    }
                    //Debug.Log("Impulse value: " + impulseValue);
                }
            }

            if (Input.GetButtonUp("Fire2"))
            {
                Release();
                ballRb.AddForce(ball.transform.forward.normalized * impulseValue, ForceMode.Impulse);
                //startChargeTime = 0;
                //isCharging = false;
            }
        }
    }

}
