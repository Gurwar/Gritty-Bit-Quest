using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class g_AIBehaviourScript : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
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
        currentAction = aIBehavior.ActionsDict[FirstAction];
    }

    void Update()
    {
        currentActionTime += Time.deltaTime;
    }

    void EnableColliders()
    {

    }

    void DisableColliders()
    {

    }

    void EnableRigidbodies()
    {

    }

    void DisableRigidbodies()
    {

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


    void SwitchToNextAction(Switch.SwitchCondition condition)
    {
        currentActionTime = 0;
        currentAction = aIBehavior.ActionsDict[currentAction.getNextAction(condition)];
        currentActionName = currentAction.Action.ToString();
        currentAction.setActionTime();
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

    public void Roar()
    {
        agent.speed = 0;
        animationScript.PlayRoarAnimation();
        EvaluateSwitchConditions();
    }
}
