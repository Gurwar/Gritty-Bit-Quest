using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * This script aims the grenade and launches it.
 *  It will also warn other agents of itself when it lands, usually prompting them to move out of the way.
 * */

    public class g_GrenadeScript : MonoBehaviour
    {

    OVRGrabbable grabbable;
    bool active;
    void Start()
    {
    
        grabbable = GetComponent<OVRGrabbable>();
    
    }

    void Update()
    {
        if (grabbable.isGrabbed)
        {
            active = true;
            GetComponent<ExplosiveScript>().SetExplosive();
        }

        if (active)
        {
            if (!grabbable.isGrabbed)
            {
                Debug.Log("active");
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }

	
    }
