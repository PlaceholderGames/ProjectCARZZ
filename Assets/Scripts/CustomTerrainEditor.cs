using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Links code within here to the the customterrain class
[CustomEditor(typeof(CustomTerrain))]
//[CanEditMultipleObjects]

public class NewBehaviourScript : Editor {
    //properties ------------------------------
    SerializedProperty randomHeightRange;
    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;

    //fold outs -------------------------------
    bool showRandom = false;
    bool showLoadHeights = false;
    bool showResetTerrain = false;

    void OnEnable()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");//one in customTerrain.cs
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
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
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
