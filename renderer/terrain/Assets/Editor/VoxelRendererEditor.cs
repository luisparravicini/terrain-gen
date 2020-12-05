using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoxelRenderer))]
public class VoxelRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            Generate();
        }
    }

    private void Generate()
    {
        VoxelRenderer container = (VoxelRenderer)target;
        Transform containerTransform = container.transform;

        RemoveChildren(containerTransform);

        var centerPos = containerTransform.position - new Vector3(container.size.x / 2, 0, container.size.y / 2);
        centerPos += new Vector3(0.5f, 0, 0.5f);

        for (int x = 0; x < container.size.x; x++)
        {
            for (int z = 0; z < container.size.y; z++)
            {
                var position = centerPos + new Vector3(x, 0, z);
                GameObject obj = Instantiate(container.voxelPrefab, position, container.voxelPrefab.transform.rotation, containerTransform);
            }

        }
    }

    private void RemoveChildren(Transform container)
    {
        while (container.childCount > 0)
        {
            DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}
