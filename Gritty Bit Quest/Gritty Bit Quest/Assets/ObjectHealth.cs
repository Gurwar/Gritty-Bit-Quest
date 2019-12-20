using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float baseHealth;
    public float CurrentHealth;
    public float MaxHealth;
    [SerializeField]
    bool die;
    // Use this for initialization
    void Start()
    {
        MaxHealth = baseHealth;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        MaxHealth = baseHealth;
        if (die)
        {
            Damage(CurrentHealth);
            die = false;
        }
    }

    public void Damage(float damage)
    {
        //subtract damage from health
        CurrentHealth = Mathf.Min(CurrentHealth - damage, MaxHealth);

        //Player death
        if (CurrentHealth <= 0.0f)
        {
            //Clear screen
            //gameStateScript.KillPlayer();
            Destroy(gameObject);
        }
    }
}
