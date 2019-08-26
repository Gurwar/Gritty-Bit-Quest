<<<<<<< HEAD
/target:library
/nowarn:0169
/out:Temp/Unity.XR.Oculus.Editor.dll
/debug:portable
/optimize-
/nostdlib+
/preferreduilang:en-US
/langversion:latest
/reference:Library/ScriptAssemblies/UnityEditor.UI.dll
/reference:Library/ScriptAssemblies/UnityEngine.UI.dll
/reference:Library/ScriptAssemblies/UnityEditor.TestRunner.dll
/reference:Library/ScriptAssemblies/UnityEngine.TestRunner.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AIModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.ARModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AccessibilityModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AndroidJNIModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AnimationModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AssetBundleModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.AudioModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.ClothModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.ClusterInputModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.ClusterRendererModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.CoreModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.CrashReportingModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.DSPGraphModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.DirectorModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.FileSystemHttpModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.GameCenterModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.GridModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.HotReloadModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.IMGUIModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.ImageConversionModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.InputModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.InputLegacyModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine.JSONSerializeModule.dll
/reference:D:/Unity/Unity/Hub/Editor/2019.2.2f1/Editor/Data/Managed/UnityEngine/UnityEngine
=======
﻿using System.Collections;
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
>>>>>>> parent of d0d11103... Gritty Bit Vehicle
