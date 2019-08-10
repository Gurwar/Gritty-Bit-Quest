using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
public class ControlType : MonoBehaviour {
	public enum Controllers
	{
		XboxController,
		Touch
	}
	public Controllers controller;

    public enum VRDevices
    {
        OculusRift,
        Vive
    }
    [HideInInspector]
    public VRDevices device;

    void Start()
    {
        //if (UnityEngine.VR.VRDevice.model == "Oculus Rift CV1")
        //{
        //    device = VRDevices.OculusRift;
        //}
        //else if (UnityEngine.VR.VRDevice.model == "Vive MV")
        //{
        //    device = VRDevices.Vive;
        //}

    }
}
