using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
public class g_PlayerInput : MonoBehaviour {
    bool m_canshootRight = true;
    bool m_canshootLeft = true;
    bool canWalk;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    GameObject UI;
    [SerializeField]
    GameObject radialUILeft;
    [SerializeField]
    GameObject radialUIRight;
    [SerializeField]
    PauseScript pauseScript;
    [SerializeField]
    gameState gameState;
    [SerializeField]
    g_UILine LeftLine;
    [SerializeField]
    g_UILine RightLine;
    [SerializeField]
    ControlType controlScript;
    bool leftTriggerReleased;
    bool rightTriggerReleased;
    bool leftGripReleased;
    bool rightGripReleased;
    bool grippedLeft;
    bool grippedRight;
    Vector3 previousLeftPos;
    Vector3 previousRightPos;
    void Start()
    {
        UI = GameObject.Find("UI");

    }

    void Update()
    {
        if (rightHand.GetComponent<handScript>().heldGameObject != null)
        {
            if (rightHand.GetComponent<handScript>().currentWeapon.tapFiring)
            {
                TapFiringAdjustment("RightHandTrigger", ref m_canshootRight);
            }
        }
        if (leftHand.GetComponent<handScript>().heldGameObject != null)
        {
            if (leftHand.GetComponent<handScript>().currentWeapon.tapFiring)
            {
                TapFiringAdjustment("LeftHandTrigger", ref m_canshootLeft);
            }
        }
        // manage input for GUI

        // toggle pausing and abort if paused
        UpdatePause();
        InputRecenter();
        InputVRButtons();
        EmptyHandInput();
        if (pauseScript != null)
        {
            if (pauseScript.paused)
                return;
        }
        // manage input for weapons

        InputAttack();
        InputMovement();
        InputRotation();
        CalculateHandMovement();
    }

    void CalculateHandMovement()
    {
        InputInfo.SetHandMovementVectors(leftHand.transform.localPosition - previousLeftPos, rightHand.transform.localPosition - previousRightPos);
        previousLeftPos = leftHand.transform.localPosition;
        previousRightPos = rightHand.transform.localPosition;
    }

    void EmptyHandInput()
    {
        if (GameObject.Find("Level Scripts").GetComponent<ControlType>().controller == ControlType.Controllers.Touch)
        {
            if (Input.GetAxisRaw("LeftHandGrip") > .2f || Input.GetKey(KeyCode.L))
            {
                if (!grippedLeft)
                {
                    grippedLeft = true;
                    if (leftHand.GetComponent<handScript>().heldGameObject == null)
                    {
                        leftHand.GetComponent<handScript>().AttemptPickup();
                        GameManager.Car.GetComponent<GhostScript>().AttemptToGetInVehicle();
                    }
                    else
                    {
                        leftHand.GetComponent<handScript>().DropObject();
                    }
                }
            }
            else
            {
                grippedLeft = false;
            }

            if (Input.GetAxisRaw("RightHandGrip") > .2f || Input.GetKey(KeyCode.R))
            {
                if (!grippedRight)
                {
                    grippedRight = true;
                    if (rightHand.GetComponent<handScript>().heldGameObject == null)
                    {
                        rightHand.GetComponent<handScript>().AttemptPickup();
                        GameManager.Car.GetComponent<GhostScript>().AttemptToGetInVehicle();
                    }
                    else
                    {
                        rightHand.GetComponent<handScript>().DropObject();
                    }
                }
            }
            else
            {
                grippedRight = false;
            }
        }
        else
        {
            if (Input.GetAxisRaw("LeftHandTrigger") > .2f)
            {
                leftHand.GetComponent<handScript>().AttemptPickup();
                GameManager.Car.GetComponent<GhostScript>().AttemptToGetInVehicle();
            }

            if (Input.GetAxisRaw("RightHandTrigger") > .2f)
            {
                rightHand.GetComponent<handScript>().AttemptPickup();
                GameManager.Car.GetComponent<GhostScript>().AttemptToGetInVehicle();
            }
        }

        InputInfo.SetGrippedLeft(grippedLeft);
        InputInfo.SetGrippedRight(grippedRight);
    }

