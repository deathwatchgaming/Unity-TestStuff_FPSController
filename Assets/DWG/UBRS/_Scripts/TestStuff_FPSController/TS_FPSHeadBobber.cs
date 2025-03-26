/*
*
*  Name: DWG TestStuff FPSController HeadBobber
*  File: TS_HeadBobber.cs
*  Author: Deathwatch Gaming
*  License: MIT
*
*/

// using - Engine
using UnityEngine;

// Namespace - DWG.UBRS.TestStuff
namespace DWG.UBRS.TestStuff
{
    // public class TS_FPSHeadBobber
    public class TS_FPSHeadBobber : MonoBehaviour
    {
        // Header HeadBobbing
        [Header("HeadBobbing")]

            // public SC_FPSController characterController
            public TS_FPSController characterController;

            // public float walkingBobbingSpeed = 14f
            public float walkingBobbingSpeed = 14f;

            // public float bobbingAmount = 0.05f
            public float bobbingAmount = 0.05f;

        // float defaultPosY = 0
        float defaultPosY = 0;

        // float timer = 0
        float timer = 0;

        // Start is called before the first frame update

        // public void Start
        public void Start()
        {
            // defaultPosY = transform.localPosition.y
            defaultPosY = transform.localPosition.y;

        } // Close - public void Start

        // Update is called once per frame

        // public void Update
        public void Update()
        {
            // if
            if(Mathf.Abs(characterController.moveDirection.x) > 0.1f || Mathf.Abs(characterController.moveDirection.z) > 0.1f)
            {
                // Player is moving

                // Debug Log
                //Debug.Log("Player is moving.");
                
                // timer
                timer += Time.deltaTime * walkingBobbingSpeed;

                // transform.localPosition
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);

            } // Close - if
            
            // else
            else
            {
                // Player is not moving (Idle)

                // Debug Log
                //Debug.Log("Player is not moving.");

                // timer = 0
                timer = 0;

                // transform.localPosition
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
            
            } // Close - else

        } // Close - public void Update

    } // Close - public class TS_FPSHeadBobber

} // Close -  Namespace - DWG.UBRS.TestStuff
