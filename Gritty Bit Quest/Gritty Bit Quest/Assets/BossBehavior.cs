using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBehavior : MonoBehaviour
{
    public List<BossState> bossStates;
    [SerializeField]
    BossState currentBossState;
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
    GameObject target;
    [HideInInspector]
    public float currentActionTime;
    [HideInInspector]
    public string currentActionName;
    [HideInInspector]
    public string currentBossStateName;
    AIAction currentAction;
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
    float LightingFollowSpeed;
    bool canSpawnEnemies = true;
    void Start()
    {
        target = GameManager.Player;
        for (int i = 0; i < bossStates.Count; i++)
        {
            for (int j = 0; j < bossStates[i].GetAIActions().Count; j++)
            {
                bossStates[i].ActionsDict.Add(bossStates[i].GetAIActions()[j].Action, bossStates[i].GetAIActions()[j]);
                bossStates[i].ActionsList[j].setActionTime();
            }
        }

        if (attackNumber > projectile.GetProjectileLimit())
            attackNumber = projectile.GetProjectileLimit();
        currentBossState = bossStates[0];
        currentAction = currentBossState.ActionsDict[currentBossState.firstAction];
        currentAction.setActionTime();
    }

    void SwitchToNextAction(Switch.SwitchCondition condition)
    {
        currentActionTime = 0;   
        currentAction = currentBossState.ActionsDict[currentAction.getNextAction(condition)];
        currentActionName = currentAction.Action.ToString();
        currentAction.setActionTime();
    }

    void SwitchToNextAction(AIAction.ActionState aIAction)
    {
        currentActionTime = 0;
        currentAction = currentBossState.ActionsDict[aIAction];
        currentActionName = currentAction.Action.ToString();
        currentAction.setActionTime();
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentState();
        absorbSphere.SetActive(AbsorbColliderActive);
    }

    private void SetCurrentState()
    {
        currentBossStateName = currentBossState.state.ToString();

        if (currentBossState == bossStates[0])
        {
            if (GetComponent<BossHealth>().GetCurrentHealth() <= 0)
            {
                currentBossState = bossStates[1];
                currentBossStateName = currentBossState.state.ToString();
                currentAction = currentBossState.ActionsDict[bossStates[1].firstAction];
                currentActionName = currentAction.Action.ToString();
                currentActionTime = 0;
            }
        }
        else if (currentBossState == bossStates[1])
        {
            if (GameManager.Boundaries.GetComponent<BoundaryManager>().GetDestroyed())
            {
                currentBossState = bossStates[2];
                currentBossStateName = currentBossState.state.ToString();
                currentAction = currentBossState.ActionsDict[bossStates[2].firstAction];
                currentActionName = currentAction.Action.ToString();
                currentActionTime = 0;
            }
        }
    }

    public float GetInaccuracy()
    {
        return inaccuracy;
    }

    public void Rest()
    {
        animationScript.PlayIdle();
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    void EvaluateSwitchConditions()
    {
        for (int i = 0; i < currentAction.Switches.Count; i++)
        {
            if (currentAction.Switches[i].switchCondition == Switch.SwitchCondition.Trigger)
            {
                SwitchToNextAction(Switch.SwitchCondition.Trigger);
            }
            if (currentAction.Switches[i].switchCondition == Switch.SwitchCondition.AnimationLength)
            {
                if (currentActionTime >= currentAction.Switches[i].AnimationLength)
                {
                    SwitchToNextAction(Switch.SwitchCondition.AnimationLength);
                }
            }
            if (currentAction.Switches[i].switchCondition == Switch.SwitchCondition.DistanceToTarget)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < currentAction.Switches[i].MinDistanceToTarget
                    && Vector3.Distance(transform.position, target.transform.position) > currentAction.Switches[i].MaxDistanceToTarget)
                {
                    SwitchToNextAction(Switch.SwitchCondition.DistanceToTarget);
                }
            }
            if (currentAction.Switches[i].switchCondition == Switch.SwitchCondition.MinionsKilled)
            {
                SwitchToNextAction(Switch.SwitchCondition.MinionsKilled);
            }
            if (currentAction.Switches[i].switchCondition == Switch.SwitchCondition.Time)
            {
                if (currentActionTime >= currentAction.Switches[i].ActionTime)
                    SwitchToNextAction(Switch.SwitchCondition.Time);
            }

        }
    }

    public void ResetCubes()
    {
        animationScript.PlayIdle();
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);
        LightningEffect.SetActive(false);
        projectile.ResetCubes();
        currentActionTime += Time.deltaTime;
        EvaluateSwitchConditions();
    }

    public void CubeAbsorb()
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

    public void CubeAttack()
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
                proj.GetComponent<CubeBehaviour>().SetDirectionToMove(inaccuracy, GameManager.Player);
                proj.GetComponent<CubeBehaviour>().SetIsShot(true);
            }
        }
        EvaluateSwitchConditions();
    }

    public void LightningAttack()
    {
        AbsorbColliderActive = false;
        AbsorbEffect.SetActive(false);

        LightningEffect.transform.position = LightningEffectStartTransform.position;
        LightningEffect.SetActive(true);
        animationScript.PlayLightning();
        if (currentActionTime == 0)
        {
            //snap to playerPosition
            Lightning(99999);
        }
        else
        {
            Lightning(LightingFollowSpeed);
        }
        currentActionTime += Time.deltaTime;

        EvaluateSwitchConditions();
    }

    public void SpawnEnemies()
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
                other.GetComponent<CubeBehaviour>().SetDirectionToMove(0, gameObject);
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
