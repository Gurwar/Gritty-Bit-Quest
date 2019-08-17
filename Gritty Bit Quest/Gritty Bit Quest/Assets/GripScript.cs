using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripScript : MonoBehaviour
{
    [SerializeField]
    GhostScript Ghost;
    [SerializeField]
    GameObject steeringWheel;
    [SerializeField]
    Color hoveredColor;
    [SerializeField]
    Color grippedColor;
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    bool grabbedLeft;
    bool grabbedRight;
    [SerializeField]
    float inRangeDistance;
    bool inRangeLeft;
    bool inRangeRight;
    void Start()
    {
        SetHandleColor(defaultColor);
    }

    void Steer(Vector3 movement)
    {
        Vector2 validMovementVector = transform.up;
        float magnitude = Vector2.Dot(movement, validMovementVector);
        //do a dot product between the movement vector and the valid vector for movement on the grip
        if (Mathf.Abs(magnitude) > 0.75)
        {
            float targetZ = steeringWheel.transform.localEulerAngles.z + magnitude;
            targetZ = Mathf.Clamp(targetZ, 135, 225);
            Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetZ);
            steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.rotation,
                                                               targetQuaternion,
                                                               Time.deltaTime * Ghost.GetSteeringWheelTurnMagnitude());

        }
    }

    void SetupGrabbing()
    {
        if (!grabbedLeft)
        {
            if (InputInfo.GetGrippedLeft() && inRangeLeft)
            {
                grabbedLeft = true;
            }
        }
        else if (!InputInfo.GetGrippedLeft() && (leftHand))
        {
            grabbedLeft = false;
        }

        if (!grabbedRight)
        {
            if (InputInfo.GetGrippedRight() && inRangeRight)
            {
                grabbedRight = true;
            }
        }
        else if (!InputInfo.GetGrippedRight())
        {
            grabbedRight = false;
        }

        if (grabbedLeft || grabbedRight)
        {
            SetHandleColor(grippedColor);
        }
        else if (InputInfo.GetGrippedLeft() && inRangeLeft || InputInfo.GetGrippedRight() && inRangeRight)
        {
            SetHandleColor(grippedColor);
        }
        else if (!InputInfo.GetGrippedLeft() && inRangeLeft || (!InputInfo.GetGrippedRight() && inRangeRight))
        {
            SetHandleColor(hoveredColor);
        }
        else if (!InputInfo.GetGrippedLeft() && !inRangeLeft || (!InputInfo.GetGrippedRight() && !inRangeRight))
        {
            SetHandleColor(defaultColor);
        }
    }

    void LateUpdate()
    {
        SetInRangeLeft();
        SetInRangeRight();
        SetupGrabbing();
        //to first grab the handle, checkgrip has to be true, once it is grabbed it is tied to a hand
        if (grabbedLeft || grabbedRight)
        {
            if (grabbedLeft)
            {
                if (new Vector2(InputInfo.GetLeftMovement().x, InputInfo.GetLeftMovement().y).magnitude >= .0025)
                {
                    Steer(new Vector2(InputInfo.GetLeftMovement().x, InputInfo.GetLeftMovement().y).normalized);
                }
            }
            if (grabbedRight)
            {
                if (new Vector2(InputInfo.GetRightMovement().x, InputInfo.GetRightMovement().y).magnitude >= .0025)
                {
                    Steer(new Vector2(InputInfo.GetRightMovement().x, InputInfo.GetRightMovement().y).normalized);
                }
            }
        }
        if (steeringWheel.transform.localEulerAngles.x != 0 || steeringWheel.transform.localEulerAngles.y != 0)
        {
            steeringWheel.transform.localEulerAngles = new Vector3(0, 0, steeringWheel.transform.localEulerAngles.z);
        }
        Ghost.SetSteerAngle(steeringWheel.transform.localEulerAngles.z - 180);
    }

    void SetHandleColor(Color color)
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
    }

    void SetInRangeLeft()
    {
        if (Vector3.Distance(leftHand.transform.position, transform.position) < inRangeDistance)
        {
            inRangeLeft = true;
        }
        else
        {
            inRangeLeft = false;
        }
    }

    void SetInRangeRight()
    {
        if (Vector3.Distance(rightHand.transform.position, transform.position) < inRangeDistance)
        {
            inRangeRight = true;
        }
        else
        {
            inRangeRight = false;
        }
    }
}