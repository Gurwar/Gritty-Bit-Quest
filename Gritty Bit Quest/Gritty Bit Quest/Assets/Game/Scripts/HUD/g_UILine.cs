
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_UILine : MonoBehaviour {
    public LayerMask grabLayerMask;
    Ray ray;
    LineRenderer line;
    gameState gameStateScript;
	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(1, new Vector3(0, 0, 10));
        gameStateScript = GameObject.Find("Level Scripts").GetComponent<gameState>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        line.enabled = true;
        //if (gameStateScript.playerState != gameState.GameStates.Wave && gameStateScript.playerState != gameState.GameStates.Pregame)
        //{
        //    line.enabled = true;
        //    ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        //    {
        //        if (hit.transform.gameObject.tag == "Button")
        //        {
        //            if(hit.transform.GetComponent<VRButtons>()!= null)
        //            hit.transform.GetComponent<VRButtons>().Select();
        //        }
        //    }
        //}
        //else
        //    line.enabled = false;
    }

    public void ButtonPressed()
    {
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, grabLayerMask))
        {
            if (hit.transform.gameObject.tag == "Button")
            {
                GetComponent<AudioSource>().Play();
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public GameObject RayToDistantObjects()
    {
        //Shoot a raycast forward to check if the player is aiming at an object with their hand
        RaycastHit m_hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out m_hit, 10f, grabLayerMask))
        {
            if (m_hit.collider.gameObject.GetComponent<ObjectState>())
            {
                if (m_hit.collider.gameObject.GetComponent<ObjectState>().currentState != ObjectState.ObjectStates.Held)
                {
                    return m_hit.collider.gameObject;
                }
            }
        }
        return null;
    }
}
