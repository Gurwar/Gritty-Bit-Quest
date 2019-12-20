using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo
{
    static Vector3 leftMovementVector;
    static Vector3 rightMovementVector;
    static bool grippedLeft;
    static bool grippedRight;
    static bool triggerLeft;
    static bool triggerRight;

    public static void SetHandMovementVectors(Vector3 left, Vector3 right)
    {
        leftMovementVector = left;
        rightMovementVector = right;
    }
    public static Vector3 GetLeftMovement()
    {
        return leftMovementVector;
    }

    public static Vector3 GetRightMovement()
    {
        return rightMovementVector;
    }

    public static void SetGrippedLeft(bool g)
    {
        grippedLeft = g;
    }

    public static void SetGrippedRight(bool g)
    {
        grippedRight = g;
    }

    public static void SetGrippedTriggerLeft(bool g)
    {
        grippedLeft = g;
    }

    public static void SetGrippedTriggerRight(bool g)
    {
        grippedRight = g;
    }

    public static bool GetGrippedLeft()
    {
        return grippedLeft;
    }

    public static bool GetGrippedRight()
    {
        return grippedRight;
    }

    public static bool GetTriggerLeft()
    {
        return grippedLeft;
    }

    public static bool GetTriggerRight()
    {
        return grippedRight;
    }

    public static bool CheckButtonPressed(string button)
    {
        if (Input.GetButtonDown(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckButtonHeld(string button)
    {
        if (Input.GetButton(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckButtonReleased(string button)
    {
        if (Input.GetButtonUp(button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

