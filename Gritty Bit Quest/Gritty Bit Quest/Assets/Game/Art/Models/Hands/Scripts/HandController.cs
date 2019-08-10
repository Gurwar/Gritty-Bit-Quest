using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}
	

    public void SetGrabAnimation(bool grab)
    {
    if (animator != null)
        animator.SetBool("isGrabbing", grab);

    }


}
