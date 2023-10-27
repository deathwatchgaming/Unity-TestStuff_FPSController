// Using - Engine
using UnityEngine;

// Namespace - DWG.UBRS.TestStuff
namespace DWG.UBRS.TestStuff
{

    // Public - Class - TS_HeadBobber
    public class TS_HeadBobber : MonoBehaviour
    {

        // public - float - walkingBobbingSpeed
        public float walkingBobbingSpeed = 14f;

        // public - float - bobbingAmount
        public float bobbingAmount = 0.05f;

        // public - TS_FPSController - controller
        public TS_FPSController controller;

        // float - defaultPosY - 0
        float defaultPosY = 0;

        // float - timer - 0
        float timer = 0;

        // Start is called before the first frame update

        // Public - Void - Start
        public void Start()
        {

            // defaultPosY - transform - localPosition - y
            defaultPosY = transform.localPosition.y;

        } // Public - Void - Start

        // Update is called once per frame

        // Public - Void - Update
        public void Update()
        {

            // If - Mathf - Abs - controller - moveDirection - x > 0.1 - Mathf - Abs - controller - moveDirection - z > 0.1
            if (Mathf.Abs(controller.moveDirection.x) > 0.1f || Mathf.Abs(controller.moveDirection.z) > 0.1f)
            {
                //Player is moving

                // timer - Time - deltaTime - walkingBobbingSpeed
                timer += Time.deltaTime * walkingBobbingSpeed;

                // transform - localPosition - new Vector3 - transform - localPosition - x - defaultPosY - Mathf - Sin - timer - bobbingAmount - transform - localPosition - z
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);

            } // Close - If - Mathf - Abs - controller - moveDirection - x > 0.1 - Mathf - Abs - controller - moveDirection - z > 0.1

            // Else
            else
            {

                //Idle

                // timer - 0
                timer = 0;

                // transform - localPosition - new Vector3 - transform - localPosition - x - Mathf - Lerp - transform -localPosition - y - defaultPosY - Time - deltaTime - walkingBobbingSpeed - transform - localPosition - z
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);

            } // Close - Else

        } // Close -  Public - Void - Update

    } // Close - Public - Class - TS_HeadBobber

} // Close - Namespace - DWG.UBRS.TestStuff