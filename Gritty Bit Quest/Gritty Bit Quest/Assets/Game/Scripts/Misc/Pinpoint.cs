using UnityEngine;
using System.Collections;

public class Pinpoint : MonoBehaviour {
    [SerializeField]
    Color color;
	void OnDrawGizmos()
	{
        Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, .5f);
	}
}
