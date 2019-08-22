using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Switch
{
    public enum SwitchCondition { DoOnce,Time, MinionsKilled, DistanceToAttackTarget, DistanceToMoveTarget, DistanceToPlayer, AnimationLength, OnTriggerEnter,}
    public SwitchCondition switchCondition;
    public float MinDistanceToTarget;
    public float MaxDistanceToTarget;
    public float AnimationLength;
    public enum EnemyType { Soldier, Behemoth }
    public EnemyType EnemyTypeToSpawn;
    public int EnemyAmount;
    public Vector2 ActionTimeRange;
    [HideInInspector]
    public float ActionTime;
    public List<string> PossibleNextStates = new List<string>();
}

[System.Serializable]
public class AIState
{
    public string Name;
    public List<AIAction> Actions = new List<AIAction>();
    public List<Switch> Switches = new List<Switch>();
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public Dictionary<Switch.SwitchCondition, Switch> SwitchesDict = new Dictionary<Switch.SwitchCondition, Switch>();

    public void Initialize()
    {
        RefreshSwitchDictionary();
        RefreshActionDictionary();
    }
    public void SetActionTime()
    {
        if (SwitchesDict.ContainsKey(Switch.SwitchCondition.Time))
            SwitchesDict[Switch.SwitchCondition.Time].ActionTime = Random.Range(SwitchesDict[Switch.SwitchCondition.Time].ActionTimeRange.x, SwitchesDict[Switch.SwitchCondition.Time].ActionTimeRange.y);
    }

    public void RefreshActionDictionary()
    {
        ActionsDict.Clear();
        for (int i = 0; i < Actions.Count; i++)
        {
            if (!ActionsDict.ContainsKey(Actions[i].Action))
                ActionsDict.Add(Actions[i].Action, Actions[i]);
        }
    }

    public void RefreshSwitchDictionary()
    {
        SwitchesDict.Clear();
        for (int i = 0; i < Switches.Count; i++)
        {
            if (!SwitchesDict.ContainsKey(Switches[i].switchCondition))
                SwitchesDict.Add(Switches[i].switchCondition, Switches[i]);
        }
    }

    public string GetNextState(Switch.SwitchCondition condition)
    {
        Switch temp = SwitchesDict[condition];
        int Rand = Random.Range(0, temp.PossibleNextStates.Count);
        return temp.PossibleNextStates[Rand];
    }
}

[System.Serializable]
public class AIActionSettings
{
    public float StartTime;
    public float EndTime;
}

[System.Serializable]
public class AIAction
{
    public enum ActionState { Spawn, Idle, Runforward, WalkForward, WalkLeft, WalkRight, Attack, Roar, ResetCubes, Absorb, CubeAttack, LightningAttack, SpawnEnemies, Animation, MoveToTransform, MoveToVector, RotateTo, RotateBy, NextState };
    public ActionState Action;
    public AIActionSettings ActionSettings;
    public AnimationClip AnimationClip;
    public float MoveSpeed;
    public Vector3 VectorToMoveTo;
    public string MoveTransformName;
    public float RotateSpeed;
    public Vector3 TargetRotation;
}