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
    float upgradeHealthMultiplier;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightHand;
    //[SerializeField]
    //PPEManager LeftEyePPE;
    //[SerializeField]
    //PPEManager RightEyePPE;
    [SerializeField]
    gameState gameStateScript;
    //[SerializeField]
    //HoloController floor;
    [SerializeField]
    bool die;
    [SerializeField]
    g_UpgradeScreenManager upgrades;
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
        float red = 255 - 255 * CurrentHealth/MaxHealth;
        float blue = 255* CurrentHealth/MaxHealth;
    }

    public void IncreaseHealth(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void Damage(float damage)
    {
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
