// Using - System - Engine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace - DWG.UBRS.TestStuff
namespace DWG.UBRS.TestStuff
{

    // Require - Component - Type Of - CharacterController
    [RequireComponent(typeof(CharacterController))]

    // RequireComponent - Type Of - AudioSource
    [RequireComponent(typeof(AudioSource))]

    // Public - Class - TS_FPSController
    public class TS_FPSController : MonoBehaviour
    {

        // Standard

        // public - float - walkingSpeed
        public float walkingSpeed = 7.5f;

        // public - float - runningSpeed
        public float runningSpeed = 11.5f;

        // public - float - jumpSpeed
        public float jumpSpeed = 8.0f;

        // public float gravity
        public float gravity = 20.0f;

        // public - Camera - playerCamera
        public Camera playerCamera;

        // public - float - lookSpeed
        public float lookSpeed = 2.0f;

        // public - float - lookXLimit
        public float lookXLimit = 45.0f;

        // Crouching

        // public - bool - isCrouching
        public bool isCrouching = false;

        // public - float - crouchHeight
        public float crouchHeight = 0.5f;

        // public - float - crouchSpeed
        public float crouchSpeed = 3.5f;

        // public - float - crouchTransitionSpeed
        public float crouchTransitionSpeed = 10f;

        // private - float - originalHeight
        private float originalHeight;

        // public - float - crouchCameraOffset
        public float crouchCameraOffset = -0.5f;

        // private - Vector3 - cameraStandPosition
        private Vector3 cameraStandPosition;

        // private - Vector3 - cameraCrouchPosition
        private Vector3 cameraCrouchPosition;

        // Wall Jumping

        // public - LayerMask - wallLayer
        public LayerMask wallLayer;

        // public - float - wallJumpForce
        public float wallJumpForce = 10f;

        // private - bool - isTouchingWall
        private bool isTouchingWall = false;

        // CharacterController - characterController 
        CharacterController characterController;

        // Hide In Inspector 
        [HideInInspector]
        public Vector3 moveDirection = Vector3.zero; // public - Vector3 - moveDirection - Vector3 - zero

        // Float - RotationX - 0
        float rotationX = 0;

        // Hide In Inspector 
        [HideInInspector]
        public bool canMove = true; // Public - Bool - canMove - True

        // Walking Footsteps
        
        // public AudioClip[] footstepSounds
        public AudioClip[] footstepSounds; // Array to hold footstep sound clips

        // public float minTimeBetweenFootsteps = 0.3f
        public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds

        // public float maxTimeBetweenFootsteps = 0.6f
        public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds
        
        // public AudioSource footstepAudioSource
        public AudioSource footstepAudioSource; // Reference to the Audio Source component

        // public bool isWalking = false
        public bool isWalking = false; // Flag to track if the player is walking

        // public float timeSinceLastFootstep
        public float timeSinceLastFootstep; // Time since the last footstep sound

        // Sprinting Footsteps
        
        // public AudioClip[] sprintstepSounds
        public AudioClip[] sprintstepSounds; // Array to hold sprint footstep sound clips

        // public float minTimeBetweenSprintsteps = 0.2f
        public float minTimeBetweenSprintsteps = 0.2f; // Minimum time between sprint footstep sounds

        // public float maxTimeBetweenSprintsteps = 0.4f
        public float maxTimeBetweenSprintsteps = 0.4f; // Maximum time between sprint footstep sounds
        
        // public AudioSource sprintstepAudioSource
        public AudioSource sprintstepAudioSource; // Reference to the Audio Source component

        // public bool isSprinting = false
        public bool isSprinting = false; // Flag to track if the player is sprinting

        // public float timeSinceLastSprintstep
        public float timeSinceLastSprintstep; // Time since the last sprint footstep sound
        
        // public void Awake
        public void Awake()
        {

           // footstepAudioSource
           footstepAudioSource = GetComponent<AudioSource>(); // Get the Audio Source component

           // sprintstepAudioSource
           sprintstepAudioSource = GetComponent<AudioSource>(); // Get the Audio Source component

        } // Close - public void Awake

