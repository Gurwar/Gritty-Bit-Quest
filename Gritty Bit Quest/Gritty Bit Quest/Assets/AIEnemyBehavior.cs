using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIEnemyBehavior : MonoBehaviour
{
    [SerializeField]
    AIBehavior aIBehavior;
    [SerializeField]
    AIState currentState;
    [SerializeField]
    NavMeshAgent agent;
    GameObject closestPlayer;
    GameObject attackTarget;
    [HideInInspector]
    public float currentStateTime;
    g_AIAnimationScript animationScript;
    bool hitThisFrame;
    delegate void ActionFunc();
    ActionFunc currentStateFunc;
    Dictionary<AIAction.ActionState, ActionFunc> ActionFunctionsDict = new Dictionary<AIAction.ActionState, ActionFunc>();
    private bool alive = true;
    [SerializeField]
    float remainingDistance;
    [SerializeField]
    float distanceToPlayer;
    Vector3 moveTarget;
    enum MovementTarget { Player, Patrol};
    [SerializeField]
    Transform AIHead;
    [SerializeField]
    LayerMask lineOfSightMask;
    [SerializeField]
    float lineOfSightAngle;
    [SerializeField]
    SquadAreaCheck patrolArea;
    [SerializeField]
    GameObject boxCheck;
    bool patrolCheckFrame;
    GameObject currentCheckBox = null;
    Vector3 tempPos;
    float timeCounter;
    float width;
    float height;
    void Start()
    {
        width = 10;
        height = 10;
        animationScript = GetComponent<g_AIAnimationScript>();
        FindClosestPlayer();
        attackTarget = closestPlayer;
        ActionFunctionsDict.Add(AIAction.ActionState.Spawn, Spawn);

        ActionFunctionsDict.Add(AIAction.ActionState.Idle, Idle);
        ActionFunctionsDict.Add(AIAction.ActionState.Runforward, RunForward);
        ActionFunctionsDict.Add(AIAction.ActionState.Melee, Melee);
        ActionFunctionsDict.Add(AIAction.ActionState.TakeDamage, TakeDamage);
        ActionFunctionsDict.Add(AIAction.ActionState.Animation, Animation);
        ActionFunctionsDict.Add(AIAction.ActionState.WalkForward, WalkForward);
        ActionFunctionsDict.Add(AIAction.ActionState.Roar, Roar);
        ActionFunctionsDict.Add(AIAction.ActionState.Taunt, Taunt);
        ActionFunctionsDict.Add(AIAction.ActionState.WalkLeft, WalkLeft);
        ActionFunctionsDict.Add(AIAction.ActionState.WalkRight, WalkRight);
        ActionFunctionsDict.Add(AIAction.ActionState.MoveLeft, MoveLeft);
        ActionFunctionsDict.Add(AIAction.ActionState.MoveRight, MoveRight);
        ActionFunctionsDict.Add(AIAction.ActionState.FireArrow, FireArrow);
        ActionFunctionsDict.Add(AIAction.ActionState.Patrol, Patrol);

        aIBehavior.Initialize();
        currentState = aIBehavior.StatesDict[aIBehavior.firstState];
        SetupCurrentActionFunc();
        currentState.SetActionTime();
    }

    public void FindClosestPlayer()
    {
        //float min = Mathf.Infinity;
        //GameObject currentClosest = null;
        //for (int i = 0; i < GameManager.Player; i++)
        //{
        //    if (GameManager.Players[i] != null)
        //    {
        //        float distance = Vector3.Distance(GameManager.Players[i].transform.position, transform.position);
        //        if (distance < min)
        //        {
        //            min = distance;
        //            currentClosest = GameManager.Players[i];
        //        }
        //    }
        //}
        //closestPlayer = currentClosest;
        closestPlayer = GameManager.Player;

    }
    public void SetAlive(bool a)
    {
        alive = a;
        if (a == false)
        {
            agent.isStopped = true;
        }
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

    void SwitchToNextState(string condition)
    {
        currentStateTime = 0;
        //Debug.Log(currentState.Name);
        //Debug.Log(condition);
        //Debug.Log(currentState.GetNextState(condition));
        currentState = aIBehavior.StatesDict[currentState.GetNextState(condition)];
        currentState.SetActionTime();
        currentStateFunc = null;
        SetupCurrentActionFunc();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            hitThisFrame = true;
        }
        if (alive)
        {
            if (!currentState.MovementThisState)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
            SetRemainingDistance();

            currentStateTime += Time.deltaTime;
            currentStateFunc?.Invoke();
        }
    }

    void EvaluateSwitchConditions()
    {
        for (int i = 0; i < currentState.Switches.Count; i++)
        {
            if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.DoOnce)
            {
                SwitchToNextState(currentState.Switches[i].Name);
                return;
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.AnimationLength)
            {
                if (currentStateTime >= currentState.Switches[i].AnimationLength)
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.DistanceToAttackTarget)
            {
                if (Vector3.Distance(transform.position, attackTarget.transform.position) < currentState.Switches[i].MinDistanceToTarget
                    || Vector3.Distance(transform.position, attackTarget.transform.position) > currentState.Switches[i].MaxDistanceToTarget)
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.DistanceToMoveTarget)
            {
                if (!agent.pathPending)
                {
                    if (remainingDistance <= currentState.Switches[i].MinDistanceToTarget
                        || remainingDistance >= currentState.Switches[i].MaxDistanceToTarget)
                    {
                        moveTarget = Vector3.zero;
                        SwitchToNextState(currentState.Switches[i].Name);
                        return;
                    }
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.DistanceToPlayer)
            {
                if (!agent.pathPending)
                {
                    if (distanceToPlayer < currentState.Switches[i].MinDistanceToTarget
                        || distanceToPlayer > currentState.Switches[i].MaxDistanceToTarget)
                    {
                        SwitchToNextState(currentState.Switches[i].Name);
                        return;
                    }
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.MinionsKilled)
            {
                SwitchToNextState(currentState.Switches[i].Name);
                return;
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.Time)
            {
                if (currentStateTime >= currentState.Switches[i].ActionTime)
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.GetHit)
            {
                if (hitThisFrame)
                {
                    hitThisFrame = false;
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if(currentState.Switches[i].SwitchCondition == Switch.SwitchState.Freeze)
            {
                if (moveTarget == transform.position)
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.UnFreeze)
            {
                if (moveTarget != transform.position)
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.GetLineOfSight)
            {
                if (CheckLineOfSight())
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
            else if (currentState.Switches[i].SwitchCondition == Switch.SwitchState.LoseLineOfSight)
            {
                if (!CheckLineOfSight())
                {
                    SwitchToNextState(currentState.Switches[i].Name);
                    return;
                }
            }
        }
    }

    void Spawn()
    {
        EvaluateSwitchConditions();
    }

    void Idle()
    {
        animationScript.PlayIdleAnimation();
        EvaluateSwitchConditions();
    }

    void Patrol()
    {
        animationScript.PlayWalkAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.Patrol].MoveSpeed;
        if (moveTarget == Vector3.zero)
        {
            SetMoveTarget(MovementTarget.Patrol);
            Vector3 direction = (moveTarget - transform.position).normalized;

            animationScript.SetWalkDirection(direction.x, direction.z);

        }
        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void RunForward()
    {
        animationScript.PlayRunAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.Runforward].MoveSpeed;
        FindClosestPlayer();
        SetMoveTarget(MovementTarget.Player);
        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void Taunt()
    {
        animationScript.PlayTaunt();
        agent.speed = 0;
        EvaluateSwitchConditions();
    }

    void Roar()
    {
        animationScript.PlayRoar();
        agent.speed = 0;
        EvaluateSwitchConditions();
    }

    void Animation()
    {
        transform.SendMessage(currentState.ActionsDict[AIAction.ActionState.Animation].AnimationFunction);
        EvaluateSwitchConditions();
    }

    void WalkForward()
    {
        //Debug.Log("Walk forward");
        animationScript.PlayWalkAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.WalkForward].MoveSpeed;
        SetMoveTarget(MovementTarget.Player);
        Vector3 direction = (moveTarget - transform.position).normalized;
        direction = transform.TransformDirection(direction);
        animationScript.SetWalkDirection(direction.x, direction.z);

        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void WalkLeft()
    {
        //Debug.Log("2");

        timeCounter += Time.deltaTime * 1 / currentState.ActionsDict[AIAction.ActionState.WalkLeft].MoveSpeed;

        animationScript.PlayWalkAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.WalkLeft].MoveSpeed;
        SetMoveTarget(true);
        Vector3 direction = (moveTarget - transform.position).normalized;
        direction = transform.TransformDirection(direction);

        animationScript.SetWalkDirection(direction.x, direction.z);

        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void WalkRight()
    {
        //Debug.Log("3");

        timeCounter -= Time.deltaTime * 1 / currentState.ActionsDict[AIAction.ActionState.WalkRight].MoveSpeed;

        animationScript.PlayWalkAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.WalkRight].MoveSpeed;
        SetMoveTarget(false);
        Vector3 direction = (moveTarget - transform.position).normalized;
        direction = transform.TransformDirection(direction);

        animationScript.SetWalkDirection(direction.x, direction.z);
        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void MoveLeft()
    {
        //animationScript.PlayWalkLeftAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.MoveLeft].MoveSpeed;
        SetMoveTarget(true);
        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void MoveRight()
    {
        //animationScript.PlayWalkRightAnimation();
        agent.speed = currentState.ActionsDict[AIAction.ActionState.MoveRight].MoveSpeed;
        SetMoveTarget(false);
        agent.SetDestination(moveTarget);
        EvaluateSwitchConditions();
    }

    void TakeDamage()
    {
        agent.speed = 0;
        animationScript.PlayHitAnimation();
        EvaluateSwitchConditions();
    }

    void Melee()
    {
        //Debug.LogError("Melee");
        agent.speed = 0;
        GetComponent<g_AIAnimationScript>().PlayAttackAnimation();
        agent.SetDestination(transform.position);
        EvaluateSwitchConditions();
    }

    private void FireArrow()
    {
        EvaluateSwitchConditions();
    }

    void SetRemainingDistance()
    {
        remainingDistance = Vector3.Distance(transform.position, moveTarget);
        FindClosestPlayer();
        distanceToPlayer = Vector3.Distance(transform.position, closestPlayer.transform.position);

    }

    private void SetMoveTarget(MovementTarget target)
    {
        if (target == MovementTarget.Player)
        {
            moveTarget = closestPlayer.transform.position;
            // Debug.Log("Moving to " + closestPlayer.name, closestPlayer);
        }
        else if (target == MovementTarget.Patrol)
        {
            
            //Instantiate a box at the movetarget and check if its inside our box
            if (!patrolCheckFrame)
            {
                tempPos = RandomNavSphere(transform.position, 20);

                currentCheckBox = (GameObject)Instantiate(boxCheck, tempPos, Quaternion.identity);
                currentCheckBox.name = "PatrolCheck";
                patrolCheckFrame = true;
            }
            else
            {
                if (patrolArea.GetIsInBox())
                {
                    moveTarget = tempPos;
                }
                else
                {
                    moveTarget = transform.position;
                }
                patrolArea.SetIsInBox(false);
                if (currentCheckBox)
                Destroy(currentCheckBox);
                patrolCheckFrame = false;
            }
        }
       GetComponent<g_AIRotateToTarget>().SetTargetTransform(moveTarget);
    }

    bool CheckLineOfSight()
    {
        //raycast at player, if hit player check the angle between AIhead and to player. If not return false
        Vector3 directionToPlayer = (GameManager.Player.transform.position - AIHead.transform.position).normalized;
        Ray ray = new Ray(AIHead.position, directionToPlayer);
        RaycastHit hit;
        if (Vector3.Angle(directionToPlayer, AIHead.forward) < lineOfSightAngle)
        {
            if (Physics.Raycast(ray, out hit, 50, lineOfSightMask.value, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
         Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
         randDirection += origin;
         NavMeshHit navHit;
         NavMesh.SamplePosition(randDirection, out navHit, dist, 1);
         NavMeshPath path = new NavMeshPath();
         agent.CalculatePath(navHit.position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            return navHit.position;
        }
        else
        {
            return transform.position;
        }
    }

    //private void SetMoveTarget(bool left)
    //{
    //    float x = Mathf.Cos(timeCounter) * height;
    //    float y = 0;
    //    float z = Mathf.Sin(timeCounter) * width;
    //
    //
    //    if (left)
    //    {
    //        Vector3 tempPos = GameManager.Player.transform.position + new Vector3(x, y, z) * currentState.ActionsDict[AIAction.ActionState.WalkLeft].MoveMagnitude;
    //
    //        moveTarget = tempPos;
    //    }
    //    else
    //    {
    //        Vector3 tempPos = GameManager.Player.transform.position + new Vector3(x, y, z) * currentState.ActionsDict[AIAction.ActionState.WalkRight].MoveMagnitude;
    //
    //        moveTarget = tempPos;
    //    }
    //
    //}

    private void SetMoveTarget(bool left)
    {
        Vector3 tempPos = transform.position;


        if (left)
        {
            tempPos = tempPos - (transform.right * currentState.ActionsDict[AIAction.ActionState.WalkLeft].MoveMagnitude);
    
        }
        else
        {
            tempPos = tempPos + (transform.right * currentState.ActionsDict[AIAction.ActionState.WalkRight].MoveMagnitude);

        }
        moveTarget = tempPos;

        GetComponent<g_AIRotateToTarget>().SetTargetTransform(GameManager.Player.transform.position);

    }

    public void SetHitThisFrame(bool hit)
    {
        hitThisFrame = hit;
    }
}
