using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    PlayerManager PM;
    
    //The camera attached to the player object
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] AudioSource footstep1;
    [SerializeField] AudioSource footstep2;
    [SerializeField] AudioSource outOfStamina;
    [SerializeField] float pitchMin = 0.8f;
    [SerializeField] float pitchMax = 1.2f;
    [SerializeField] float volumeMin = 0.8f;
    [SerializeField] float volumeMax = 1f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] [Range(0.0f, 1.0f)] float staminaDecayRate = 1.0f;
    [SerializeField] [Range(0.0f, 1.0f)] float staminaRefillRate = 0.5f;
    [SerializeField] float runStamina = 400.0f;

    //Locks the cursor in place
    [SerializeField] bool lockCursor = true;

    //camera controls
    float cameraPitch = 0.0f;
    float velocityDown = 0.0f;
    CharacterController controller;

    Vector2 currentDirection = Vector2.zero;
    Vector2 currentDirectionVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    //movement related stuff
    Vector3 velocity;
    static float MAX_STAMINA = 400f;

    //audio related stuff
    //AudioSource footstep1, footstep2;
    bool whichStep = false;
    static float MAX_DELAY_WALKING = 0.500f; //half a second
    static float MAX_DELAY_RUNNING = 0.333f; //1/3 second
    static float OUT_OF_STAMINA_COUNTER = 1.666f; //1 2/3 second
    //float footStepDelay = 60f;
    //float breathDelay = 60f;
    //System.DateTime momentWalking = System.DateTime.Now;
    //System.DateTime momentStamina = System.DateTime.Now;
    //System.DateTime now;
    float timerWalk = 0f;
    float timerRun = 0f;
    float timerOutOfStamina = 0f;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("Player").GetComponent<PlayerManager>();
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        if(PM.canMove)          UpdateMovement();
        if(PM.hasAxe)           PM.UseAxe();
        if(PM.hasFlashlight)    PM.CheckFlashlight();
    }

    //Get the x and y movement of the mouse. Rotate the camera on the x axis and the character on the y axis.
    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDirection.Normalize();

        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, moveSmoothTime);

        //getting time for audio effects
        timerWalk += Time.deltaTime;
        timerRun += Time.deltaTime;
        timerOutOfStamina += Time.deltaTime;

        if (controller.isGrounded)
        {
            velocityDown = 0.0f;
        }


        velocityDown += gravity * Time.deltaTime;

        //check if shift is held down for running
        if (Input.GetButton("Sprint") && runStamina > 0)
        {
            isRunning = true;
            runStamina = (float)Math.Max(runStamina - staminaDecayRate, 0);
            velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * runSpeed + Vector3.up * velocityDown;
        }
        else
        {
            //to not allow the player to just hold shift for running indefinitely
            if (!Input.GetButton("Sprint"))
            {
                runStamina = (float)Math.Min(runStamina + staminaRefillRate, MAX_STAMINA);
            }
            velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * walkSpeed + Vector3.up * velocityDown;
            isRunning = false;
        }
        //sound effect out of stamina
        if ( timerOutOfStamina > OUT_OF_STAMINA_COUNTER && runStamina < 50)
        {
            outOfStamina.pitch = UnityEngine.Random.Range(0.9f, 0.95f);
            outOfStamina.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
            outOfStamina.Play();
            timerOutOfStamina = 0f;
            //breathDelay = OUT_OF_STAMINA_COUNTER;
            //momentStamina = DateTime.Now;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //sound effect steps
            if (whichStep)
            {
                if (!isRunning && timerWalk > MAX_DELAY_WALKING && timerRun > MAX_DELAY_RUNNING)
                {
                    footstep1.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                    footstep1.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                    footstep1.Play();
                    whichStep = false;
                    timerWalk = 0f;
                    //footStepDelay = Input.GetButton("Sprint") && !(runStamina < 1) ? MAX_DELAY_RUNNING : MAX_DELAY_WALKING;
                    //momentWalking = DateTime.Now;
                }
                else if (isRunning && timerRun > MAX_DELAY_RUNNING && timerWalk > MAX_DELAY_WALKING)
                {
                    footstep1.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                    footstep1.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                    footstep1.Play();
                    whichStep = false;
                    //footStepDelay = Input.GetButton("Sprint") && !(runStamina < 1) ? MAX_DELAY_RUNNING : MAX_DELAY_WALKING;
                    //momentWalking = DateTime.Now;
                    timerRun = 0f;
                }
            }
            else
            {
                if (!isRunning && timerWalk > MAX_DELAY_WALKING && timerWalk > MAX_DELAY_RUNNING)
                {
                    footstep2.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                    footstep2.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                    footstep2.Play();
                    whichStep = true;
                    timerWalk = 0f;
                    //footStepDelay = Input.GetButton("Sprint") && !(runStamina < 1) ? MAX_DELAY_RUNNING : MAX_DELAY_WALKING;
                    //momentWalking = DateTime.Now;
                }
                else if(isRunning && timerRun > MAX_DELAY_RUNNING && timerWalk > MAX_DELAY_WALKING)
                {
                    footstep2.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                    footstep2.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                    footstep2.Play();
                    whichStep = true;
                    timerRun = 0f;
                    //footStepDelay = Input.GetButton("Sprint") && !(runStamina < 1) ? MAX_DELAY_RUNNING : MAX_DELAY_WALKING;
                    //momentWalking = DateTime.Now;
                }
            }
        }

        //footStepDelay -= 1;
        //breathDelay -= 1;
        controller.Move(velocity * Time.deltaTime);
    }
}
