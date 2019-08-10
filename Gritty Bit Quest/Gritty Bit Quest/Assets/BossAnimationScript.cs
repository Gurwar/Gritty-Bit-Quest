using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationScript : MonoBehaviour
{
    [SerializeField]
    Animator animationController;
    int idle;
    int lightning;
    // Start is called before the first frame update
    void Start()
    {
        SetHashes();
    }

    void SetHashes()
    {
        idle = Animator.StringToHash("Idle");
        lightning = Animator.StringToHash("Lighting");
    }

    public void PlayIdle()
    {
        animationController.SetBool(idle, true);
        animationController.SetBool(lightning, false);
    }

    public void PlayLightning()
    {
        animationController.SetBool(idle, false);
        animationController.SetBool(lightning, true);
    }
}
