using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIBehavior", menuName = "ScriptableObjects/AIBehavior", order = 1)]
public class AIBehavior : ScriptableObject
{
    public AIAction.ActionState firstAction;
    [HideInInspector]
<<<<<<< HEAD
<<<<<<< HEAD
    public Dictionary<string, AIState> StatesDict = new Dictionary<string, AIState>();
    public List<AIState> StatesList = new List<AIState>();

    public List<AIState> GetAIStates()
=======
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public List<AIAction> ActionsList = new List<AIAction>();

    public List<AIAction> GetAIActions()
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
=======
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public List<AIAction> ActionsList = new List<AIAction>();

    public List<AIAction> GetAIActions()
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
    {
        return ActionsList;
    }
}
