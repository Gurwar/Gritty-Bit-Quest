using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadAreaCheck : MonoBehaviour
{
    [SerializeField]
    bool IsInBox;

    public bool GetIsInBox()
    {
        return IsInBox;
    }

    public void SetIsInBox(bool b)
    {
        IsInBox = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PatrolCheck")
        {
            //Debug.Break();
            IsInBox = true;
        }
    }

    // every patrol point we need to spawn an object in here and check if it triggers 
    // 
}
