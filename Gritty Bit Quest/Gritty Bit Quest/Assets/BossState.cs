using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crystal Lord Boss State", menuName = "ScriptableObjects/Crystal Lord Boss States", order = 1)]
public class BossState : ScriptableObject
{
    public enum State { CrystalThrow, LightningAttack, Absorb }
    public State state;
    public AIAction.ActionState firstAction;
    [HideInInspector]
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public List<AIAction> ActionsList = new List<AIAction>();

    public List<AIAction> GetAIActions()
    {
        return ActionsList;
    }

}
