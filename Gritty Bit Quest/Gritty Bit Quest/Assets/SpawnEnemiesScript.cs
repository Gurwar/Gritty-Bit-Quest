using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> EnemiesToSpawn = new List<GameObject>();

    void Start()
    {
        foreach (Transform enemy in transform)
            EnemiesToSpawn.Add(enemy.gameObject);
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < EnemiesToSpawn.Count; i++)
            EnemiesToSpawn[i].SetActive(true);
    }

}
