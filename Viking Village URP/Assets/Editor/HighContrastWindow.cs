using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
[System.Serializable]
public class Tile
{
    public string name;
    // TODO: Get list of existing Layers in dropdown to select from
    public string layer;
    public Color color;
}

public class HighContrastWindow : EditorWindow {
    private SerializedProperty m_RendererFeatures;

    string renderDataName;
    string renderFeatureName;
    int howMany;
    public List<Tile> tileList;
    [SerializeField]
    int myInt = 1;

    [MenuItem("Window/Accessibility/High Contrast Mode")]
    public static void ShowWindow() {
        GetWindow<HighContrastWindow>("High Contrast");
    }

    private void OnGUI() {
        // m_RendererFeatures = serializedObject.FindProperty(nameof(ScriptableRendererData.m_RendererFeatures));
        GUILayout.Label("Create your own HCM!");
        renderDataName = EditorGUILayout.TextField("Forward Renderer", renderDataName);
        renderFeatureName = EditorGUILayout.TextField("Renderer Feature", renderFeatureName);

        howMany = EditorGUILayout.IntField("How Many?", howMany);

        // TODO: Display List as unexpandable
        //to show the list of tiles//
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("tileList");

        

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();
        //end for the list of tiles

        if(GUILayout.Button("Create")) {
            Material material = new Material(Shader.Find("Specular"));


            ForwardRendererData rd = new ForwardRendererData();
            ScriptableRenderer[] m_Renderers = new ScriptableRenderer[1];
            
            
            
            // TODO: Get reference to rendererdata and set render features with override materials according to the data in this window
            // ForwardRendererData.;

            ScriptableObject rendererData = ScriptableRendererData.CreateInstance("ForwardRendererData");
            AssetDatabase.CreateAsset(rendererData, "Assets/" + renderDataName + ".asset");

            

            // ScriptableObject renderFeature = ScriptableRendererFeature.CreateInstance("RenderObjects");
            // // AssetDatabase.CreateAsset(renderFeature, "Assets/" + renderDataName + "/" + renderFeatureName + ".asset");
            // AssetDatabase.AddObjectToAsset(renderFeature, "Assets/" + renderDataName + ".asset");

            for (int i = 0; i < howMany; i++) {
                
            }
            
        }
        if(GUILayout.Button("Add Render Feature")) {
            ScriptableObject renderFeature = ScriptableRendererFeature.CreateInstance("RenderObjects");
            AssetDatabase.AddObjectToAsset(renderFeature, "Assets/" + renderDataName + ".asset");
        }
        // ForwardRenderer;
    }
}
