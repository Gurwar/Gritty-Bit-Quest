using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLimb : MonoBehaviour
{
    GameObject hit;

    public GameObject GetHitObject()
    {
        return hit;
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.root.name == "Player")
        {
            hit = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.name == "Player")
        {
            hit = null;
        }
    }
}
