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
    SerializedProperty perlinXScale;
    SerializedProperty perlinYScale;

    //fold outs -------------------------------
    bool showRandom = false;

    void OnEnable()
    {
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");//one in customTerrain.cs
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        perlinXScale = serializedObject.FindProperty("perlinXScale");
        perlinYScale = serializedObject.FindProperty("perlinYScale");
    }

    public override void OnInspectorGUI()//The display in the editor
    {
        serializedObject.Update();//updates all serialized valued in this script and the customTerrain script

        CustomTerrain terrain = (CustomTerrain)target;//'terrain' is a link to the script on the Terrain

        showRandom = EditorGUILayout.Foldout(showRandom, "Procedural Generation Config");//hide/unhide in editor
        if (showRandom)
        {
            GUILayout.Label("Load Heights From Texture:", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load Texture"))
            {
                terrain.LoadTexture();
            }


            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values:", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if (GUILayout.Button("Random Heights"))
            {
                terrain.RandomTerrain();
            }



            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Perlin Noise", EditorStyles.boldLabel);
            EditorGUILayout.Slider(perlinXScale, 0, 1, new GUIContent("X Scale"));
            EditorGUILayout.Slider(perlinYScale, 0, 1, new GUIContent("Y Scale"));
            if (GUILayout.Button("Apply"))
            {
                terrain.Perlin();
            }


            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            if (GUILayout.Button("Reset Terrain"))
            {
                terrain.ResetTerrain();
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
