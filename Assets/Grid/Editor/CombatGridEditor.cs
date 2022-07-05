using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombatGrid))]
public class CombatGridEditor : Editor
{

    CombatGrid grid;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        grid = (CombatGrid)target;
        DrawDefaultInspector();

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Grid"))
        {
            grid.GenerateGrid();
            EditorUtility.SetDirty(grid);
        }
            
        if (GUILayout.Button("Clear Grid"))
        {
            grid.ClearGrid();
            EditorUtility.SetDirty(grid);
        }
            
        GUILayout.EndHorizontal();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

}
