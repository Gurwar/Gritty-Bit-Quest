using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIBehavior", menuName = "ScriptableObjects/AIBehavior", order = 1)]
public class AIBehavior : ScriptableObject
{

    public string firstState;
    [HideInInspector]
    public Dictionary<string, AIState> StateDict = new Dictionary<string, AIState>();
    public List<AIState> StatesList = new List<AIState>();

    public List<AIState> GetAIActions()
    {
        return StatesList;
    }
}
