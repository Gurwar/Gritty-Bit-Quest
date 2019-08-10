using UnityEngine;
using System.Collections;

public class g_AIAnimationScript : MonoBehaviour 
{
	Animator animator;
    
	int attack1Hash;
	int crouchingHash;
	int runHash;
	int shotHash;
	int dieHash;
    int roarHash;
    int walkHash;
    int walkLeftHash;
    int walkRightHash;
    int spawnHash;
	bool isRunning;
	bool doShotAnimation;
	float shotAnimationTime = 0.5f;
	float currentShotAnimationTime;
	void SetHashes()
	{
		attack1Hash = Animator.StringToHash("Attack");
		crouchingHash = Animator.StringToHash("Crouching");
		runHash = Animator.StringToHash("Run");
		shotHash = Animator.StringToHash("GotShot");
		dieHash = Animator.StringToHash("Die");
        roarHash = Animator.StringToHash("Roar");
        walkHash = Animator.StringToHash("Walk");
        walkLeftHash = Animator.StringToHash("WalkLeft");
        walkRightHash = Animator.StringToHash("WalkRight");

    }
    // Use this for initialization
    void Start () 
	{
		animator = GetComponent<Animator> ();
		SetHashes ();
	}


	public void PlayRunAnimation()
	{
		animator.SetTrigger (runHash);
	}
    public void PlayWalkAnimation()
    {
        animator.SetTrigger(walkHash);
    }
    public void PlayWalkLeftAnimation()
    {
        animator.SetTrigger(walkLeftHash);
    }
    public void PlayWalkRightAnimation()
    {
        animator.SetTrigger(walkRightHash);
    }

    public void PlayAttackAnimation()
	{
		animator.SetTrigger (crouchingHash);
		animator.SetTrigger (attack1Hash);
	}

	public void PlayCrouchAnimation()
	{
		animator.SetTrigger (crouchingHash);
	}

	public void PlayGotShotAnimation()
	{
		doShotAnimation = true;
		animator.SetTrigger(shotHash);
	}

    public void PlayRoarAnimation()
    {
        animator.SetTrigger(roarHash);
    }
}
