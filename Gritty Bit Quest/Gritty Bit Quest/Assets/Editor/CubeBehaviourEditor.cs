using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeBehaviour))]
public class CubeBehaviourEditor : Editor
{
    SerializedProperty m_damage;
    SerializedProperty m_distanceMultiplier;
    SerializedProperty m_distanceToPlayer;

    private void OnEnable()
    {
        m_damage = serializedObject.FindProperty("distance");
        m_distanceMultiplier = serializedObject.FindProperty("distanceMultiplier");
        m_distanceToPlayer = serializedObject.FindProperty("distanceToPlayer");
    }

    public override void OnInspectorGUI()
    {
        CubeBehaviour myTarget = (CubeBehaviour)target;
        base.OnInspectorGUI();
        myTarget.maxDamage = EditorGUILayout.FloatField("Max Damage", myTarget.maxDamage);
        myTarget.maxDistanceToDamage = EditorGUILayout.FloatField("Max Distance To Damage", myTarget.maxDistanceToDamage);
        float maxRange = myTarget.maxDamage / myTarget.maxDistanceToDamage;
        myTarget.distanceMultiplier = EditorGUILayout.Slider("Distance Multiplier", myTarget.distanceMultiplier, 0, maxRange);
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}
