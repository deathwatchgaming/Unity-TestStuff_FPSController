/*
*
*  Name: DWG TestStuff FPSController
*  File: TS_FPSController.cs
*  Author: Deathwatch Gaming
*  License: MIT
*
*/

// using - Engine
using UnityEngine;

// Namespace - DWG.UBRS.TestStuff
namespace DWG.UBRS.TestStuff
{

    // RequireComponent - Type Of - CharacterController
    [RequireComponent(typeof(CharacterController))]

    // RequireComponent - Type Of - AudioSource
    [RequireComponent(typeof(AudioSource))]    

    // public class TS_FPSController
    public class TS_FPSController : MonoBehaviour
    {
        // CharacterController characterController
        CharacterController characterController;    
        
        // Header Standard Movement
        [Header("Standard Movement")]

            // public float walkingSpeed = 7.5f
            public float walkingSpeed = 7.5f;

            // public float runningSpeed = 11.5f
            public float runningSpeed = 11.5f;

            // public float jumpSpeed = 8.0f
            public float jumpSpeed = 8.0f;

            // public float gravity = 20.0f
            public float gravity = 20.0f;

            // public Camera playerCamera
            public Camera playerCamera;

            // public float lookSpeed = 2.0f
            public float lookSpeed = 2.0f;

            // public float lookXLimit = 45.0f
            public float lookXLimit = 45.0f;

            // HideInInspector
            [HideInInspector]
            public Vector3 moveDirection = Vector3.zero; // public Vector3 moveDirection = Vector3.zero

            // float rotationX = 0
            float rotationX = 0;

        // Header Crouching Movement
        [Header("Crouching Movement")]

            // public float crouchHeight = 0.5f
            public float crouchHeight = 0.5f;

            // public float crouchSpeed = 3.5f
            public float crouchSpeed = 3.5f;

            // public float crouchTransitionSpeed = 10f
            public float crouchTransitionSpeed = 10f;

            // private float originalHeight
            private float originalHeight;

            // public float crouchCameraOffset = -0.5f
            public float crouchCameraOffset = -0.5f;

            // HideInInspector
            [HideInInspector]
            public Vector3 cameraStandPosition; // public Vector3 cameraStandPosition

            // HideInInspector
            [HideInInspector]
            public Vector3 cameraCrouchPosition; // public Vector3 cameraCrouchPosition  
        
        // Header WallJump Movement
        [Header("WallJump Movement")]

            // public LayerMask wallLayer
            public LayerMask wallLayer;

            // public float wallJumpForce = 10f
            public float wallJumpForce = 10f;

        // Header Footstep Audio
        [Header("Walking Steps Audio")] 

            // Walking Footsteps
            
            // public AudioClip[] footstepSounds
            public AudioClip[] footstepSounds; // Array to hold footstep sound clips

            // public float minTimeBetweenFootsteps = 0.3f
            public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds

            // public float maxTimeBetweenFootsteps = 0.6f
            public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds
            
            // public AudioSource footstepAudioSource
            public AudioSource footstepAudioSource; // Reference to the Audio Source component

            // public float timeSinceLastFootstep
            public float timeSinceLastFootstep; // Time since the last footstep sound

            // Sprinting Footsteps

        // Header Footstep Audio
        [Header("Running Steps Audio")]            
            
            // public AudioClip[] sprintstepSounds
            public AudioClip[] sprintstepSounds; // Array to hold sprint footstep sound clips

            // public float minTimeBetweenSprintsteps = 0.2f
            public float minTimeBetweenSprintsteps = 0.2f; // Minimum time between sprint footstep sounds

            // public float maxTimeBetweenSprintsteps = 0.4f
            public float maxTimeBetweenSprintsteps = 0.4f; // Maximum time between sprint footstep sounds
            
            // public AudioSource sprintstepAudioSource
            public AudioSource sprintstepAudioSource; // Reference to the Audio Source component

            // public float timeSinceLastSprintstep
            public float timeSinceLastSprintstep; // Time since the last sprint footstep sound

            // Jumping Footsteps Sounds

        // Header Footstep Audio
        [Header("Jumping Steps Audio")]             

            // public AudioSource jumpstepAudioSource
            public AudioSource jumpstepAudioSource; // Reference to the Audio Source component

            // public AudioClip jumpSound
            public AudioClip jumpSound; // player footstep sound when starting jump

            // public AudioClip landingSound
            public AudioClip landingSound; // player footstep sound when ending jump (landed)           

        // Header Active Movement States
        [Header("Active Movement States")]

