using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_ControlsImage : MonoBehaviour {
    SpriteRenderer renderer;
    [SerializeField]
    ControlType controlScript;
    [SerializeField]
    Sprite OculusControls;
    [SerializeField]
    Sprite ViveControls;
	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
		if (controlScript.device == ControlType.VRDevices.OculusRift)
        {
            //put oculus image up
            renderer.sprite = OculusControls;
        }
        else
        {
            renderer.sprite = ViveControls;
            //put vive image up
        }
	}
	
}
