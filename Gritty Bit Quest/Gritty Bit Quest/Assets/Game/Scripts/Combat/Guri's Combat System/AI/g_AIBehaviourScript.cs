using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class g_AIBehaviourScript : MonoBehaviour
{
   // [SerializeField]
   // NavMeshAgent agent;
   // [SerializeField]
   // g_AIHealthScript healthScript;
   // [SerializeField]
   // g_AIAnimationScript animationScript;
   // [SerializeField]
   // AIBehavior aIBehavior;
   // [SerializeField]
   // AIState FirstState;
   // public float currentActionTime;
   // public string currentActionName;
   // AIState currentState;
   // GameObject target;
   //
   // void Start()
   // {
   //     target = GameManager.Player;
   //
   //     for (int j = 0; j < aIBehavior.GetAIActions().Count; j++)
   //     {
   //         //aIBehavior.ActionsDict.Add(aIBehavior.GetAIActions()[j].Action, aIBehavior.GetAIActions()[j]);
   //        // aIBehavior.ActionsList[j].Initialize();
   //     }
   //     currentState = aIBehavior.ActionsDict[FirstState];
   //     DisableColliders();
   //     DisableRigidbodies();
   // }
   //
   // void Update()
   // {
   //     currentActionTime += Time.deltaTime;
   // }
   //
   // void EnableColliders()
   // {
   //     healthScript.EnableColliders();
   // }
   //
   // void DisableColliders()
   // {
   //     healthScript.DisableColliders();
   // }
   //
   // void EnableRigidbodies()
   // {
   //     healthScript.EnableRigidbodies();
   // }
   //
   // void DisableRigidbodies()
   // {
   //     healthScript.DisableRigidbodies();
   // }
   //
   // IEnumerator AnimationChangeState(float secondsToWait)
   // {
   //     yield return new WaitForSeconds(secondsToWait);
   //     SwitchToNextAction(Switch.SwitchCondition.AnimationLength);
   // }
   //
   // IEnumerator InitalSpawnOutOfGround(float secondsToWait)
   // {
   //     yield return new WaitForSeconds(secondsToWait);
   //     EnableColliders();
   //     EnableRigidbodies();
   // }
   //
   // void EvaluateSwitchConditions()
   // {
   //     for (int i = 0; i < currentState.Switches.Count; i++)
   //     {
   //         if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DoOnce)
   //         {
   //             SwitchToNextAction(Switch.SwitchCondition.DoOnce);
   //         }
   //         if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.AnimationLength)
   //         {
   //             StartCoroutine(AnimationChangeState(currentState.Switches[i].AnimationLength));
   //         }
   //         if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.DistanceToAttackTarget)
   //         {
   //             if (Vector3.Distance(transform.position, target.transform.position) < currentState.Switches[i].MinDistanceToTarget
   //                 && Vector3.Distance(transform.position, target.transform.position) > currentState.Switches[i].MaxDistanceToTarget)
   //             {
   //                 SwitchToNextAction(Switch.SwitchCondition.DistanceToAttackTarget);
   //             }
   //         }
   //         if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.MinionsKilled)
   //         {
   //             SwitchToNextAction(Switch.SwitchCondition.MinionsKilled);
   //         }
   //         if (currentState.Switches[i].switchCondition == Switch.SwitchCondition.Time)
   //         {
   //             if (currentActionTime >= currentState.Switches[i].ActionTime)
   //                 SwitchToNextAction(Switch.SwitchCondition.Time);
   //         }
   //     }
   // }
   //
   //
   // void SwitchToNextAction(Switch.SwitchCondition condition)
   // {
   //     currentActionTime = 0;
   //     currentState = aIBehavior.ActionsDict[currentState.getNextAction(condition)];
   //     currentActionName = currentState.Action.ToString();
   //     currentState.SetActionTime();
   // }
   //
   // public void Spawn()
   // {
   //     EvaluateSwitchConditions();
   //     StartCoroutine(InitalSpawnOutOfGround(currentState.SwitchesDict[Switch.SwitchCondition.AnimationLength].AnimationLength));
   // }
   //
   // public void Roar()
   // {
   //     agent.speed = 0;
   //     animationScript.PlayRoarAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void Idle()
   // {
   //     agent.speed = 0;
   //     animationScript.PlayRoarAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void RunAtTarget(GameObject target)
   // {
   //     agent.SetDestination(target.transform.position);
   //     animationScript.PlayRunAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void WalkAtTarget(GameObject target)
   // {
   //     agent.SetDestination(target.transform.position);
   //     animationScript.PlayWalkAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void WalkLeft()
   // {
   //     agent.SetDestination(-transform.right * 10);
   //     animationScript.PlayWalkLeftAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void WalkRight()
   // {
   //     agent.SetDestination(transform.right * 10);
   //     animationScript.PlayWalkRightAnimation();
   //     EvaluateSwitchConditions();
   // }
   //
   // public void Attack()
   // {
   //     agent.speed = 0;
   //     GetComponent<G_AIMeleeScript>().DoMeleeAttack();
   //     EvaluateSwitchConditions();
   // }
}
