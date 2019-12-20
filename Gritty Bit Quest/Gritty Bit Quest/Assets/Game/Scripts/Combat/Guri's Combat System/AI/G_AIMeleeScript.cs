using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G_AIMeleeScript : MonoBehaviour 
{
    [SerializeField]
    LayerMask layerMask;
	[SerializeField]
	float meleeRange;
    [SerializeField]
    List<Transform> raycastPoints = new List<Transform>();
    // Update is called once per frame
    void Update () 
	{
        for (int i = 0; i < raycastPoints.Count; i++)
            Debug.DrawLine(raycastPoints[i].position, raycastPoints[i].transform.position + (meleeRange *raycastPoints[i].transform.forward));

    }

    public void SolveForMeleeAttack(float damage)
	{
        GameObject target = CheckRange();
        if (target)
        {
            //Debug.Log(GameManager.Player.name);
            //Debug.Log("hit");
            target.transform.root.SendMessage("Damage", damage, SendMessageOptions.RequireReceiver);
        }
	}

    GameObject CheckRange()
    {
        //Debug.Log("Check");
        for (int i = 0; i < raycastPoints.Count; i++)
        {
            Ray ray = new Ray(raycastPoints[i].position, raycastPoints[i].transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, meleeRange, layerMask, QueryTriggerInteraction.Ignore))
            {
                //Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "Player")
                {
                   // Debug.Log("Player");
                    return hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag == "Damageable Object")
                {
                    return hit.collider.gameObject;
                }
            }
        }
        return null;
    }
		
}
