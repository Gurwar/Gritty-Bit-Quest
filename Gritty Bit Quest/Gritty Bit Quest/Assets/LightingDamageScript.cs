using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingDamageScript : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float damage;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + (transform.forward*1000));
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.gameObject.transform.root.name == "Player")
            {
                hit.collider.gameObject.transform.root.GetComponent<g_PlayerHealthScript>().Damage(damage);
            }
            else if (hit.collider.gameObject.tag == "Boundary")
            {
                hit.collider.gameObject.GetComponent<BoundaryHealth>().Damage(damage);
            }
        }
    }
}