        // Public - Void -Start
        public void Start()
        {

            // characterController - GetComponent - CharacterController
            characterController = GetComponent<CharacterController>();

            // originalHeight - characterController - height
            originalHeight = characterController.height;

            // Define camera positions for standing and crouching

            // cameraStandPosition - playerCamera - transform -localPosition
            cameraStandPosition = playerCamera.transform.localPosition;

            // cameraCrouchPosition - cameraStandPosition - new Vector3 - 0 - crouchCameraOffset - 0
            cameraCrouchPosition = cameraStandPosition + new Vector3(0, crouchCameraOffset, 0);

            // Lock cursor

            // Cursor - lockState - CursorLockMode - Locked
            Cursor.lockState = CursorLockMode.Locked;

            // Cursor - visible - false
            Cursor.visible = false;

        } // Close - Public - Void - Start

        // Public - Void - Update
        public void Update()
        {

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

            // We are grounded, so recalculate move direction based on axes

            // Vector3 - forward - transform - TransformDirection - Vector3 - forward
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // Vector3 - right - transform - TransformDirection - Vector3 - right
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run

            // bool - isRunning - Input - GetKey - KeyCode - LeftShift
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            // Player's movement speed

            // Walking - Running - Crouching 

            // float - curSpeedX - canMove - isRunning - runningSpeed - isCrouching - crouchSpeed - walkingSpeed - Input - GetAxis - Vertical - 0
            float curSpeedX = canMove ? (isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed)) * Input.GetAxis("Vertical") : 0;

            // float - curSpeedX - canMove - isRunning - runningSpeed - isCrouching - crouchSpeed - walkingSpeed - Input - GetAxis - Horizontal - 0
            float curSpeedY = canMove ? (isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed)) * Input.GetAxis("Horizontal") : 0;

            // float - movementDirectionY - moveDirection -y
            float movementDirectionY = moveDirection.y;

            // moveDirection - forward - curSpeedX - right - curSpeedY
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Jump

            // If - Input - GetButton - Jump - & canMove - & characterController - isGrounded
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {

                // moveDirection - y - jumpSpeed
                moveDirection.y = jumpSpeed;

            } // Close -  If - Input - GetButton - Jump - & canMove - & characterController - isGrounded

            // Wall Jump

            // Else If - Input - GetButton - Jump - & canMove & isTouchingWall
            else if (Input.GetButton("Jump") && canMove && isTouchingWall)
            {

                // moveDirection - y - wallJumpForce
                moveDirection.y = wallJumpForce;

                // This adds a bit of horizontal force opposite the wall for a more dynamic jump

                // If - Physics - Raycast - transform - position - transform - right - 1f - wallLayer
                if (Physics.Raycast(transform.position, transform.right, 1f, wallLayer))
                {

                    // moveDirection - -transform - right - wallJumpForce
                    moveDirection += -transform.right * wallJumpForce / 2.5f; // Adjust the divisor for stronger/weaker push.

                } // Close -  If - Physics - Raycast - transform - position - transform - right - 1f - wallLayer

                // Else If - Physics - Raycast - transform - position - -transform right - 1f - wallLayer
                else if (Physics.Raycast(transform.position, -transform.right, 1f, wallLayer))
                {

                    // moveDirection - transform - right - wallJumpForce 
                    moveDirection += transform.right * wallJumpForce / 2.5f;

                } // Close - Else If

            } // Close - Else If - Input - GetButton - Jump - & canMove & isTouchingWall

            // Else
            else
            {

                // moveDirection - y - movementDirectionY
                moveDirection.y = movementDirectionY;

            } // Close - Else
            
            // Crouching

            // If - Input - GetKeyDown - KeyCode - C - & canMove
            if (Input.GetKeyDown(KeyCode.C) && canMove)
            {
        
                // isCrouching - Not isCrouching
                isCrouching = !isCrouching;

                // If - isCrouching
                if (isCrouching)
                {

                    // characterController - height - crouchHeight
                    characterController.height = crouchHeight;

                    // walkingSpeed - crouchSpeed
                    walkingSpeed = crouchSpeed;

                } // Close - If - isCrouching

                // Else If - Not - isCrouching
                else if (!isCrouching)
                {

                    // characterController - height - originalHeight
                    characterController.height = originalHeight;

                    // walkingSpeed - 7.5
                    walkingSpeed = 7.5f;

                } // Close -  Else If - Not - isCrouching

            } // Close - If - Input - GetKeyDown - KeyCode - C - & canMove

