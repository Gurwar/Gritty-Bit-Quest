using UnityEngine;
using System.Collections;
using System;

public class g_AIAnimationScript : MonoBehaviour 
{
    [SerializeField]
    Animator animator;
    int walkHash;
    int runHash;
    int attackHash;
    int attackTypeHash;
    int walkXHash;
    int walkYHash;
    int roarHash;
    int idleHash;
    int hitHash;
    int jumpLeftHash;
    int jumpRightHash;
    int fireArrowHash;
    int dieHash;
    int tauntHash;
    bool isAttackAnimationPlaying;
    bool isWalk;
    bool isRun;
    bool isAttack;
    float attackType;
    Vector2 walkDirection;
    bool isRoar;
    bool isIdle;
    bool isHit;
    bool isJumpLeft;
    bool isJumpRight;
    bool isFireArrow;
    bool isDie;
    bool isTaunt;
    [SerializeField]
    int attackNum;

    private void Start()
    {
        SetHashes();
    }


    void SetHashes()
    {
        walkHash = Animator.StringToHash("Walk");
        runHash = Animator.StringToHash("Run");
        attackHash = Animator.StringToHash("Attack");
        attackTypeHash = Animator.StringToHash("AttackType");
        walkXHash = Animator.StringToHash("WalkX");
        walkYHash = Animator.StringToHash("WalkY");

        idleHash = Animator.StringToHash("Idle");
        hitHash = Animator.StringToHash("Get Hit");
        jumpLeftHash = Animator.StringToHash("JumpLeft");
        jumpRightHash = Animator.StringToHash("JumpRight");
        fireArrowHash = Animator.StringToHash("FireArrow");
        dieHash = Animator.StringToHash("Die");
        roarHash = Animator.StringToHash("Roar");
        tauntHash = Animator.StringToHash("Taunt");
    }

    private void Update()
    {
        if (animator != null)
        {
            if (GameManager.ContainsParam(animator, "Walk"))
                animator.SetBool(walkHash, isWalk);
            if (GameManager.ContainsParam(animator, "WalkX"))
                animator.SetFloat(walkXHash, walkDirection.x);
            if (GameManager.ContainsParam(animator, "WalkY"))
                animator.SetFloat(walkYHash, walkDirection.y);
            if (GameManager.ContainsParam(animator, "Walk"))
                animator.SetBool(walkHash, isWalk);
            if (GameManager.ContainsParam(animator, "Run"))
                animator.SetBool(runHash, isRun);
            if (GameManager.ContainsParam(animator, "Attack"))
                animator.SetBool(attackHash, isAttack);
            if (GameManager.ContainsParam(animator, "AttackType"))
                animator.SetFloat(attackTypeHash, attackType);
            if (GameManager.ContainsParam(animator, "Idle"))
                animator.SetBool(idleHash, isIdle);
            if (GameManager.ContainsParam(animator, "Get Hit"))
                animator.SetBool(hitHash, isHit);
            if (GameManager.ContainsParam(animator, "JumpLeft"))
                animator.SetBool(jumpLeftHash, isJumpLeft);
            if (GameManager.ContainsParam(animator, "JumpRight"))
                animator.SetBool(jumpRightHash, isJumpRight);
            if (GameManager.ContainsParam(animator, "FireArrow"))
                animator.SetBool(fireArrowHash, isFireArrow);
            if (GameManager.ContainsParam(animator, "Die"))
                animator.SetBool(dieHash, isDie);
            if (GameManager.ContainsParam(animator, "Roar"))
                animator.SetBool(roarHash, isRoar);
            if (GameManager.ContainsParam(animator, "Taunt"))
                animator.SetBool(tauntHash, isTaunt);
        }
    }

    public void PlayHitAnimation()
    {
        if (!isHit)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = true;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }
    public void PlayIdleAnimation()
    {
        if (!isIdle)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = true;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayWalkAnimation()
    {
        if (!isWalk)
        {
            isWalk = true;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayRunAnimation()
    {
        if (!isRun)
        {
            isWalk = false;
            isRun = true;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayJumpLeft()
    {
        if (!isJumpLeft)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = true;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayJumpRight()
    {
        if (!isJumpRight)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = true;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayFireArrow()
    {
        if (!isFireArrow)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = true;
            isDie = false;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void PlayRoar()
    {
        if (!isRoar)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = true;
            isTaunt = false;

        }
    }

    public void PlayTaunt()
    {
        if (!isTaunt)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = false;
            isRoar = false;
            isTaunt = true;
        }
    }

    public void PlayDieAnimation()
    {
        if (!isDie)
        {
            isWalk = false;
            isRun = false;
            isAttack = false;
            isIdle = false;
            isHit = false;
            isJumpLeft = false;
            isJumpRight = false;
            isFireArrow = false;
            isDie = true;
            isRoar = false;
            isTaunt = false;

        }
    }

    public void SetWalkDirection(float x, float y)
    {
        walkDirection.x = x;
        walkDirection.y = y;
    }

    public void PlayAttackAnimation()
    {

        if (!isAttackAnimationPlaying)
        {
            int temp = UnityEngine.Random.Range(0, attackNum);
            if (temp != attackType)
            {

                if (!isAttack)
                {
                    //Debug.Log(temp);
                    attackType = temp;
                    isWalk = false;
                    isRun = false;
                    isAttack = true;
                    isIdle = false;
                    isHit = false;
                    isJumpLeft = false;
                    isJumpRight = false;
                    isFireArrow = false;
                    isDie = false;
                    isRoar = false;
                    isTaunt = false;
                    StartCoroutine(waitToSwitchIsActionPlaying());
                }
            }
        }
    }

    IEnumerator waitToSwitchIsActionPlaying()
    {
        isAttackAnimationPlaying = true;
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttackAnimationPlaying = false;
        isAttack = false;
    }

}
