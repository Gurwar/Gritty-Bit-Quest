using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRObjectScript : MonoBehaviour
{
    public int id;
    bool grabbed;
    OVRGrabbable OVR;

    private void Start()
    {
        OVR = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        //get frame when item is grabbed
        if (!grabbed)
        {
            if (OVR.isGrabbed)
            {
                grabbed = true;
                transform.parent = OVR.grabbedBy.transform;
            }
        }
        else
        {
            if (!OVR.isGrabbed)
            {
                grabbed = false;
                transform.parent = null;
            }
        }
    }

}