            // public bool PreviouslyGrounded
            public bool PreviouslyGrounded; // Flag to track if the player was previously grounded       

            // public bool canMove = true
            public bool canMove = true; // Flag to track if the player can move       

            // public bool isCrouching = false
            public bool isCrouching = false; // Flag to track if the player is crouching

            // public bool isWalking = false
            public bool isWalking = false; // Flag to track if the player is walking

            // public bool isSprinting = false
            public bool isSprinting = false; // Flag to track if the player is running

            // public bool isJump; 
            public bool isJump; // Flag to track if the player started a jump

            // public bool isJumping
            public bool isJumping;  // Flag to track if the player is in the act of jumping

            // public bool isTouchingWall = false
            public bool isTouchingWall = false; // Flag to track if the player is touching a wall            

        // public void Awake
        public void Awake()
        {

           // footstepAudioSource
           footstepAudioSource = GetComponent<AudioSource>(); // Get the Audio Source component

           // sprintstepAudioSource
           sprintstepAudioSource = GetComponent<AudioSource>(); // Get the Audio Source component

           // jumpstepAudioSource
           jumpstepAudioSource = GetComponent<AudioSource>(); // Get the Audio Source component

           // Dont destroy this (ie: perhaps say if desired for Scene Switch Optional)
           //DontDestroyOnLoad(this);

        } // Close - public void Awake

        // Start is called before the first frame update

        // Use This For Initialization

        // public void Start
        public void Start()
        {

            // characterController = GetComponent CharacterController
            characterController = GetComponent<CharacterController>();

            // Since we can Crouch add characterController original height of characterController

            // originalHeight = characterController.height
            originalHeight = characterController.height;

            // Define camera positions for standing and crouching

            // cameraStandPosition = playerCamera.transform.localPosition
            cameraStandPosition = playerCamera.transform.localPosition;

            // cameraCrouchPosition
            cameraCrouchPosition = cameraStandPosition + new Vector3(0, crouchCameraOffset, 0);

            // Lock cursor

            // Cursor.lockState = CursorLockMode.Locked
            Cursor.lockState = CursorLockMode.Locked;

            // Cursor.visible = false
            Cursor.visible = false;

            // Jumping
            isJumping = false;

        } // Close - public void Start
        
        // Update is called once per frame

        // public void Update
        public void Update()
        {

            // We are grounded, so recalculate move direction based on axes

            // Vector3 forward
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // Vector3 right
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run

            // bool isRunning = Input.GetKey(KeyCode.LeftShift)
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            
            // Movement Speeds

            // float curSpeedX
            float curSpeedX = canMove ? (isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed)) * Input.GetAxis("Vertical") : 0;

            // float curSpeedY
            float curSpeedY = canMove ? (isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed)) * Input.GetAxis("Horizontal") : 0;
            
            // float movementDirectionY
            float movementDirectionY = moveDirection.y;

            // moveDirection
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Crouching

            // if Input.GetKeyDown(KeyCode.C) && canMove
            if (Input.GetKeyDown(KeyCode.C) && canMove)
            {

                // isCrouching = !isCrouching
                isCrouching = !isCrouching;
                
                // if isCrouching
                if (isCrouching)
                {

                    // characterController.height
                    characterController.height = crouchHeight;

                    // walkingSpeed = crouchSpeed
                    walkingSpeed = crouchSpeed;
                    
                    // Debug Log
                    Debug.Log("Player is Crouching.");

                } // Close - if isCrouching
                
                // else
                else
                {

                    // characterController.height
                    characterController.height = originalHeight;

                    // walkingSpeed = 7.5f
                    walkingSpeed = 7.5f;

                } // Close - else

            } // Close - if Input.GetKeyDown(KeyCode.C) && canMove
            
