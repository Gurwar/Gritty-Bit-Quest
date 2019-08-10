using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> boundaries = new List<GameObject>();
    bool destroyed;

    public bool GetDestroyed()
    {
        return destroyed;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Boundaries = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
            return;
        int numDestroyed = 0;
        for (int i = 0; i < boundaries.Count; i++)
        {
            if (boundaries[i].GetComponent<BoundaryHealth>().GetHealth() <= 0)
            {
                numDestroyed++;
            }
        }
        if (numDestroyed == boundaries.Count)
        {
            destroyed = true;
        }
    }
}
