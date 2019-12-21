using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Switch
{
    public enum SwitchState { DoOnce,Time, MinionsKilled, DistanceToAttackTarget, DistanceToMoveTarget, DistanceToPlayer, AnimationLength, OnTriggerEnter, GetHit, GetLineOfSight, LoseLineOfSight, Freeze, UnFreeze}
    public SwitchState SwitchCondition;
    public string Name;
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
    public int Index;
    public string Name;
    public float Chance;
    public bool MovementThisState;
    public List<AIAction> Actions = new List<AIAction>();
    public List<Switch> Switches = new List<Switch>();
    public Dictionary<AIAction.ActionState, AIAction> ActionsDict = new Dictionary<AIAction.ActionState, AIAction>();
    public Dictionary<string, Switch> SwitchesDict = new Dictionary<string, Switch>();
    [HideInInspector]
    public AIBehavior behaviour;

    public void Initialize(AIBehavior aIBehavior)
    {
        behaviour = aIBehavior;
        RefreshSwitchDictionary();
        RefreshActionDictionary();
        //RefreshStateDictionary();

    }



    public void SetActionTime()
    {
        for (int i = 0; i < Switches.Count; i++)
            Switches[i].ActionTime = UnityEngine.Random.Range(Switches[i].ActionTimeRange.x, Switches[i].ActionTimeRange.y);
    }

    public void RefreshStateDictionary()
    {
        behaviour.StatesDict.Clear();
        for (int i = 0; i < behaviour.StatesList.Count; i++)
        {
            if (!behaviour.StatesDict.ContainsKey(behaviour.StatesList[i].Name))
                behaviour.StatesDict.Add(behaviour.StatesList[i].Name, behaviour.StatesList[i]);
        }
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
            if (Switches[i].Name != null)
            {
                if (!SwitchesDict.ContainsKey(Switches[i].Name))
                    SwitchesDict.Add(Switches[i].Name, Switches[i]);
            }
        }
    }

    public string GetNextState(string condition)
    {
        if (condition != "")
        {
            Switch temp = SwitchesDict[condition];
            float totalChance = 0;
            Dictionary<Vector2, string> ChangeandStates = new Dictionary<Vector2, string>();
            for (int i = 0; i < temp.PossibleNextStates.Count; i++)
            {
                float chance = behaviour.StatesDict[temp.PossibleNextStates[i]].Chance;
                ChangeandStates.Add(new Vector2(totalChance, totalChance + chance), temp.PossibleNextStates[i]);
                totalChance += chance;
            }
            float Rand = UnityEngine.Random.Range(0, totalChance);

            foreach (KeyValuePair <Vector2, string> entry in ChangeandStates)
            {
                if (Rand >= entry.Key.x && Rand <= entry.Key.y)
                {
                    return entry.Value;
                }
            }


        }
        return null;
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
    public enum ActionState { Spawn, Idle, Runforward, WalkForward, WalkLeft, WalkRight, Attack, Roar, ResetCubes, Absorb, CubeAttack, LightningAttack, SpawnEnemies, Animation, MoveToTransform, MoveToVector, RotateTo, RotateBy, NextState, TakeDamage, Melee, MoveLeft, MoveRight, FireArrow, Patrol, Taunt};
    public ActionState Action;
    public AIActionSettings ActionSettings;
    public string AnimationFunction;
    public float MoveSpeed;
    public Vector3 VectorToMoveTo;
    public string MoveTransformName;
    public float MoveMagnitude;
    public float RotateSpeed;
    public Vector3 TargetRotation;
}
