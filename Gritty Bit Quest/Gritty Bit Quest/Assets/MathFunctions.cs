using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MathFunctions
{
    
    public static Vector3 MultiplyVector3(Vector3 one, Vector3 two)
    {
        return new Vector3(one.x * two.x, one.y * two.y, one.z * two.z);
    }
}
