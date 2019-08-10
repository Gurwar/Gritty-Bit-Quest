using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIBehavior", menuName = "ScriptableObjects/AIBehavior", order = 1)]
public class AIBehavior : ScriptableObject
{
    public AIAction.ActionState firstAction;
    [HideInInspector]
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public List<AIAction> ActionsList = new List<AIAction>();

    public List<AIAction> GetAIActions()
    {
        return ActionsList;
    }
}
