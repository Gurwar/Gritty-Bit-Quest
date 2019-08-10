using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_heightManager : MonoBehaviour {
    [SerializeField]
    ControlType controlScript;
	// Use this for initialization
	void Start ()
    {
		if (controlScript.device == ControlType.VRDevices.OculusRift)
        {
            GetComponent<CapsuleCollider>().height = 4;
        }
        else if (controlScript.device == ControlType.VRDevices.Vive)
        {
            GetComponent<CapsuleCollider>().height = 2.5f;
        }
    }
	
}
