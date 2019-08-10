using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_HealthBar : MonoBehaviour {
    Image healthImage;
    [SerializeField]
    g_AIHealthScript healthScript;
	// Use this for initialization
	void Start ()
    {
        healthImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //high health means high green
        // low health means high red
        if (healthScript != null)
        {
            healthImage.color = new Color(255 - (255 * (healthScript.health / healthScript.maxHealth)), 255 * (healthScript.health / healthScript.maxHealth), 0, 1);
            healthImage.fillAmount = healthScript.health / healthScript.maxHealth;
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //print(healthImage.color);
        }
    }
}
