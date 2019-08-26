using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField]
    List<WheelCollider> backWheels;
    [SerializeField]
    List<WheelCollider> frontWheels;
    [SerializeField]
    float steeringWheelTurnMagnitude;
    float steerAngle;
    float torque;
    [SerializeField]
    GameObject steeringWheel;
    [SerializeField]
    Vector2 minMaxTorque;
    [SerializeField]
    Vector2 minMaxSteer;
    [SerializeField]
    GripScript LeftGrip;
    [SerializeField]
    GripScript RightGrip;

    public float GetSteeringWheelTurnMagnitude()
    {
        return steeringWheelTurnMagnitude;
    }

    public void SetSteerAngle(float sa)
    {
        steerAngle = sa;
    }
    // Update is called once per frame
    void Update()
    {
        if (InputInfo.CheckButtonHeld("B"))
        {
            IncrementSpeed(500);
        }
        else if (InputInfo.CheckButtonHeld("A"))
        {
            IncrementSpeed(-2000);
        }

        if (torque < minMaxTorque.x)
        {
            torque = minMaxTorque.x;
        }
        else if (torque > minMaxTorque.y)
        {
            torque = minMaxTorque.y;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            steeringWheel.transform.localEulerAngles += new Vector3(0, 0, .5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            steeringWheel.transform.localEulerAngles -= new Vector3(0, 0, .5f);
        }

        

        steerAngle *= -1;
        for (int i = 0; i < frontWheels.Count; i++)
        {
            frontWheels[i].steerAngle = steerAngle;
        }

        for (int i = 0; i < backWheels.Count; i++)
            backWheels[i].motorTorque = torque;
    }

    public void IncrementSpeed(float t)
    {
        torque += t;
    }
}
