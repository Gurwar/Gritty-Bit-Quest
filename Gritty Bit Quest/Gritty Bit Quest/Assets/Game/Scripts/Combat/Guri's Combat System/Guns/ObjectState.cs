using UnityEngine;
using System.Collections;

public class ObjectState : MonoBehaviour 
{
    public int ID;
	public enum ObjectStates
    {
		Holstered,
		Held,
        HeldByBoss,
        Free,
        Decaying,
        MovingTowardsPlayer,
        MovingTowardsBoss,
        OnGround
	};
	public ObjectStates currentState;
    g_ObjectManager ObjectManager;
    [SerializeField]
    CustomDissolve dissolveScript;
    [SerializeField]
    bool lifeLine;
    [SerializeField]
    float freeTimeUntilDelete;
    [SerializeField]
    Collider collider;
    bool startCountingFreeTime;
    float currentFreeTime;
    [SerializeField]
    GunUIScript UIScript;
    [SerializeField]
    bool ignoreCollisionWithPlayer = true;
    [SerializeField]
    int layerToIgnore;
    handScript handScript;
    void Start()
    {
        ObjectManager = GameObject.Find("Object Manager").GetComponent<g_ObjectManager>();

    }

    public void SetIgnoreCollisions(bool i)
    {
        Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<CapsuleCollider>(), collider);
    }
    
    public handScript GetHandScript()
    {
        return handScript;
    }

    void Update()
    {
        if (currentState == ObjectStates.Decaying)
            StartDissolve();
        else if (currentState == ObjectStates.Held)
            currentFreeTime = 0;
        if (startCountingFreeTime)
        {
            currentFreeTime += Time.deltaTime;
            if (currentFreeTime >= freeTimeUntilDelete)
            {
                StartDissolve();
                currentFreeTime = 0;
            }
        }
    }

    public void BeginDeleteCountdown()
    {
        startCountingFreeTime = true;
    }
    void StartDissolve()
    {
        if (lifeLine)
        {
            currentState = ObjectStates.Decaying;
            if (dissolveScript)
            {
                dissolveScript.Dissolve();
                ObjectManager.m_Objects.Remove(gameObject);
                Destroy(gameObject, dissolveScript.dissolveDuration);
            }
            else
            {
                ObjectManager.m_Objects.Remove(gameObject);
                Destroy(gameObject, 1);
            }
            Destroy(this);
        }
    }

    public void PickupObject(handScript hand)
    {
        if (ignoreCollisionWithPlayer)
            SetIgnoreCollisions(true);
        handScript = hand;
        currentState = ObjectStates.Held;
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().enabled = true;
    }

    public GunUIScript GetUIScript()
    {
        return UIScript;
    }
}
