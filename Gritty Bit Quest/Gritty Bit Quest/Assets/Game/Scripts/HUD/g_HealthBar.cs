using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_HealthBar : MonoBehaviour {
    Slider healthImage;
    [SerializeField]
    g_AIHealthScript healthScript;
	// Use this for initialization
	void Start ()
    {
        healthImage = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //high health means high green
        // low health means high red
        if (healthScript != null)
        {
            healthImage.value = healthScript.health;
            //transform.eulerAngles = new Vector3(0, 0, 0);
            //print(healthImage.color);
        }
    }
}
