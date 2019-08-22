using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crystal Lord Boss State", menuName = "ScriptableObjects/Crystal Lord Boss States", order = 1)]
public class BossPhase : ScriptableObject
{
    public string firstState;
    [HideInInspector]
    public Dictionary<string, AIState> StatesDict = new Dictionary<string, AIState>();
    public List<AIState> StatesList = new List<AIState>();

    public List<AIState> GetAIStates()
    {
        return StatesList;
    }

}
