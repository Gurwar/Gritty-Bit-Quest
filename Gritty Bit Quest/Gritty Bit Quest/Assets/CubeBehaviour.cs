using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField]
    bool explodable;
    [SerializeField]
    GameObject UI;
    bool didInitialHit = false;
    g_ObjectManager objectManager;
    [SerializeField]
    float selfDestructTime;
    float currentSelfDestructTime;
    bool selfDestruct = false;
    [SerializeField]
    Color defaultColor;
    Color color;
    [SerializeField]
    Renderer renderer;
    bool pullingTowardsBoss;
    GameObject boss;
    [SerializeField]
    float pullTowardsBossSpeed;
    float pushTowardsPlayerSpeed;
    bool isShot;
    Vector3 directionToMove;
    [SerializeField]
    GameObject GroundExplosionPrefab;
    [SerializeField]
    GameObject HandExplosionPrefab;
    [HideInInspector]
    public float maxDamage;
    [HideInInspector]
    public float maxDistanceToDamage;
    [HideInInspector]
    public float distanceMultiplier;
    float distanceToPlayer;
    ObjectState.ObjectStates currentState;
    void Start()
    {
        renderer.material.SetColor("_Color", defaultColor);
    }

    public void SetSpeedToPlayer(float speed)
    {
        pushTowardsPlayerSpeed = speed;
    }

    public void SetDirectionToMove(float inaccuracy, GameObject target)
    {
        directionToMove = (target.transform.position - transform.position);
        directionToMove += new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));
        directionToMove.Normalize();
    }

    void FixedUpdate()
    {
        if (currentState == ObjectState.ObjectStates.MovingTowardsBoss)
        {
            GetComponent<Rigidbody>().velocity = directionToMove * pullTowardsBossSpeed * Time.deltaTime;
        }
        else if (currentState == ObjectState.ObjectStates.MovingTowardsPlayer)
        {
            GetComponent<Rigidbody>().velocity = (directionToMove * pushTowardsPlayerSpeed * Time.deltaTime);
        }
    }

    void Update()
    {

        currentState = GetComponent<ObjectState>().currentState;

        distanceToPlayer = (Vector3.Distance(GameManager.Player.transform.position, transform.position));
        if (explodable)
        {
            if (selfDestruct)
            {
                currentSelfDestructTime += Time.deltaTime;
                color = Color.Lerp(defaultColor, Color.red, currentSelfDestructTime / selfDestructTime);
                renderer.material.SetColor("_Color", color);
                if (currentSelfDestructTime >= selfDestructTime)
                {
                    SelfDestruct();
                }
            }
        }
    }

    public void SetIsShot(bool s)
    {
        isShot = s;
    }

    public bool GetIsShot()
    {
        return isShot;
    }

    public void SetDidInitialHit(bool d)
    {
        didInitialHit = d;
    }

    public bool GetDidInitialHit()
    {
        return didInitialHit;
    }

    public void SetSelfDestructTimer(float t)
    {
        selfDestructTime = t;
    }

    public void SetSelfDestruct(bool s)
    {
        selfDestruct = s;
    }

    public ObjectState.ObjectStates GetCubeState()
    {
        return GetComponent<ObjectState>().currentState;
    }

    public void SetCubeState(ObjectState.ObjectStates state)
    {
        GetComponent<ObjectState>().currentState = state;
    }

    public void SelfDestruct()
    {
        if (GetComponent<ObjectState>().GetHandScript() != null)
        {
            GetComponent<ObjectState>().GetHandScript().DropObject();
            Instantiate(HandExplosionPrefab, transform.position, Quaternion.identity);
            GameManager.Player.GetComponent<g_PlayerHealthScript>().Damage(maxDamage);
        }
        else
        {
            Instantiate(GroundExplosionPrefab, transform.position, Quaternion.identity);
            if (GameManager.GetInRange(GameManager.Player.transform.position, transform.position, 5))
            {
                GameManager.Player.GetComponent<g_PlayerHealthScript>().Damage(maxDamage - (distanceToPlayer * distanceMultiplier));
            }
        }
        Destroy(gameObject);
    }

    public void SetBoss(GameObject b)
    {
        boss = b;
    }


    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Terrain" && !didInitialHit)
        {
            UI.SetActive(true);
            objectManager = GameObject.Find("Object Manager").GetComponent<g_ObjectManager>();
            objectManager.m_Objects.Add(gameObject);
            GetComponent<Rigidbody>().useGravity = true;
            SetSelfDestruct(true);
            didInitialHit = true;
            SetCubeState(ObjectState.ObjectStates.OnGround);
        }
        else if (collider.transform.root.gameObject.name == "Player")
        {
            collider.transform.root.gameObject.GetComponent<g_PlayerHealthScript>().Damage(maxDamage);
            if (explodable)
                SelfDestruct();
            else
            {
                UI.SetActive(true);
                objectManager = GameObject.Find("Object Manager").GetComponent<g_ObjectManager>();
                objectManager.m_Objects.Add(gameObject);
                GetComponent<Rigidbody>().useGravity = true;
                SetSelfDestruct(true);
                didInitialHit = true;
                SetCubeState(ObjectState.ObjectStates.Free);
            }
        }
    }
}
