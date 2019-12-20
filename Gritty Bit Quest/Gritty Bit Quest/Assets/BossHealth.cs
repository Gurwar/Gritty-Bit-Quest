using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    public float currentHealth;
    [SerializeField]
    SkinnedMeshRenderer mesh;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Damage(75);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void Damage(float damage)
    {
       currentHealth -= damage;
       mesh.material.SetColor("_Color", new Color((maxHealth/maxHealth - currentHealth/maxHealth), 0, 0, 0.18f));
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.GetComponent<CubeBehaviour>() && collider.gameObject.GetComponent<CubeBehaviour>().GetDidInitialHit())
        {
            Damage(25);
            collider.gameObject.GetComponent<CubeBehaviour>().SelfDestruct();
        }
    }
}
