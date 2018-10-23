﻿using UnityEngine;
using UnityEditor;
//Links code within here to the the customterrain class
[CustomEditor(typeof(CustomTerrain))]
//[CanEditMultipleObjects]

public class NewBehaviourScript : Editor {
    //properties ------------------------------
    SerializedProperty randomHeightRange;
    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;

    SerializedProperty smoothAmount;
    
    SerializedProperty splatHeights;

    //fold outs -------------------------------
    bool showRandom = false;
    bool showLoadHeights = false;
    bool showResetTerrain = false;
    bool showSmooth = false;
    bool showSplatMap = false;

    void OnEnable()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");//one in customTerrain.cs
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        smoothAmount = serializedObject.FindProperty("smoothAmount");
    }

    public override void OnInspectorGUI()//The display in the editor
    {
        serializedObject.Update();//updates all serialized valued in this script and the customTerrain script

        CustomTerrain terrain = (CustomTerrain)target;//'terrain' is a link to the script on the Terrain

        showRandom = EditorGUILayout.Foldout(showRandom, "Procedural Generation Config");//hide/unhide in editor
        if (showRandom)
        {
            GUILayout.Label("Set Heights Between Random Values:", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }
        }

        showLoadHeights = EditorGUILayout.Foldout(showLoadHeights, "Heightmap Config");
        if (showLoadHeights)
        {
            GUILayout.Label("Load Heights From Texture:", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load Texture"))
            {
                terrain.LoadTexture();
            }
        }

        showResetTerrain = EditorGUILayout.Foldout(showResetTerrain, "Reset Terrain");
        if (showResetTerrain)
        {
            GUILayout.Label("Reset Terrain to Flat surface", EditorStyles.label);
            if (GUILayout.Button("Reset Terrain"))
            {
                terrain.ResetTerrain();
            }
            serializedObject.ApplyModifiedProperties();//applies any changes
        }

        showSmooth = EditorGUILayout.Foldout(showSmooth, "Smooth Terrain");
        if (showSmooth)
        {
            EditorGUILayout.IntSlider(smoothAmount, 1, 10, new GUIContent("smoothAmount"));
            if (GUILayout.Button("Smooth"))
            {
                terrain.Smooth();
            }

        }

        showSplatMap = EditorGUILayout.Foldout(showSmooth, "Splat Maps");
        if (showSplatMap)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Splat Maps", EditorStyles.boldLabel);
            if (GUILayout.Button("Apply SplatMaps"))
            {
                terrain.Smooth();
            }

        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
