using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Switch
{
    public enum SwitchCondition { Trigger,Time, MinionsKilled, DistanceToTarget, AnimationLength }
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
    public List<AIAction.ActionState> PossibleNextActions;


}


[System.Serializable]
public class AIAction
{
    public enum ActionState { spawn, idle, runforward, walkForward, walkLeft, walkRight, attack, roar, bossRest, bossResetCubes, bossAbsorb, bossCubeAttack, bossLightningAttack, bossSpawnEnemies };
    public ActionState Action;
    public List<Switch> Switches = new List<Switch>();
    public Dictionary<Switch.SwitchCondition, Switch> SwitchesDict = new Dictionary<Switch.SwitchCondition, Switch>();

    public void setActionTime()
    {
        for (int i = 0; i < Switches.Count; i++)
            Switches[i].ActionTime = Random.Range(Switches[i].ActionTimeRange.x, Switches[i].ActionTimeRange.y);
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

    public ActionState getNextAction(Switch.SwitchCondition condition)
    {
        Switch temp = SwitchesDict[condition];
        int Rand = Random.Range(0, temp.PossibleNextActions.Count);
        return temp.PossibleNextActions[Rand];
    }
}