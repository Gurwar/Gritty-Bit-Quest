using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class g_PlayerHealthScript : MonoBehaviour {
    public float baseHealth;
    public float CurrentHealth;
    public float MaxHealth;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightHand;
    [SerializeField]
    bool die;
	// Use this for initialization
	void Start ()
    {
        MaxHealth = baseHealth;
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {

        MaxHealth = baseHealth;
        if (die)
        {
            Damage(CurrentHealth);
            die = false;
        }
    }

    public void IncreaseHealth(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void Damage(float damage)
    {
        //Debug.Log("damage");
        //subtract damage from health
        CurrentHealth = Mathf.Min(CurrentHealth - damage, MaxHealth);
        //Post processing effects on hit
        //Vibrations in controllers
        LeftHand.GetComponent<TouchController>().Hit();
        RightHand.GetComponent<TouchController>().Hit();

        //Player death
        if (CurrentHealth <= 0.0f)
        { 
            //Clear screen
            LeftHand.GetComponent<TouchController>().StopVibration();
            RightHand.GetComponent<TouchController>().StopVibration();
            //gameStateScript.KillPlayer();
            //Destroy(this);
        }
    }
}
