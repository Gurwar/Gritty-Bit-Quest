using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputInfo
{
    static Vector3 leftMovementVector;
    static Vector3 rightMovementVector;
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
}

