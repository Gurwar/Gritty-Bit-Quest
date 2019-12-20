using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
public class g_PlayerInput : MonoBehaviour {
    bool leftTriggerReleased;
    bool rightTriggerReleased;
    bool leftGripReleased;
    bool rightGripReleased;
    bool grippedLeft;
    bool grippedRight;
    bool triggerLeft;
    bool triggerRight;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightHand;
    void Update()
    {
        // manage input for GUI

        // toggle pausing and abort if paused
        InputRecenter();
        EmptyHandInput();
        // manage input for weapons
        InputMovement();
        InputRotation();
    }

    void EmptyHandInput()
    {
        if (Input.GetAxisRaw("LeftHandGrip") > .2f || Input.GetKey(KeyCode.L))
        {
            grippedLeft = true;
        }
        else
        {
            grippedLeft = false;
        }

        if (Input.GetAxisRaw("RightHandGrip") > .2f || Input.GetKey(KeyCode.R))
        {
            grippedRight = true;
        }
        else
        {
            grippedRight = false;
        }

        if (Input.GetAxisRaw("LeftHandTrigger") > .2f || Input.GetKey(KeyCode.L))
        {
            triggerLeft = true;
        }
        else
        {
            triggerLeft = false;
        }

        if (Input.GetAxisRaw("RightHandTrigger") > .2f || Input.GetKey(KeyCode.R))
        {
            triggerRight = true;
        }
        else
        {
            triggerRight = false;
        }


        InputInfo.SetGrippedLeft(grippedLeft);
        InputInfo.SetGrippedRight(grippedRight);
        InputInfo.SetGrippedTriggerLeft(triggerLeft);
        InputInfo.SetGrippedTriggerRight(triggerRight);
    }


    void InputMovement()
    {
        float x = 0;
        float y = 0;
        if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }

        GetComponent<PlayerMovement>().CalculateSpeedAndDirection(new Vector2(
                                                            x,
                                                            y));
        //both controllers can open/close the gun menu
        if ((Input.GetAxisRaw("LeftJoystick_Horizontal") != 0) || (Input.GetAxisRaw("LeftJoystick_Vertical") != 0))
        {
            GetComponent<PlayerMovement>().CalculateSpeedAndDirection(new Vector2(
                                                                        Input.GetAxis("LeftJoystick_Horizontal"), 
                                                                        Input.GetAxis("LeftJoystick_Vertical")));
        }
    }

    void InputRotation()
    {
        //if (InputInfo.CheckButtonPressed("X"))
        //{
        //    GetComponent<PlayerMovement>().RotatePlayer(true);
        //}
        //
        //if (InputInfo.CheckButtonPressed("A"))
        //{
        //    GetComponent<PlayerMovement>().RotatePlayer(false);
        //} 
    }

    void InputRecenter()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OVRManager.display.RecenterPose();
        }
        
    }
}
