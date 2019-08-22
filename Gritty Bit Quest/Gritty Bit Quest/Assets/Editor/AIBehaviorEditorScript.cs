using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AIBehavior))]

public class AIBehaviorEditorScript : Editor
{
    SerializedProperty m_Name;
    AIBehavior AIBehavior;
    List<string> SkipPropertyList = new List<string>();
    float lineHeight;
    float lineHeightSpace;

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

        lineHeight = EditorGUIUtility.singleLineHeight;
        lineHeightSpace = lineHeight + 10;
        AIBehavior = (AIBehavior)target;
    }

    void DrawList(SerializedProperty element)
    {
        GUILayout.BeginVertical("Box");
        IEnumerator childEnum = element.GetEnumerator();
        while (childEnum.MoveNext())
        {
            SerializedProperty current = childEnum.Current as SerializedProperty;
            if (current.name.ToLower() == "switchcondition")
            {
                HandleSkipPropertiesList(current);
                EditorGUILayout.TextField("Switch Condition", EditorStyles.boldLabel);

            }
            if (current.name == "PossibleNextActions")
                SkipPropertyList.Remove("data");

            bool skip = false;
            for (int i = 0; i < SkipPropertyList.Count; i++)
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
            SkipPropertyList.Remove("MinDistanceToTarget");
            SkipPropertyList.Remove("MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
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
        //AIBehavior.firstAction = (AIAction.ActionState)EditorGUILayout.EnumPopup("First Action", AIBehavior.firstAction);
        for (int i = 0; i < AIBehavior.StatesList.Count; i++)
        {
            DrawList(serializedObject.FindProperty("ActionsList").GetArrayElementAtIndex(i));
        }

        if (GUILayout.Button("Add AI Action"))
        {
            AIState tempState = new AIState();
            tempState.Switches.Add(new Switch());
            AIBehavior.StatesList.Add(tempState);
        }

        if (GUILayout.Button("Remove AI Action"))
        {
            AIBehavior.StatesList.Remove(AIBehavior.StatesList[AIBehavior.StatesList.Count - 1]);
        }

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();



        Repaint();
    }
}