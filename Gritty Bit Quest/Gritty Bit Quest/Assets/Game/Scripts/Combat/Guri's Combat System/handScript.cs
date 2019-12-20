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
    gameState gameStateScript;
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
     
        m_currentWeaponDelayTime += Time.deltaTime;
        //set a weapondelay so guns arent fired as soon as theyre picked up

        //raycast to objects lying around and pick them up from a distance
        //if (UILine.RayToDistantObjects() != null && grabbing && handState != HandStates.Wielding)
        //{
        //    //PickupObject(UILine.RayToDistantObjects());
        //}
    }

    public g_UILine GetUILine()
    {
        return UILine;
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
}
