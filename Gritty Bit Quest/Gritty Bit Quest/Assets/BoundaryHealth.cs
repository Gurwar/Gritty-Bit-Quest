using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Damage(1000);
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        GetComponent<Renderer>().material.SetColor("_Color", new Color(((maxHealth / maxHealth) - (currentHealth / maxHealth)), 0, 0, 0.51f));

        if (currentHealth <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