            // If - isCrouching
            if (isCrouching)
            {

                // playerCamera - transform - localPosition - Vector3 - Lerp - playerCamera - transform - localPosition - cameraCrouchPosition - crouchTransitionSpeed - Time - deltaTime
                playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraCrouchPosition, crouchTransitionSpeed * Time.deltaTime);

            } // Close -  If - isCrouching

            // Else If - Not - isCrouching
            else if (!isCrouching)
            {

                // playerCamera - transform - localPosition - Vector3 - Lerp - playerCamera - transform - localPosition - cameraStandPosition - crouchTransitionSpeed - Time - deltaTime
                playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraStandPosition, crouchTransitionSpeed * Time.deltaTime);

            } // Close -  Else If - Not - isCrouching

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)

            // If - Not - characterController - isGrounded
            if (!characterController.isGrounded)
            {

                // moveDirection - y - gravity - Time - deltaTime
                moveDirection.y -= gravity * Time.deltaTime;

            } // Close - If - Not - characterController - isGrounded

            // Move the controller

            // characterController - Move - moveDirection - Time - deltaTime
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation

            // If - canMove
            if (canMove)
            {

                // rotationX - -Input - GetAxis - Mouse - Y - lookSpeed
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;

                // rotationX - Mathf - Clamp - rotationX - -lookXLimit - lookXLimit
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

                // playerCamera - transform - localRotation Quaternion - Euler - rotationX - 0 - 0
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

                // transform - rotation - Quaternion - Euler - 0 - Input - GetAxis - Mouse - X - lookSpeed - 0
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            } // Close - If - canMove

            // Detect Walls       

            // RaycastHit - hit
            RaycastHit hit;

            // If - Physics - Raycast - transform - position - transform - right - out hit - 1f -wallLayer - Physics - Raycast - transform - position - -transform - right - out hit - 1f - wallLayer
            if (Physics.Raycast(transform.position, transform.right, out hit, 1f, wallLayer) || Physics.Raycast(transform.position, -transform.right, out hit, 1f, wallLayer))
            {

                // isTouchingWall - True
                isTouchingWall = true;

            } // Close -  If

            // Else
            else
            {

                // isTouchingWall - False
                isTouchingWall = false;

            } // Close - Else

            // Player movement code - Walking

            // Input.GetKey(KeyCode.W) "Up / Forward"

            // if
            if (Input.GetAxis("Vertical") > 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

            } // Close - if

            // Input.GetKey(KeyCode.S) "Down / Backward"

            // else if            
            else if (Input.GetAxis("Vertical") < 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

            } // Close - else if

            // Input.GetKey(KeyCode.D) "Right"

            // else if           
            else if (Input.GetAxis("Horizontal") > 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

            } // Close - else if
            
            // Input.GetKey(KeyCode.A) "Left"

            // else if           
            else if (Input.GetAxis("Horizontal") < 0 && canMove && characterController.isGrounded)
            {

                // StartWalking
                StartWalking();

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

            } // Close - if

            // Input.GetKey(KeyCode.S) "Down / Backward" + Input.GetKey(KeyCode.LeftShift)

            // else if            
            else if (Input.GetAxis("Vertical") < 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

            } // Close - else if

            // Input.GetKey(KeyCode.D) "Right" + Input.GetKey(KeyCode.LeftShift)

            // else if           
            else if (Input.GetAxis("Horizontal") > 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

            } // Close - else if
            
            // Input.GetKey(KeyCode.A) "Left" + Input.GetKey(KeyCode.LeftShift)

            // else if           
            else if (Input.GetAxis("Horizontal") < 0 && canMove && characterController.isGrounded && isRunning)
            {

                // StopWalking
                StopWalking();

                // StartSprinting
                StartSprinting();

            } // Close - else if 

            // else
            else
            {

                // StopSprinting
                StopSprinting();	

            } // Close - else

        } // Close - Public - Void - Update

        // Call this method when the player starts walking

        // void StartWalking
        void StartWalking()
        {

            // isWalking
            isWalking = true;

        } // Close - void StartWalking

        // Call this method when the player stops walking

        // void StopWalking
        void StopWalking()
        {

            // isWalking
            isWalking = false;

        } // Close - void StopWalking

        // Call this method when the player starts running

        // void StartSprinting
        void StartSprinting()
        {

            // isSprinting
            isSprinting = true;

        } // Close - void StartSprinting

        // Call this method when the player stops running

        // void StopSprinting
        void StopSprinting()
        {

            // isWalking
            isSprinting = false;

        } // Close - void StopSprinting

    } // Close - Public - Class - TS_FPSController

} // Close -  Namespace - DWG.UBRS.TestStuff
