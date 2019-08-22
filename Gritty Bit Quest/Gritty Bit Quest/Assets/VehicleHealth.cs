using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHealth : MonoBehaviour
{
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float currentHealth;
    [SerializeField]
    GhostScript ghost;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        ghost.AttemptToGetOutOfCar();
        Destroy(gameObject);
    }
}
