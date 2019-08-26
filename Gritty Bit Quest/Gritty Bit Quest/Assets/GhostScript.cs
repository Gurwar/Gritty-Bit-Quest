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
    [SerializeField]
    Transform carSeat;
    [SerializeField]
    Transform carExitSpot;
    [SerializeField]
    float seatYOffset;
    bool inRangeToGetInCar;
    bool inCar;

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
        if (!inCar)
            return;
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

    public void AttemptToGetInVehicle()
    {
        if (inRangeToGetInCar && !inCar)
        {
            inCar = true;
            GameManager.Player.transform.parent = carSeat;
            GameManager.Player.transform.localPosition = new Vector3(0, seatYOffset);
            GameManager.Player.transform.rotation = Quaternion.Euler(transform.eulerAngles);
            GameManager.Player.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.Player.GetComponent<PlayerMovement>().SetInCar(true);
            OVRManager.display.RecenterPose();
        }
    }

    public void AttemptToGetOutOfCar()
    {
        if (inCar)
        {
            GameManager.Player.transform.parent = null;
            GameManager.Player.transform.position = carExitSpot.transform.position;

            GameManager.Player.GetComponent<Rigidbody>().isKinematic = false;
            GameManager.Player.GetComponent<PlayerMovement>().SetInCar(false);

            OVRManager.display.RecenterPose();
        }
    }

    public bool GetInRangeToGetInCar()
    {
        return inRangeToGetInCar;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.root.tag == "Player")
        {
            inRangeToGetInCar = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.transform.root.tag == "Player")
        {
            inRangeToGetInCar = false;
        }
    }
}
