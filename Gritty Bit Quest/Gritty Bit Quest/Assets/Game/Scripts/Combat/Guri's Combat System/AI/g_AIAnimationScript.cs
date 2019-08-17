using UnityEngine;
using System.Collections;

public class g_AIAnimationScript : MonoBehaviour 
{
	Animator animator;
    int runHash;
    int attack1Hash;
	int crouchingHash;
    int idleHash;
	int shotHash;
	int dieHash;
    int roarHash;
    int walkHash;
    int walkLeftHash;
    int walkRightHash;
    int spawnHash;
	bool isRunning;
    bool isAttack1;
    bool isCrouch;
    bool isIdle;
    bool isShot;
    bool isDie;
    bool isRoar;
    bool isWalk;
    bool isWalkLeft;
    bool isWalkRight;
    bool isSpawn;
	bool doShotAnimation;
	float shotAnimationTime = 0.5f;
	float currentShotAnimationTime;
	void SetHashes()
	{
		attack1Hash = Animator.StringToHash("Attack");
		crouchingHash = Animator.StringToHash("Crouching");
        idleHash = Animator.StringToHash("Idle");
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
    public void PlayRoarAnimation()
    {
        if (!isRunning)
        {
            animator.SetBool(roarHash, true);

            isRunning = true;
            isAttack1 = false;
            isCrouch = false;
            isIdle = false;
            isShot = false;
            isDie = false;
            isRoar = false;
            isWalk = false;
            isWalkLeft = false;
            isWalkRight = false;
            isSpawn = false;
        }
    }
    public void PlayIdleAnimation()
    {
        animator.SetBool(idleHash, true);
    }
    public void PlayRunAnimation()
	{
		animator.SetBool (runHash, true);
	}
    public void PlayWalkAnimation()
    {
        animator.SetBool(walkHash, true);
    }
    public void PlayWalkLeftAnimation()
    {
        animator.SetBool(walkLeftHash, true);
    }
    public void PlayWalkRightAnimation()
    {
        animator.SetBool(walkRightHash, true);
    }
    public void PlayAttackAnimation()
	{
		animator.SetBool (crouchingHash, true);
		animator.SetBool (attack1Hash, true);
	}
	public void PlayCrouchAnimation()
	{
		animator.SetBool (crouchingHash, true);
	}
	public void PlayGotShotAnimation()
	{
		doShotAnimation = true;
		animator.SetBool(shotHash, true);
	}
}
