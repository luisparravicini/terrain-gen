using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(VoxelRenderer))]
public class VoxelRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            ((VoxelRenderer)target).Generate();
        }
        if (GUILayout.Button("Remove children"))
        {
            RemoveChildren();
        }
    }

    private void RemoveChildren()
    {
        Transform container = ((VoxelRenderer)target).transform;

        Debug.Log("removing " + container.childCount + " objects");
        while (container.childCount > 0)
        {
            DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
