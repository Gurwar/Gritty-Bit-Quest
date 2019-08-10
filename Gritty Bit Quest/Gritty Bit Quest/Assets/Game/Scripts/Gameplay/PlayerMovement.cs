using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    Transform camera;
    Vector3 cameraInitialPosition;
    [SerializeField]
    float speedMultiplier;
    [SerializeField]
    float boostMoveSpeed;
    [SerializeField]
    float rotateDegrees;
    [SerializeField]
    float rotateSpeed;
    Vector3 targetRotation;
    float yOffset;
    Vector3 direction;
    float distanceToGround;
    bool spinning;
    bool turningLeft;
    // Use this for initialization
    void Start()
    {
        OVRManager.display.RecenterPose();
        targetRotation = transform.eulerAngles;
        //distanceToGround = GetComponent<Collider>().bounds.extents.y;
        //StartCoroutine(StartCalibrateCenter()); for openvr
    }

    IEnumerator StartCalibrateCenter()
    {
        yield return new WaitForSeconds(.5f);
        ChangeCameraInitialPos();
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
    // Update is called once per frame
    void Update()
    {
        spinning = true;
        direction = transform.TransformDirection(direction).normalized;
        transform.position += speedMultiplier * Time.deltaTime * direction;
        direction = Vector3.zero;
        transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, targetRotation, Time.deltaTime * rotateSpeed, rotateSpeed * Time.deltaTime);
        if ((transform.eulerAngles - targetRotation).magnitude < 1)
        {
            transform.eulerAngles = targetRotation;
            spinning = false;
        }
        
        if ((transform.eulerAngles - targetRotation).magnitude >= 360)
        {
            if (turningLeft)
            {
                targetRotation = new Vector3(0, 270, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                targetRotation = Vector3.zero;
            }
            spinning = false;
        }
        //GameManager.Player = gameObject;
    }

    public void CalculateSpeedAndDirection(Vector2 movementVector)
    {
        direction.x = movementVector.x * speedMultiplier;
        direction.z = movementVector.y * speedMultiplier;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 1f);
    }

 
    public void ChangeCameraInitialPos()
    {
        cameraInitialPosition = camera.localPosition;
    }
    public void RotatePlayer(bool left)
    {
        if (!spinning)
        {
            turningLeft = left;
            if (left)
                targetRotation.y -= rotateDegrees;
            else
                targetRotation.y += rotateDegrees;
        }
    }
}
