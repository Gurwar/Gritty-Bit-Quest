using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(gameState))]
public class GameStateEditor : Editor
{
    SerializedProperty m_gameTime;
    SerializedProperty m_LevelType;
    SerializedProperty m_PlayerState;
    SerializedProperty m_Player;
    SerializedProperty m_EnemyManager;
    SerializedProperty m_UpgradeScreen;
    SerializedProperty m_GameOverScreen;
    SerializedProperty m_KeyBoard;
    SerializedProperty m_Name;
    SerializedProperty m_MainMenuObjects;
    SerializedProperty m_DisableOnDeath;
    SerializedProperty m_MoveTutorialObjects;
    SerializedProperty m_SpinTutorialObjects;
    SerializedProperty m_GunTutorialObjects;
    private void OnEnable()
    {
        m_gameTime = serializedObject.FindProperty("gameTime");
        m_LevelType = serializedObject.FindProperty("levelType");
        m_PlayerState = serializedObject.FindProperty("playerState");
        m_Player = serializedObject.FindProperty("player");
        m_EnemyManager = serializedObject.FindProperty("EnemyManager");
        m_UpgradeScreen = serializedObject.FindProperty("UpgradeScreen");
        m_GameOverScreen = serializedObject.FindProperty("GameOverScreen");
        m_KeyBoard = serializedObject.FindProperty("keyBoard");
        m_Name = serializedObject.FindProperty("name");
        m_MainMenuObjects = serializedObject.FindProperty("MainMenuObjects"); // only in main menu
        m_DisableOnDeath = serializedObject.FindProperty("DisableOnDeath");
        m_MoveTutorialObjects = serializedObject.FindProperty("MoveTutorialObjects");
        m_SpinTutorialObjects = serializedObject.FindProperty("SpinTutorialObjects");
        m_GunTutorialObjects = serializedObject.FindProperty("GunTutorialObjects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        gameState myTarget = (gameState)target;
        myTarget.levelType = (gameState.LevelTypes)EditorGUILayout.EnumPopup("Level Types:", myTarget.levelType);
        myTarget.playerState = (gameState.GameStates)EditorGUILayout.EnumPopup("Player States:", myTarget.playerState);

        if (myTarget.levelType == gameState.LevelTypes.Arcade)
        {
            GUILayout.Label("Arcade");
            myTarget.gameTime = EditorGUILayout.FloatField("Game Time:", myTarget.gameTime);
            EditorGUILayout.PropertyField(m_Player, new GUIContent("Player"));
            EditorGUILayout.PropertyField(m_EnemyManager, new GUIContent("Enemy Manager"));
            EditorGUILayout.PropertyField(m_UpgradeScreen, new GUIContent("Upgrade Screen"));
            EditorGUILayout.PropertyField(m_GameOverScreen, new GUIContent("Game Over Screen"));
            EditorGUILayout.PropertyField(m_KeyBoard, new GUIContent("Keyboard"));
            EditorGUILayout.PropertyField(m_Name, new GUIContent("Name"));
            EditorGUILayout.PropertyField(m_DisableOnDeath, new GUIContent("Disable On Death"), true);
        }
        else if (myTarget.levelType == gameState.LevelTypes.MainMenu)
        {
            GUILayout.Label("Main Menu");
            EditorGUILayout.PropertyField(m_MainMenuObjects, new GUIContent("Main Menu Objects"), true);
            myTarget.playerState = gameState.GameStates.MainMenu;

        }
        else if (myTarget.levelType == gameState.LevelTypes.Tutorial)
        {
            GUILayout.Label("Tutorial");
            EditorGUILayout.PropertyField(m_Player, new GUIContent("Player"));
            EditorGUILayout.PropertyField(m_MoveTutorialObjects, new GUIContent("Move Tutorial Objects"), true);
            EditorGUILayout.PropertyField(m_SpinTutorialObjects, new GUIContent("Spin Tutorial Objects"), true);
            EditorGUILayout.PropertyField(m_GunTutorialObjects, new GUIContent("Gun Tutorial Objects"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