    void FireHand(string currentHand, string axis, ref bool canShoot)
    {
        if (Input.GetAxisRaw(axis) > 0.1)
        {
            //get right hand and fire it
            if (GameObject.Find(currentHand).GetComponent<handScript>().heldGameObject != null)
            {
                if (canShoot)
                {
                    GameObject.Find(currentHand).GetComponent<handScript>().Fire();
                    if (GameObject.Find(currentHand).GetComponent<handScript>().currentWeapon.tapFiring)
                    {
                        //canShoot = false;
                    }
                }
                if (!GameObject.Find(currentHand).GetComponent<handScript>().currentWeapon.tapFiring)
                {
                    GameObject.Find(currentHand).GetComponent<handScript>().Fire();
                }
            }
        }
        else if (GameObject.Find(currentHand).GetComponent<handScript>().heldGameObject != null)
        {
            if (GameObject.Find(currentHand).GetComponent<handScript>().heldGameObject.GetComponent<gunShooter>() != null)
                GameObject.Find(currentHand).GetComponent<handScript>().heldGameObject.GetComponent<gunShooter>().StandBy();
        }
    }
  
    void InputAttack()
    {
        if (gameState.playerState == gameState.GameStates.Wave || gameState.playerState == gameState.GameStates.Pregame)
        {
            if (GameObject.Find("RightHand") != null)
                FireHand("RightHand", "RightHandTrigger", ref m_canshootRight);
            if (GameObject.Find("LeftHand") != null)
                FireHand("LeftHand", "LeftHandTrigger", ref m_canshootLeft);
        }
    }

    void TapFiringAdjustment(string axis, ref bool m_canShootSide)
    {
        if (Input.GetAxisRaw(axis) < 0.1)
        {
            m_canShootSide = true;
        }
    }

    void InputInteract()
    {

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

    void InputRadialUI()
    {
        //both controllers can open/close the gun menu
        if ((Input.GetAxisRaw("LeftJoystick_Horizontal") != 0) || (Input.GetAxisRaw("LeftJoystick_Vertical") != 0))
        {
            radialUILeft.SetActive(true);
        }
        else
        {
            radialUILeft.SetActive(false);
        }

        if ((Input.GetAxisRaw("RightJoystick_Horizontal") != 0) || (Input.GetAxisRaw("RightJoystick_Vertical") != 0))
        {
            radialUIRight.SetActive(true);
        }
        else
        {
            radialUIRight.SetActive(false);
        }
    }

    void InputRotation()
    {
        if (controlScript.device == ControlType.VRDevices.OculusRift)
        {
            if (InputInfo.CheckButtonPressed("X"))
            {
                GetComponent<PlayerMovement>().RotatePlayer(true);
            }

            if (InputInfo.CheckButtonPressed("A"))
            {
                GetComponent<PlayerMovement>().RotatePlayer(false);
            }
        }
        else if (controlScript.device == ControlType.VRDevices.Vive)
        {
            if (Input.GetButtonDown("LeftJoystickClick"))
            {
                GetComponent<PlayerMovement>().RotatePlayer(true);
            }

            if (Input.GetButtonDown("RightJoystickClick"))
            {
                GetComponent<PlayerMovement>().RotatePlayer(false);
            }
        }
    }

    void InputRecenter()
    {
        if (controlScript.device == ControlType.VRDevices.OculusRift)
        {
            if (Input.GetButtonDown("Y"))
            {
                OVRManager.display.RecenterPose();
            }
        }
    }

    void InputVRButtons()
    {
         if (gameState.playerState != gameState.GameStates.Wave)
         {
            if (Input.GetAxisRaw("LeftHandTrigger") > 0.5)
            {
                if (leftTriggerReleased)
                {
                    LeftLine.ButtonPressed();
                    leftTriggerReleased = false;
                }
            }
            else
            {
                leftTriggerReleased = true;
            }

            if (Input.GetAxisRaw("RightHandTrigger") > 0.5)
            {
                if (rightTriggerReleased)
                {
                    RightLine.ButtonPressed();
                    rightTriggerReleased = false;
                }
            }
            else
            {
                rightTriggerReleased = true;
            }
         }
    }


    void UpdatePause()
    {
        if (controlScript.device == ControlType.VRDevices.OculusRift)
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (!pauseScript.paused)
                {
                    pauseScript.Pause();
                }
                else
                {
                    pauseScript.UnPause();
                }
            }
        }
        else if (controlScript.device == ControlType.VRDevices.Vive)
        {
            if (Input.GetButtonDown("B"))
            {
                if (!pauseScript.paused)
                {
                    pauseScript.Pause();
                }
                else
                {
                    pauseScript.UnPause();
                }
            }
        }
    }
}
