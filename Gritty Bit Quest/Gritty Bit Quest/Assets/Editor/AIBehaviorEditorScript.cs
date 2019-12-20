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
    List<string> BoldLabelPropertyList = new List<string>();
    List<string> BoldMiniLabelPropertyList = new List<string>();

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
        SkipPropertyList.Add("z");
        SkipPropertyList.Add("StartTime");
        SkipPropertyList.Add("EndTime");
        SkipPropertyList.Add("data");

        BoldLabelPropertyList.Add("Switches");
        BoldMiniLabelPropertyList.Add("SwitchCondition");
        BoldMiniLabelPropertyList.Add("PossibleNextStates");

        m_Name = serializedObject.FindProperty("name");
        AIBehavior = (AIBehavior)target;


        for (int i = 0; i < AIBehavior.StatesList.Count; i++)
        {
            for (int j = 0; j < AIBehavior.StatesList[i].Actions.Count; j++)
            {
                AIBehavior.StatesList[i].Actions[j].ActionSettings.StartTime = 0;
                AIBehavior.StatesList[i].Actions[j].ActionSettings.EndTime = Mathf.Infinity;
            }
        }
    }

    void DrawList(SerializedProperty element)
    {

        GUILayout.BeginVertical("Box");

        IEnumerator childEnum = element.GetEnumerator();
        while (childEnum.MoveNext())
        {
            SerializedProperty current = childEnum.Current as SerializedProperty;
            if (current.name.ToLower() == "action")
                HandleSkipPropertiesListActions(current.enumValueIndex);
            if (current.name.ToLower() == "switchcondition")
                HandleSkipPropertiesListSwitches(current.enumValueIndex);
            if (current.name == "PossibleNextStates")
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
        //if (property.isArray && property.propertyType != SerializedPropertyType.String)
        //{
        //    EditorGUILayout.TextField(property.name, EditorStyles.boldLabel);
        //}
        if (BoldLabelPropertyList.Contains(property.name))
        {
            EditorGUILayout.TextField(property.name, EditorStyles.boldLabel);
        }
        else if (BoldMiniLabelPropertyList.Contains(property.name))
        {
            EditorGUILayout.TextField(property.name, EditorStyles.miniBoldLabel);
        }
        
        //use reflection to get the variable
        if (property.propertyType == SerializedPropertyType.ArraySize)
        {
            property.intValue = EditorGUILayout.IntField("Size: ", property.intValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.String)
        {
            property.stringValue = EditorGUILayout.TextField(property.name, property.stringValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUILayout.IntField(property.name, property.intValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Boolean)
        {
            property.boolValue = EditorGUILayout.Toggle(property.name, property.boolValue);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Vector3)
        {
            property.vector3Value = EditorGUILayout.Vector3Field(property.name, property.vector3Value);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Vector2)
        {
            property.vector2Value = EditorGUILayout.Vector2Field(property.name, property.vector2Value);
            return;
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = EditorGUILayout.FloatField(property.name, property.floatValue);
            return;
        }
        else if (property.name == "ActionSettings")
        {
            EditorGUILayout.PropertyField(property, true);
        }
        else if (property.propertyType != SerializedPropertyType.Generic)
        {
            EditorGUILayout.PropertyField(property, true);
            return;
        }
    }

    void HandleSkipPropertiesListActions(int enumValue) //Do Once
    {
        if (enumValue == 0) // Spawn
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");

        }
        else if (enumValue == 1) //Idle
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 2) //RunForward
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 3)//Walk Forward
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 4)//WalkLeft
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            SkipPropertyList.Remove("MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 5)//WalkRight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            SkipPropertyList.Remove("MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 6)// Roar
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 7)// ResetCubes
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 8)// Absorb
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 9)// Attack
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 10)// Cube Attack
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 11)// Lightning Attack
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 12)// Spawn Enemies
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 13)// Animation
        {
            SkipPropertyList.Remove("AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 14)// MoveToTransform
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            SkipPropertyList.Remove("MoveSpeed");
            SkipPropertyList.Remove("MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");

        }
        else if (enumValue == 15)// MoveToVector
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            SkipPropertyList.Remove("VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");

        }
        else if (enumValue == 16)// RotateTo
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            SkipPropertyList.Remove("RotateSpeed");
            SkipPropertyList.Remove("TargetRotation");
        }
        else if (enumValue == 17)// RotateBy
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            SkipPropertyList.Remove("RotateSpeed");
            SkipPropertyList.Remove("TargetRotation");
        }
        else if (enumValue == 18)// Next State
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 19)// Take Damage
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 20)// Melee
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 21)// MoveLeft
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            SkipPropertyList.Remove("MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 22)// MoveRight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            SkipPropertyList.Remove("MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 23)// MoveRight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 24)// MoveRight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 25)// FireArrow
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 26)// Patrol
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            SkipPropertyList.Remove("MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
        else if (enumValue == 27)// Taunt
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationFunction");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "VectorToMoveTo");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveTransformName");
            GameManager.AddToListOnce(ref SkipPropertyList, "MoveMagnitude");
            GameManager.AddToListOnce(ref SkipPropertyList, "RotateSpeed");
            GameManager.AddToListOnce(ref SkipPropertyList, "TargetRotation");
        }
    }


    void HandleSkipPropertiesListSwitches(int enumValue) //Do Once
    {
        if (enumValue == 0)
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
        }
        else if (enumValue == 1) //Time
        {
            SkipPropertyList.Remove("ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
        }
        else if (enumValue == 2) //MinionsKilled
        {
            SkipPropertyList.Remove("EnemyTypeToSpawn");
            SkipPropertyList.Remove("EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
        }
        else if (enumValue == 3)//Distance to attack target
        {
            SkipPropertyList.Remove("MinDistanceToTarget");
            SkipPropertyList.Remove("MaxDistanceToTarget");
            SkipPropertyList.Remove("MaxDistInfinity");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
        }
        else if (enumValue == 4)//Distance to move target
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            SkipPropertyList.Remove("MinDistanceToTarget");
            SkipPropertyList.Remove("MaxDistanceToTarget");
            SkipPropertyList.Remove("MaxDistInfinity");
        }
        else if (enumValue == 5)//Distance to move target
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            SkipPropertyList.Remove("MinDistanceToTarget");
            SkipPropertyList.Remove("MaxDistanceToTarget");
            SkipPropertyList.Remove("MaxDistInfinity");
        }
        else if (enumValue == 6)// Animation Length
        {
            SkipPropertyList.Remove("AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
        }
        else if (enumValue == 7)// Animation Length
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
        }
        else if (enumValue == 8)// Get hit
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
        }
        else if (enumValue == 9)// Get Line Of Sight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
        }
        else if (enumValue == 10)// Lose Line Of Sight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
        }
        else if (enumValue == 11)// Lose Line Of Sight
        {
            GameManager.AddToListOnce(ref SkipPropertyList, "AnimationLength");
            GameManager.AddToListOnce(ref SkipPropertyList, "ActionTimeRange");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyTypeToSpawn");
            GameManager.AddToListOnce(ref SkipPropertyList, "EnemyAmount");
            GameManager.AddToListOnce(ref SkipPropertyList, "MinDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistanceToTarget");
            GameManager.AddToListOnce(ref SkipPropertyList, "MaxDistInfinity");
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
        AIBehavior.name = EditorGUILayout.TextField("State", AIBehavior.name);
        AIBehavior.firstState = EditorGUILayout.TextField("First State", AIBehavior.firstState);
        for (int i = 0; i < AIBehavior.StatesList.Count; i++)
        {
            DrawList(serializedObject.FindProperty("StatesList").GetArrayElementAtIndex(i));
        }

        if (GUILayout.Button("Add AI State"))
        {
            AIState tempState = new AIState();
            tempState.Switches.Add(new Switch());
            tempState.Name = "NULL";
            for (int i = 0; i < tempState.Actions.Count; i++)
            {
                if (i == tempState.Actions.Count - 1)
                {
                    tempState.Actions[i].ActionSettings.StartTime = 0;
                    tempState.Actions[i].ActionSettings.EndTime = Mathf.Infinity;
                }
            }
            AIBehavior.StatesList.Add(tempState);
        }
        if (GUILayout.Button("Remove AI State"))
        {
            AIBehavior.StatesList.Remove(AIBehavior.StatesList[AIBehavior.StatesList.Count - 1]);
        }
        for (int i = 0; i < AIBehavior.StatesList.Count; i++)
        {
            AIBehavior.Initialize();
        }

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();



        Repaint();
    }
}