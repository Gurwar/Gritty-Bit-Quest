using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public enum Hands
    {
        LeftHand,
        RightHand
    };
    public Hands hand;
    [SerializeField]
    ControlType controller;
    Animation anim;
    float m_vibrateTime;
    float m_shootVibrateTime = .1f;
    float m_hitVibrateTime = .4f;
    float m_interactPromptTime = .1f;
    float m_currentVibrateTime;
    bool vibrate;
    float zLastFrame;
    [SerializeField]
    List<Vector3> listOfVelocities = new List<Vector3>();
    Vector3 averageVelocity;
    [SerializeField]
    Transform handTransform;
    void Start()
    {
        anim = GetComponent<Animation>();

    }

    void CalculateAverageVelocity()
    {
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < listOfVelocities.Count; i++)
            sum += listOfVelocities[i];

        averageVelocity = new Vector3(sum.x / (listOfVelocities.Count - 1), sum.y / (listOfVelocities.Count - 1), sum.z / (listOfVelocities.Count - 1));
    }

    void SetPosition()
    {
        transform.position = handTransform.position;
    }

    void SetRotation()
    {
        transform.rotation = handTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AddVelocityFrames();
        CalculateAverageVelocity();
        UpdateVelocity();
        SetPosition();
        SetRotation();
        if (vibrate)
        {
            m_currentVibrateTime += Time.deltaTime;
            if (m_currentVibrateTime >= m_vibrateTime)
            {
                m_currentVibrateTime = 0;
                if (hand == Hands.LeftHand)
                {
                    StopVibration();
                }
                else
                {
                    StopVibration();
                }
                vibrate = false;
            }
        }
        //if (controller.GetComponent<ControlType>().controller == ControlType.Controllers.Touch)
        //{
        //    if (hand == Hands.LeftHand)
        //    {
        //        transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        //        transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        //    }
        //    else
        //    {
        //        transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //        transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        //    }
        //    zLastFrame = transform.localPosition.z;
        //}
    }

    public void UpdateVelocity()
    {
        if (hand == Hands.LeftHand)
            InputInfo.SetVelocityLeft(averageVelocity);
        else
            InputInfo.SetVelocityRight(averageVelocity);
    }

    public void AddVelocityFrames()
    {
        if (hand == Hands.LeftHand)
        {
            if (listOfVelocities.Count < 20)
            {
                listOfVelocities.Add(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
            }
            else
            {
                listOfVelocities.Add(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
                listOfVelocities.RemoveAt(0);

            }
        }
        else if(hand == Hands.RightHand)
        {
            if (listOfVelocities.Count < 5)
            {
                listOfVelocities.Add(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
            }
            else
            {
                listOfVelocities.Add(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
                listOfVelocities.RemoveAt(0);
            }
        }
    }

    public void Hit()
    {
        OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.RTouch);
        m_vibrateTime = m_hitVibrateTime;
        vibrate = true;
    }

    public void Shoot()
    {
        if (hand == Hands.LeftHand)
        {
            OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0, .3f, OVRInput.Controller.RTouch);
        }
        m_vibrateTime = m_shootVibrateTime;
        vibrate = true;
    }

    public void InteractPrompt()
    {
        if (hand == Hands.LeftHand)
        {
            OVRInput.SetControllerVibration(0, .2f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0, .2f, OVRInput.Controller.RTouch);
        }
        m_vibrateTime = m_interactPromptTime;
        vibrate = true;
    }

    public void StopVibration()
    {
        if (hand == Hands.LeftHand)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

        vibrate = false;
        m_vibrateTime = 0;

    }

}
