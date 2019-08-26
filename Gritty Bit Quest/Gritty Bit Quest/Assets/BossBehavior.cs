using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBehavior : MonoBehaviour
{
    public List<BossPhase> bossPhases;
    [SerializeField]
    BossPhase currentBossPhase;
    [SerializeField]
    AIState currentState;
    [SerializeField]
    float attackForce;
    [SerializeField]
    int attackNumber;
    [SerializeField]
    ProjectileManager projectile;
    [SerializeField]
    EnemyPrefabHolder enemies;
    [SerializeField]
    float inaccuracy;
    GameObject attackTarget;
    [SerializeField]
    Transform moveTarget;
    [HideInInspector]
    public float currentActionTime;
    bool AbsorbColliderActive;
    [SerializeField]
    GameObject absorbSphere;
    [SerializeField]
    GameObject AbsorbEffect;
    [SerializeField]
    GameObject AttackEffect;
    [SerializeField]
    GameObject LightningEffect;
    [SerializeField]
    Transform LightningEffectStartTransform;
    [SerializeField]
    BossAnimationScript animationScript;
    [SerializeField]
    Animation animation;
    [SerializeField]
    float LightingFollowSpeed;
    bool canSpawnEnemies = true;
    
    delegate void ActionFunc();
    ActionFunc currentStateFunc;
    Dictionary<AIAction.ActionState, ActionFunc> ActionFunctionsDict = new Dictionary<AIAction.ActionState, ActionFunc>();

    void Start()
    {
        attackTarget = GameManager.Player;
        ActionFunctionsDict.Add(AIAction.ActionState.Idle, Idle);
        ActionFunctionsDict.Add(AIAction.ActionState.MoveToTransform, MoveToTransform);
        ActionFunctionsDict.Add(AIAction.ActionState.NextState, NextState);
        ActionFunctionsDict.Add(AIAction.ActionState.ResetCubes, ResetCubes);
        ActionFunctionsDict.Add(AIAction.ActionState.Absorb, Absorb);
        ActionFunctionsDict.Add(AIAction.ActionState.CubeAttack, CubeAttack);
        ActionFunctionsDict.Add(AIAction.ActionState.LightningAttack, LightningAttack);
        ActionFunctionsDict.Add(AIAction.ActionState.Animation, Animation);

        for (int i = 0; i < bossPhases.Count; i++)
        {
            for (int j = 0; j < bossPhases[i].GetAIStates().Count; j++)
            {
                bossPhases[i].StatesDict.Add(bossPhases[i].GetAIStates()[j].Name, bossPhases[i].GetAIStates()[j]);
                bossPhases[i].StatesList[j].Initialize();
            }
        }

        if (attackNumber > projectile.GetProjectileLimit())
            attackNumber = projectile.GetProjectileLimit();
        currentBossPhase = bossPhases[0];
        currentState = currentBossPhase.StatesDict[currentBossPhase.firstState];
        SetupCurrentActionFunc();
        currentState.SetActionTime();
    }

    void SetupCurrentActionFunc()
    {
        for (int i = 0; i < currentState.Actions.Count; i++)
        {
            if (ActionFunctionsDict.ContainsKey(currentState.Actions[i].Action))
            {
                currentStateFunc += ActionFunctionsDict[currentState.Actions[i].Action];
            }
        }
    }

    void SwitchToNextState(Switch.SwitchCondition condition)
    {
        Debug.Log(condition.ToString());
        currentActionTime = 0;   
        currentState = currentBossPhase.StatesDict[currentState.GetNextState(condition)];
        currentState.SetActionTime();
        currentStateFunc = null;
        SetupCurrentActionFunc();
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentAttackTarget();
        currentStateFunc();
        absorbSphere.SetActive(AbsorbColliderActive);
    }

    private void SetCurrentAttackTarget()
    {
        if (GameManager.Player.GetComponent<PlayerMovement>().GetInCar())
        {
            attackTarget = GameManager.Car;
        }
        else
        {
            attackTarget = GameManager.Player;
        }
    }

    public float GetInaccuracy()
    {
        return inaccuracy;
    }

    void EvaluateActionSettings()
    {
        
    }
    void EvaluateSwitchConditions()
    {
        for (int i = 0; i < currentState.Switches.Count; i++)
        {
            if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DoOnce)
            {
                SwitchToNextState(Switch.SwitchCondition.DoOnce);
            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.AnimationLength)
            {
                if (currentActionTime >= currentState.Switches[i].AnimationLength)
                {
                    SwitchToNextState(Switch.SwitchCondition.AnimationLength);
                }
            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DistanceToAttackTarget)
            {
                if (Vector3.Distance(transform.position, attackTarget.transform.position) < currentState.Switches[i].MinDistanceToTarget
                    || Vector3.Distance(transform.position, attackTarget.transform.position) > currentState.Switches[i].MaxDistanceToTarget)
                {
                    SwitchToNextState(Switch.SwitchCondition.DistanceToAttackTarget);
                }

            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DistanceToMoveTarget)
            {
                if (Vector3.Distance(transform.position, moveTarget.position) < currentState.Switches[i].MinDistanceToTarget
                    || Vector3.Distance(transform.position, moveTarget.position) > currentState.Switches[i].MaxDistanceToTarget)
                {
                    SwitchToNextState(Switch.SwitchCondition.DistanceToMoveTarget);
                }
            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DistanceToPlayer)
            {
                
                if (Vector3.Distance(transform.position, GameManager.Player.transform.position) < currentState.Switches[i].MinDistanceToTarget
                    || Vector3.Distance(transform.position, GameManager.Player.transform.position) > currentState.Switches[i].MaxDistanceToTarget)
                {
                    SwitchToNextState(Switch.SwitchCondition.DistanceToPlayer);
                }
            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.MinionsKilled)
            {
                SwitchToNextState(Switch.SwitchCondition.MinionsKilled);
            }
            else if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.Time)
            {
                if (currentActionTime >= currentState.Switches[i].ActionTime)
                    SwitchToNextState(Switch.SwitchCondition.Time);
            }

        }
    }

    void Idle()
    {
        animationScript.PlayIdle();
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    void MoveToTransform()
    {
        //transform.position = Vector3.Lerp(transform.position, moveTarget.transform.position, moveSpeed* Time.deltaTime);
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    void ResetCubes()
    {
        animationScript.PlayIdle();
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);
        projectile.ResetCubes();
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    void Absorb()
    {
        animationScript.PlayIdle();

        AbsorbColliderActive = true;
        AbsorbEffect.SetActive(true);
        LightningEffect.SetActive(false);

        if (currentActionTime == 0)
        {
            projectile.PullTowardsBoss();
        }
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    void CubeAttack()
    {
        animationScript.PlayIdle();
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);

        if (currentActionTime == 0)
        {
            Instantiate(AttackEffect, transform.position, AttackEffect.transform.rotation);
        }

        currentActionTime += Time.deltaTime;

        for (int i = 0; i < projectile.GetProjectileLimit(); i++)
        {
            //aim at player position, select projectile, unparent it and shoot it at player with rigidbody
            GameObject proj = projectile.GetRandomProjectile();
            if (proj != null)
            {
                projectile.RemoveProjectileFromList(proj);
                Destroy(proj.GetComponent<CubeRotate>());
                proj.transform.parent = null;
                proj.GetComponent<CubeBehaviour>().SetCubeState(ObjectState.ObjectStates.MovingTowardsPlayer);
                proj.GetComponent<CubeBehaviour>().SetSpeedToPlayer(attackForce);
                proj.GetComponent<CubeBehaviour>().SetDirectionToMove(inaccuracy, attackTarget.transform.position);
                proj.GetComponent<CubeBehaviour>().SetIsShot(true);
            }
        }
        EvaluateSwitchConditions();
    }

    void LightningAttack()
    {
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);

        LightningEffect.transform.position = LightningEffectStartTransform.position;
        LightningEffect.SetActive(true);
        animationScript.PlayLightning();
        if (currentActionTime == 0)
        {
            Lightning(99999);
        }
        else
        {
            Lightning(LightingFollowSpeed);
        }
        currentActionTime += Time.deltaTime;

        EvaluateSwitchConditions();
    }

    void Animation()
    {
        
        animation.Play(currentState.ActionsDict[AIAction.ActionState.Animation].AnimationClip.name);
    }

    void NextState()
    {
        for (int i = 0; i < bossPhases.Count; i++)
        {
            if (currentBossPhase == bossPhases[i])
            {
                currentBossPhase = bossPhases[i + 1];
                break;
            }
        }
        currentState = currentBossPhase.StatesDict[currentBossPhase.firstState];
        SetupCurrentActionFunc();
        currentActionTime = 0;
    }

    
    void SpawnEnemies()
    {
        //if (canSpawnEnemies)
        //{
        //    for (int i = 0; i < currentAction.EnemyAmount; i++)
        //    {
        //        Instantiate(enemies.Charger);
        //    }
        //}
        canSpawnEnemies = false;

        //if enemies killed move to next State
        EvaluateSwitchConditions();
    }

    void Lightning(float speed)
    {
        Vector3 targetDir = GameManager.Player.transform.position - LightningEffect.transform.position;
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(LightningEffect.transform.forward, targetDir, step, 0.0f);

        // Move our position a step closer to the target.
        LightningEffect.transform.rotation = Quaternion.LookRotation(newDir);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeBehaviour>())
        {
            if(other.GetComponent<CubeBehaviour>().GetDidInitialHit() && AbsorbColliderActive)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<CubeBehaviour>().SetCubeState(ObjectState.ObjectStates.MovingTowardsBoss);
                other.GetComponent<CubeBehaviour>().SetDirectionToMove(0, gameObject.transform.position);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CubeBehaviour>())
        {
            if (!AbsorbColliderActive && other.GetComponent<CubeBehaviour>().GetCubeState() == ObjectState.ObjectStates.MovingTowardsBoss)
            {
                other.GetComponent<CubeBehaviour>().SetCubeState(ObjectState.ObjectStates.Free);
                other.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}
