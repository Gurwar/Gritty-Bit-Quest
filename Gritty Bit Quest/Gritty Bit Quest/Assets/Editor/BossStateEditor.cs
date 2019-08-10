﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

using UnityEngine.UIElements;
using System.Reflection;

[CustomEditor(typeof(BossState))]

public class BossStateEditor : Editor
{
    SerializedProperty m_Name;
    BossState bossState;
    List<string> SkipPropertyList = new List<string>();


    void OnEnable()
    {
        if (target == null)
        {
            return;
        }

        SkipPropertyList.Add("x");
        SkipPropertyList.Add("y");
        SkipPropertyList.Add("data");

        m_Name = serializedObject.FindProperty("name");
        bossState = (BossState)target;
    }

    void DrawList(SerializedProperty element)
    {

        GUILayout.BeginVertical("Box");

        IEnumerator childEnum = element.GetEnumerator();
        while (childEnum.MoveNext())
        {
            SerializedProperty current = childEnum.Current as SerializedProperty;
            if (current.name.ToLower() == "switchcondition")
                HandleSkipPropertiesList(current);
            if (current.name == "PossibleNextActions")
                SkipPropertyList.Remove("data");

            bool skip = false;
            for (int i = 0;i < SkipPropertyList.Count; i++)
            {
                if (SkipPropertyList[i] == current.name)
                {
                    skip = true;
                    break;
                }
            }
            if (!skip)
            HandleProperties(current);

        }
        GUILayout.EndVertical();
    }

    void HandleProperties(SerializedProperty property)
    {
        if (property.isArray)
        {
            EditorGUILayout.TextField(property.name, EditorStyles.boldLabel);
        }
        //use reflection to get the variable
        if (property.propertyType == SerializedPropertyType.ArraySize)
        {
            property.intValue = EditorGUILayout.IntField("Size: ", property.intValue);
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUILayout.IntField(property.name, property.intValue);
        }
        else if (property.propertyType == SerializedPropertyType.Vector2)
        {
            property.vector2Value = EditorGUILayout.Vector2Field(property.name, property.vector2Value);
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = EditorGUILayout.FloatField(property.name, property.floatValue);
        }

        else if (property.propertyType == SerializedPropertyType.Enum)
        {
            EditorGUILayout.PropertyField(property, false);
        }

    }

    void HandleSkipPropertiesList(SerializedProperty property) //Trigger
    {
        if (property.enumValueIndex == 0)
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
        }
        else if (property.enumValueIndex == 1) //Time
        {
           SkipPropertyList.Remove("ActionTimeRange");
           GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
           GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
           GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
           GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
           GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
        }
        else if (property.enumValueIndex == 2) //MinionsKilled
        {
            SkipPropertyList.Remove("EnemyTypeToSpawn");
            SkipPropertyList.Remove("EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
        }   
        else if (property.enumValueIndex == 3)//Distance to target
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            SkipPropertyList.Remove("ActionTimeRange");
            SkipPropertyList.Remove("EnemyTypeToSpawn");
            SkipPropertyList.Remove("EnemyAmount");
            SkipPropertyList.Remove("AnimationLength");
        }
        else if (property.enumValueIndex == 4)// Animation Length
        {
            SkipPropertyList.Remove("AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");

        }
    }

    public override void OnInspectorGUI()
    {
        //base.DrawDefaultInspector();
        bossState.state = (BossState.State)EditorGUILayout.EnumPopup("State", bossState.state);
        bossState.firstAction = (AIAction.ActionState)EditorGUILayout.EnumPopup("First Action", bossState.firstAction); 
        for (int i = 0; i < bossState.ActionsList.Count; i++)
        {
            DrawList(serializedObject.FindProperty("ActionsList").GetArrayElementAtIndex(i));
        }

        if (GUILayout.Button("Add AI Action"))
        {
            AIAction tempAction = new AIAction();
            tempAction.Switches.Add(new Switch());
            bossState.ActionsList.Add(tempAction);

        }

        if (GUILayout.Button("Remove AI Action"))
        {
            bossState.ActionsList.Remove(bossState.ActionsList[bossState.ActionsList.Count - 1]);
        }
        for (int i = 0; i < bossState.ActionsList.Count; i++)
            bossState.ActionsList[i].RefreshSwitchDictionary();
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();



        Repaint();
    }
}