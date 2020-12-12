using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(TerrainRenderer))]
public class VoxelRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            ((TerrainRenderer)target).GenerateMesh();
        }
        if (GUILayout.Button("Remove children"))
        {
            RemoveChildren();
        }
    }

    private void RemoveChildren()
    {
        Transform container = ((TerrainRenderer)target).transform;

        Debug.Log("removing " + container.childCount + " objects");
        while (container.childCount > 0)
        {
            DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