            // if isCrouching
            if (isCrouching)
            {

                // playerCamera.transform.localPosition
                playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraCrouchPosition, crouchTransitionSpeed * Time.deltaTime);

            } // Close - if isCrouching
            
            // else
            else
            {

                // playerCamera.transform.localPosition
                playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraStandPosition, crouchTransitionSpeed * Time.deltaTime);

            } // Close - else

            // Jumping

            // if
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {

                // moveDirection.y
                moveDirection.y = jumpSpeed;
                
                // Debug Log
                //Debug.Log("Player is Ground Jumping.");

            } // Close - if

            // Wall Jumping

            // else if Input.GetButton("Jump") && canMove && isTouchingWall
            else if (Input.GetButton("Jump") && canMove && isTouchingWall)
            {

                // moveDirection.y
                moveDirection.y = wallJumpForce;
                
                // Debug Log
                Debug.Log("Player is Wall Jumping.");

                // This adds a bit of horizontal force opposite the wall for a more dynamic jump

                // if 
                if (Physics.Raycast(transform.position, transform.right, 1f, wallLayer))
                {
                    
                    // moveDirection
                    moveDirection += -transform.right * wallJumpForce / 2.5f; // Adjust the divisor for stronger/weaker push.

                } // Close - if

                // else if
                else if (Physics.Raycast(transform.position, -transform.right, 1f, wallLayer))
                {

                    // moveDirection
                    moveDirection += transform.right * wallJumpForce / 2.5f;

                } // Close - else if 

            } // Close - else if Input.GetButton("Jump") && canMove && isTouchingWall
            
            // else 
            else
            {

                // moveDirection.y
                moveDirection.y = movementDirectionY;

            } // Close - else

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)

            // if !characterController.isGrounded
            if (!characterController.isGrounded)
            {

                // moveDirection.y
                moveDirection.y -= gravity * Time.deltaTime;

            } // Close - if !characterController.isGrounded

            // Move the controller

            // characterController.Move
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation

            // if canMove
            if (canMove)
            {

                // rotationX
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;

                // rotationX
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

                // playerCamera.transform.localRotation
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

                // transform.rotation
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            } // Close - if canMove
            
            // Detect Walls
            
            // RaycastHit hit
            RaycastHit hit;
            
            // if
            if (Physics.Raycast(transform.position, transform.right, out hit, 1f, wallLayer) || Physics.Raycast(transform.position, -transform.right, out hit, 1f, wallLayer))
            {

                // isTouchingWall = true;
                isTouchingWall = true;

            } // Close - if 
            
            // else 
            else
            {

                // isTouchingWall = false
                isTouchingWall = false;

            } // Close - else

            // Footstep Sounds

            // Check if the player is walking

            // if isWalking
            if (isWalking)
            {

                // Check if enough time has passed to play the next footstep sound

                // if
                if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
                {

                    // Play a random footstep sound from the array

                    // AudioClip footstepSound
                    AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

                    // footstepAudioSource PlayOneShot
                    footstepAudioSource.PlayOneShot(footstepSound);
                    
                    // timeSinceLastFootstep
                    timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound

                } // Close - if
    
            } // Close - if isWalking

            // Check if the player is sprinting

            // if isSprinting
            if (isSprinting)
            {

                // Check if enough time has passed to play the next sprint footstep sound

                // if 
                if (Time.time - timeSinceLastSprintstep >= Random.Range(minTimeBetweenSprintsteps, maxTimeBetweenSprintsteps))
                {

                    // Play a random sprint footstep sound from the array

                    // AudioClip sprintstepSound
                    AudioClip sprintstepSound = sprintstepSounds[Random.Range(0, sprintstepSounds.Length)];

                    // sprintstepAudioSource PlayOneShot
                    sprintstepAudioSource.PlayOneShot(sprintstepSound);
                    
                    // timeSinceLastSprintstep
                    timeSinceLastSprintstep = Time.time; // Update the time since the last sprint footstep sound

                } // Close - if
    
            } // Close - if isSprinting

            // Player movement code - Walking

            // Input.GetKey(KeyCode.W) "Up / Forward"

            // if
            if (Input.GetAxis("Vertical") > 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

                // Debug Log
                Debug.Log("Player is walking Forward.");                

            } // Close - if

            // Input.GetKey(KeyCode.S) "Down / Backward"

            // else if            
            else if (Input.GetAxis("Vertical") < 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

                // Debug Log
                Debug.Log("Player is walking Backward.");                

            } // Close - else if

            // Input.GetKey(KeyCode.D) "Right"

            // else if           
            else if (Input.GetAxis("Horizontal") > 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

                // Debug Log
                Debug.Log("Player is walking Right.");               

            } // Close - else if
            
            // Input.GetKey(KeyCode.A) "Left"

            // else if           
            else if (Input.GetAxis("Horizontal") < 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

                // Debug Log
                Debug.Log("Player is walking Left.");                

            } // Close - else if 

            // else
            else
            {
                // StopWalking
                StopWalking();	

            } // Close - else

            // Player movement code - Sprinting

            // Input.GetKey(KeyCode.W) "Up / Forward" + Input.GetKey(KeyCode.LeftShift)

            // if
            if (Input.GetAxis("Vertical") > 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

                // Debug Log
                Debug.Log("Player is running Forward.");                

            } // Close - if

            // Input.GetKey(KeyCode.S) "Down / Backward" + Input.GetKey(KeyCode.LeftShift)

            // else if            
            else if (Input.GetAxis("Vertical") < 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

                // Debug Log
                Debug.Log("Player is running Backward.");                

            } // Close - else if

            // Input.GetKey(KeyCode.D) "Right" + Input.GetKey(KeyCode.LeftShift)

            // else if           
            else if (Input.GetAxis("Horizontal") > 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

                // Debug Log
                Debug.Log("Player is running Right.");                

            } // Close - else if
            
            // Input.GetKey(KeyCode.A) "Left" + Input.GetKey(KeyCode.LeftShift)

            // else if           
            else if (Input.GetAxis("Horizontal") < 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

                // Debug Log
                Debug.Log("Player is running Left.");

            } // Close - else if 

            // else
            else
            {

                // StopSprinting
                StopSprinting();	

            } // Close - else


            // Jump Sounds

            // the jump state needs to read here to make sure it is not missed

            // if not isJump
            if (!isJump)
            {
                // Jump
                isJump = Input.GetButtonDown("Jump");

            } // Close - if not isJump
            
            // if not PreviouslyGrounded & characterController.isGrounded
            if (!PreviouslyGrounded && characterController.isGrounded)
            {
                // PlayLandingSound
                PlayLandingSound();

                // moveDirection.y
                moveDirection.y = 0f;

                // Jumping
                isJumping = false;

                // Debug Log
                Debug.Log("Player Landed.");

            } // Close - if not PreviouslyGrounded & characterController.isGrounded

            // if not characterController.isGrounded & not Jumping & PreviouslyGrounded
            if (!characterController.isGrounded && !isJumping && PreviouslyGrounded)
            {
                // moveDirection.y
                moveDirection.y = 0f;

            } // Close - if not characterController.isGrounded & not Jumping & PreviouslyGrounded

            PreviouslyGrounded = characterController.isGrounded;            

        } // Close - public void Update

        // Public - Void - FixedUpdate
        public void FixedUpdate()
        {

            // Jump Sounds

            // if characterController.isGrounded
            if (characterController.isGrounded)
            {
                // moveDirection.y
                moveDirection.y = -gravity;
                
                // if isJump
                if (isJump)
                {
                    // moveDirection.y
                    moveDirection.y = jumpSpeed;

                    // PlayJumpSound
                    PlayJumpSound();

                    // Jump
                    isJump = false;

                    // Jumping
                    isJumping = true;

                    // Debug Log
                    Debug.Log("Player is Jumping.");

                } // Close - if isJump

            } // Close - if characterController.isGrounded

        }  // Close - Public - Void - FixedUpdate

        // Call this method when the player starts walking

        // void StartWalking
        void StartWalking()
        {

            // isWalking
            isWalking = true;

            // Debug Log
            //Debug.Log("Player is Walking.");

        } // Close - void StartWalking

        // Call this method when the player stops walking

        // void StopWalking
        void StopWalking()
        {

            // isWalking
            isWalking = false;
            
            // Debug Log
            //Debug.Log("Player is not Walking.");

        } // Close - void StopWalking

        // Call this method when the player starts running

        // void StartSprinting
        void StartSprinting()
        {

            // isSprinting
            isSprinting = true;
            
            // Debug Log
            //Debug.Log("Player is Running.");

        } // Close - void StartSprinting

        // Call this method when the player stops running

        // void StopSprinting
        void StopSprinting()
        {

            // isWalking
            isSprinting = false;
            
            // Debug Log
            //Debug.Log("Player is not Running.");

        } // Close - void StopSprinting

        // Jump Sounds

        // Call this method when the player starts Jump

        // void PlayJumpSound
        void PlayJumpSound()
        {
            // jumpstepAudioSource.clip 
            jumpstepAudioSource.clip = jumpSound;

            // jumpstepAudioSource.Play
            jumpstepAudioSource.Play();

        } // Close - void PlayJumpSound

        // Call this method when the player stops Jump / Lands
        
        // void PlayLandingSound
        void PlayLandingSound()
        {
            // jumpstepAudioSource.clip 
            jumpstepAudioSource.clip = landingSound;

            // jumpstepAudioSource.Play
            jumpstepAudioSource.Play();

        } // Close - void PlayLandingSound        

    } // Close - public class TS_FPSController

} // Close -  Namespace - DWG.UBRS.TestStuff
