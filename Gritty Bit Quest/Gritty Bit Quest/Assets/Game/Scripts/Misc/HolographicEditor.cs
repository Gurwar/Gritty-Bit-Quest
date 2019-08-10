using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolographicEditor : MonoBehaviour {

    public void ChangeHologramColor(Color newColor)
    {
        //print(newColor);
        Material mat = GetComponent<Renderer>().material;
        mat.SetColor("_Color", newColor);

    }
}
