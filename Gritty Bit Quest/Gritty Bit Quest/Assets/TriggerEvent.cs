using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public enum TriggerType { ObjectGrabbed, TriggerWithPlayer};
    [SerializeField]
    TriggerType triggerType;
    OVRGrabbable GrabbableScript;
    [SerializeField]
    GameObject target;
    [SerializeField]
    string message;
    bool triggered;
    void Start()
    {
        GrabbableScript = GetComponent<OVRGrabbable>();
    }


    // Update is called once per frame
    void Update()
    {
        if (triggerType == TriggerType.ObjectGrabbed)
        {
            if (GrabbableScript)
            {
                if(GrabbableScript.isGrabbed)
                {
                    target.SendMessage(message, SendMessageOptions.RequireReceiver);
                    Destroy(this);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                target.SendMessage(message, SendMessageOptions.RequireReceiver);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (triggerType == TriggerType.ObjectGrabbed)
        {
            if (collider.transform.root.name == "Player")
            {
                collider.gameObject.SendMessage(message, SendMessageOptions.RequireReceiver);
                Destroy(this);
            }
        }
    }
}
