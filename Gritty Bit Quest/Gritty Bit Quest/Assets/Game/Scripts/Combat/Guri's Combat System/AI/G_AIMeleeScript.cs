using UnityEngine;
using System.Collections;

public class G_AIMeleeScript : MonoBehaviour 
{
	[SerializeField]
	float m_meleeRange;
	[SerializeField]
	float m_damage;
	[SerializeField]
	float m_attackRate;
	float m_currentAttackTime;
	// Update is called once per frame
	void Update () 
	{
		m_currentAttackTime += Time.deltaTime;
	}

	public void DoMeleeAttack()
	{
        if (m_currentAttackTime >= m_attackRate)
        {
            //GetComponent<g_AISoundScript> ().PlayAttackSound ();
            GetComponent<g_AIAnimationScript>().PlayAttackAnimation();
            //GetComponent<g_AIBehaviourScript>().m_attackTarget.SendMessageUpwards("Damage", m_damage, SendMessageOptions.DontRequireReceiver);
            m_currentAttackTime = 0;
        }
	}
		
}
