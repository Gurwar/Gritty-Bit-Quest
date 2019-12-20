using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionPrefab;
    [SerializeField]
    bool explosive;
    [SerializeField]
    float explosionTime;
    float currentTime;
    public void SetExplosive()
    {
        explosive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (explosive)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= explosionTime)
            {
                GameObject explosion = (GameObject)Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
