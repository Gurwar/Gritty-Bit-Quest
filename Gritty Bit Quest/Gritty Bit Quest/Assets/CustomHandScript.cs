using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GrabbableObjects
{
    public string Name;
    public int id;
    public Vector3 targetPositionInHand;
    public Vector3 targetRotationInHand;
}

public class CustomHandScript : MonoBehaviour
{
    [SerializeField]
    List<GrabbableObjects> GrabbableObjects = new List<GrabbableObjects>();
    Dictionary<int, GrabbableObjects> GrabbableObjectsDict = new Dictionary<int, GrabbableObjects>();
    GameObject currentHeldObject;
    [SerializeField]
    Transform handTransform;
    public enum Hands
    {
        LeftHand,
        RightHand
    };
    public Hands hand;
    [SerializeField]
    List<Vector3> listOfVelocities = new List<Vector3>();
    Vector3 averageVelocity;
    OVRGrabber grabber;
    [SerializeField]
    float lerpSpeed;
    bool canMoveHandle;
    public delegate void HandMoveInteract(Vector3 deltaMovement);
    public event HandMoveInteract moveLever;
    void Start()
    {
        grabber = GetComponent<OVRGrabber>();
        for (int i = 0; i < GrabbableObjects.Count; i++)
            GrabbableObjectsDict.Add(GrabbableObjects[i].id, GrabbableObjects[i]);
    }
    public void UpdateVelocity()
    {
        //if (hand == Hands.LeftHand)
        //    InputInfo.SetVelocityLeft(averageVelocity);
        //else
        //    InputInfo.SetVelocityRight(averageVelocity);
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
        else if (hand == Hands.RightHand)
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

    void CalculateAverageVelocity()
    {
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < listOfVelocities.Count; i++)
            sum += listOfVelocities[i];

        averageVelocity = new Vector3(sum.x / (listOfVelocities.Count - 1), sum.y / (listOfVelocities.Count - 1), sum.z / (listOfVelocities.Count - 1));
    }
    public void Fire()
    {
         if (GetComponentInChildren<gunShooter>() != null)
         {
             // gun has ammo and script is valid
             if (GetComponentInChildren<gunShooter>().Fire())
             {
                 //play out the aesthetic elements of shooting
                 GetComponent<TouchController>().Shoot();
             }
         } 
    }
    // Update is called once per frame
    void Update()
    {
        bool shoot;
        if (hand == Hands.LeftHand)
        {
            shoot = InputInfo.GetTriggerLeft();
        }
        else
        {
            shoot = InputInfo.GetTriggerRight();
        }

        

        if (shoot || Input.GetKey(KeyCode.Space))
        Fire();
        PullLever();

        transform.position = handTransform.position;
        transform.rotation = handTransform.rotation;

        if (grabber.grabbedObject != null)
        {
            currentHeldObject = grabber.grabbedObject.gameObject;
            if (currentHeldObject.GetComponent<VRObjectScript>())
            MoveHeldObjectToTargetTransformInHand();
        }
        else
        {
            currentHeldObject = null;
        }

        AddVelocityFrames();
        CalculateAverageVelocity();
        UpdateVelocity();
    }
    
    public void MoveHeldObjectToTargetTransformInHand()
    {
        if (GrabbableObjectsDict.ContainsKey(currentHeldObject.GetComponent<VRObjectScript>().id))
        {
            Vector3 targetPosition = GrabbableObjectsDict[currentHeldObject.GetComponent<VRObjectScript>().id].targetPositionInHand;
            Vector3 targetRotation = GrabbableObjectsDict[currentHeldObject.GetComponent<VRObjectScript>().id].targetRotationInHand;
            Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
            currentHeldObject.transform.localPosition = Vector3.Lerp(currentHeldObject.transform.localPosition, targetPosition, lerpSpeed * Time.deltaTime);
            currentHeldObject.transform.localRotation = Quaternion.Lerp(currentHeldObject.transform.localRotation, targetQuaternion, lerpSpeed * Time.deltaTime);
        }
    }

    private void PullLever()
    {
        if (hand == Hands.LeftHand)
        {
            if (InputInfo.GetGrippedLeft())
            {
                if (canMoveHandle) // first frame of pull needs to be zero, need to wait until player knows he can pull
                {
                    if (moveLever != null)
                    {
                        moveLever(averageVelocity);
                    }
                }
                canMoveHandle = true;
            }
            else
            {
                canMoveHandle = false;
            }
        }
        else
        {
            if (InputInfo.GetGrippedRight())
            {
                if (canMoveHandle) // first frame of pull needs to be zero, need to wait until player knows he can pull
                {
                    if (moveLever != null)
                    {
                        moveLever(averageVelocity);
                    }
                }
                canMoveHandle = true;
            }
            else
            {
                canMoveHandle = false;
            }
        }
    }
}
