using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class g_AIBehaviourScript : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    g_AIHealthScript healthScript;
    [SerializeField]
    g_AIAnimationScript animationScript;
    [SerializeField]
    AIBehavior aIBehavior;
    [SerializeField]
    AIAction.ActionState FirstAction;
    public float currentActionTime;
    public string currentActionName;
    AIAction currentAction;
    GameObject target;

    void Start()
    {
        target = GameManager.Player;

        for (int j = 0; j < aIBehavior.GetAIActions().Count; j++)
        {
            aIBehavior.ActionsDict.Add(aIBehavior.GetAIActions()[j].Action, aIBehavior.GetAIActions()[j]);
            aIBehavior.ActionsList[j].setActionTime();
        }
        currentAction = aIBehavior.ActionsDict[FirstAction];
        DisableColliders();
        DisableRigidbodies();
    }

    void Update()
    {
        currentActionTime += Time.deltaTime;
    }

    void EnableColliders()
    {
        healthScript.EnableColliders();
    }

    void DisableColliders()
    {
        healthScript.DisableColliders();
    }

    void EnableRigidbodies()
    {
        healthScript.EnableRigidbodies();
    }

    void DisableRigidbodies()
    {
        healthScript.DisableRigidbodies();
    }

    IEnumerator AnimationChangeState(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SwitchToNextAction(Switch.SwitchCondition.AnimationLength);
    }

    IEnumerator InitalSpawnOutOfGround(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        EnableColliders();
        EnableRigidbodies();
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
                StartCoroutine(AnimationChangeState(currentAction.Switches[i].AnimationLength));
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


    void SwitchToNextAction(Switch.SwitchCondition condition)
    {
        currentActionTime = 0;
        currentAction = aIBehavior.ActionsDict[currentAction.getNextAction(condition)];
        currentActionName = currentAction.Action.ToString();
        currentAction.setActionTime();
    }

    public void Spawn()
    {
        EvaluateSwitchConditions();
        StartCoroutine(InitalSpawnOutOfGround(currentAction.SwitchesDict[Switch.SwitchCondition.AnimationLength].AnimationLength));
    }

    public void Roar()
    {
        agent.speed = 0;
        animationScript.PlayRoarAnimation();
        EvaluateSwitchConditions();
    }

    public void Idle()
    {
        agent.speed = 0;
        animationScript.PlayRoarAnimation();
        EvaluateSwitchConditions();
    }

    public void RunAtTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        animationScript.PlayRunAnimation();
        EvaluateSwitchConditions();
    }

    public void WalkAtTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        animationScript.PlayWalkAnimation();
        EvaluateSwitchConditions();
    }

    public void WalkLeft()
    {
        agent.SetDestination(-transform.right * 10);
        animationScript.PlayWalkLeftAnimation();
        EvaluateSwitchConditions();
    }

    public void WalkRight()
    {
        agent.SetDestination(transform.right * 10);
        animationScript.PlayWalkRightAnimation();
        EvaluateSwitchConditions();
    }

    public void Attack()
    {
        agent.speed = 0;
        GetComponent<G_AIMeleeScript>().DoMeleeAttack();
        EvaluateSwitchConditions();
    }
}
