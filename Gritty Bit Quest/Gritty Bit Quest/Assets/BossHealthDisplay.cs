using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthDisplay : MonoBehaviour
{
    Text myText;
    [SerializeField]
    int health;
    GameObject boss;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        boss = GameManager.Boss;

        if (boss.GetComponent<BossHealth>() != null)
        {
            health = (int)boss.GetComponent<BossHealth>().GetCurrentHealth();
        }
        else
            health = 0;

        myText.text = health.ToString();
    }
}
