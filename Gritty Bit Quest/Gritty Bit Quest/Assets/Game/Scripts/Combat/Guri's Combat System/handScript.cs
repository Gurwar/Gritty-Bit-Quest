using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public struct Interactable
{
    //Initialize variable for Interactable Objects
	public string ObjectName;
    public int ObjectID;
	public AudioClip pickupSound;
    public AudioClip emptySound;
    public Vector3 ObjectPosition;
    public Vector3 ObjectRotation;
    public enum ObjectTypes { NonWeapon, Gun,Melee};
    public ObjectTypes objectType;
}

public class handScript : MonoBehaviour 
{
    [SerializeField]
	List<Interactable> Objects  = new List<Interactable>();
    [HideInInspector]
    public GameObject heldGameObject;
    [HideInInspector]
    public Weapon currentWeapon;
	[HideInInspector]
	public Interactable currentObject;
    [SerializeField]
    float objectToHandSpeed;
    [SerializeField]
    gameState gameStateScript;
    public bool grabbing;
    [SerializeField]
    float pickupTimer = .1f;
	float currentPickupTimer;
    [SerializeField]
    g_UILine UILine;
    //need a delay after we pickup weapon
    public enum HandStates
    {
        Wielding,
        Empty,
        PumpAction
    };
    public HandStates handState;
    [SerializeField]
	float m_weaponDelay;
	float m_currentWeaponDelayTime;

    void Start()
    {
    }

	void Update()
	{
        //after player presses the grip, the hand should only grab items for a limited time
        if (grabbing)
        {
            currentPickupTimer += Time.deltaTime;
        }
        if (currentPickupTimer >= pickupTimer)
		{
            grabbing = false;

            if (heldGameObject == null)
            {
                currentPickupTimer = 0;
                //GetComponentInChildren<HandController>().SetGrabAnimation(false);
            }

        }
        m_currentWeaponDelayTime += Time.deltaTime;
        //set a weapondelay so guns arent fired as soon as theyre picked up

        //LERP the held object to the correct position and rotation in the hand
        if (heldGameObject != null && heldGameObject.transform.localPosition != currentObject.ObjectPosition)
        {
            heldGameObject.transform.localPosition = Vector3.Lerp(heldGameObject.transform.localPosition, currentObject.ObjectPosition, Time.deltaTime * objectToHandSpeed);
            heldGameObject.transform.localEulerAngles = Vector3.RotateTowards(heldGameObject.transform.localEulerAngles, currentObject.ObjectRotation, Time.deltaTime * objectToHandSpeed, 0);
            if ((heldGameObject.transform.localPosition - currentObject.ObjectPosition).magnitude < .1)
            {
                heldGameObject.transform.localPosition = currentObject.ObjectPosition;
                heldGameObject.transform.localEulerAngles = currentObject.ObjectRotation;
            }
        }

        //raycast to objects lying around and pick them up from a distance
        if (UILine.RayToDistantObjects() != null && grabbing && handState != HandStates.Wielding)
        {
            PickupObject(UILine.RayToDistantObjects());
        }
    }

    public g_UILine GetUILine()
    {
        return UILine;
    }

    public void AttemptPickup()
    {
        if (handState == HandStates.Empty)
        {
            m_currentWeaponDelayTime = 0;
            grabbing = true;
        }
    }

    public void Fire()
	{
		if (m_currentWeaponDelayTime >= m_weaponDelay)
		{
			if (GetComponentInChildren<gunShooter>() != null)
			{
                // gun has ammo and script is valid
                if (GetComponentInChildren<gunShooter>().Fire()) 
				{
                    if (gameStateScript.levelType == gameState.LevelTypes.Tutorial)
                    {
                        gameStateScript.CloseGunTutorial();
                    }
                    //play out the aesthetic elements of shooting
                    GetComponent<TouchController>().Shoot();
                    GetComponent<Animation>().Play(currentWeapon.firingAnimation.name);
                }
                else if (heldGameObject.GetComponent<GunAmmo>().currentAmmo == 0)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        //play empty gun clip
                        PlaySound(currentObject.emptySound);
                    }
                }
			}
		}
	}

    public void PickupObject(GameObject objectToPickup)
    {
        heldGameObject = objectToPickup;
        if (heldGameObject.GetComponent<ObjectState>().currentState == ObjectState.ObjectStates.Holstered)
        {
            // begin reloading a new gun in holster
            heldGameObject.GetComponentInParent<g_HolsterScript>().BeginRefresh(); 
        }
        heldGameObject.GetComponent<ObjectState>().PickupObject(this);
        //make our object's gameobject a child of our hand's mesh 
        heldGameObject.transform.parent = transform;
        for (int i = 0; i < Objects.Count; i++)
        {
            //go through our list of possible types of objects and find the one that
            //we currently have
            if (heldGameObject.GetComponent<ObjectState>().ID == Objects[i].ObjectID)
            {
                currentObject = Objects[i];
                PlaySound(currentObject.pickupSound);
            }
        }
        //make the rigidbody kinematic so the gun doesn't fall down
        heldGameObject.GetComponent<Rigidbody>().isKinematic = true;
        handState = HandStates.Wielding;
    }
    public void DropObject()
    {
        //if we currently have something
        if (handState == HandStates.Wielding && heldGameObject != null)
        {
            heldGameObject.transform.parent = null;
            heldGameObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.ObjectName = "";
            //throw the object based on our body and hand's current velocity
            Vector3 throwVector;
            if (GetComponent<TouchController>().hand == TouchController.Hands.LeftHand)
                throwVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * (InputInfo.GetVelocityLeft() + (transform.root.GetComponent<PlayerMovement>().GetDirection() * transform.root.GetComponent<PlayerMovement>().GetSpeedMultiplier()));
            else
                throwVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * (InputInfo.GetVelocityRight() + (transform.root.GetComponent<PlayerMovement>().GetDirection() * transform.root.GetComponent<PlayerMovement>().GetSpeedMultiplier()));
            heldGameObject.GetComponent<G_ThrowObject>().Throw(throwVector);
            heldGameObject.GetComponent<ObjectState>().currentState = ObjectState.ObjectStates.Free;
            heldGameObject = null;
            handState = HandStates.Empty;
        }
    }

    public void ChangeFiringMode(int index)
    {
        GetComponent<TouchController>().InteractPrompt();
    }

    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }


    void OnTriggerStay(Collider other)
    {
        //if radius trigger is hitting an object and we are not wielding anything
        if (other.GetComponent<ObjectState>() != null && heldGameObject == null)
        {
            GameObject tempObject = other.gameObject;
            TransformDeepChildExtension.FindDeepChild(tempObject.transform, "Image").GetComponent<GunUIScript>().SetInRange(gameObject, true);
            if (grabbing)
            {
                PickupObject(tempObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectState>() != null)
        {
            GameObject tempObject = other.gameObject;
            TransformDeepChildExtension.FindDeepChild(tempObject.transform, "Image").GetComponent<GunUIScript>().SetInRange(gameObject, false);
        }
    }
}
