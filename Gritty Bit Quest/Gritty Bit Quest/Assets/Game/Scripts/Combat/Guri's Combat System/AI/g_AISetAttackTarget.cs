using UnityEngine;
using System.Collections;

public class g_AISetAttackTarget : MonoBehaviour {
    [SerializeField]
    bool offSetAim;
	void Start () 
	{
        //if (offSetAim)
        //    GetComponent<g_AIBehaviourScript>().m_attackTarget = GameObject.Find("Target");
        //else
        //    GetComponent<g_AIBehaviourScript>().m_attackTarget = GameObject.Find("PlayerCamera");

       // if (GetComponent<g_AIBehaviourScript>().m_attackTarget == null)
         //   Debug.Break();
    }
}
