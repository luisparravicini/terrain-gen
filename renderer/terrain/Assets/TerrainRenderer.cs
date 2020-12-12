using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct NoiseOctave
{
    public float octave;
    public float amplitude;
}


[RequireComponent(typeof(MeshFilter))]
public class TerrainRenderer : MonoBehaviour
{
    public Vector2Int size;
    float maxHeight;
    public Gradient heightColors;
    Vector2 noiseOffset;
    public int Scale = 50;
    public NoiseOctave[] octaves;
    public bool random;

    //private void Start()
    //{
    //    InvokeRepeating(nameof(GenerateMesh),0,2);
    //}

    private void Update()
    {
        GenerateMesh();
    }

    public void GenerateMesh()
    {
        if (random)
        {
            noiseOffset = new Vector2(Random.Range(0, 99999f), Random.Range(0, 99999f));
        }
        else
        {
            noiseOffset = Vector2.zero;
        }
        Debug.Log("using noise offset: " + noiseOffset);


        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        Transform containerTransform = transform;
        var centerPos = containerTransform.position - new Vector3(size.x / 2, 0, size.y / 2);
        centerPos += new Vector3(0.5f, 0, 0.5f);

        maxHeight = float.MinValue;
        List<Vector3> vertices = new List<Vector3>();
        for (int z = 0; z < size.y; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var pos = centerPos + new Vector3(x, 0, z);
                pos.y = Calculate(x, z);
                maxHeight = Mathf.Max(maxHeight, pos.y);

                vertices.Add(pos);
            }
        }
        mesh.vertices = vertices.ToArray();
        Debug.Log("vertices: " + mesh.vertices.Length);
        Debug.Log("maxHeight: " + maxHeight);

        List<Color> colors = new List<Color>(vertices.Count);
        for (int i = 0; i < vertices.Count; i++)
        {
            colors.Add(heightColors.Evaluate(vertices[i].y / maxHeight));
        }
        mesh.colors = colors.ToArray();
        Debug.Log("colors: " + colors.Count);

        List<int> tris = new List<int>();
        for (int z = 0; z < size.y - 1; z++)
        {
            for (int x = 0; x < size.x - 1; x++)
            {
                var index = x + z * size.y;
                tris.Add(x + (z + 1) * size.y);
                tris.Add(index + 1);
                tris.Add(index);

                tris.Add(x + (z + 1) * size.y);
                tris.Add(x + 1 + (z + 1) * size.y);
                tris.Add(index + 1);
            }
        }
        mesh.triangles = tris.ToArray();
        Debug.Log("tris: " + mesh.triangles.Length);

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, 1, size.y));
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Gizmos.DrawLine(vertices[tris[i]], vertices[tris[i + 1]]);
            Gizmos.DrawLine(vertices[tris[i + 1]], vertices[tris[i + 2]]);
            Gizmos.DrawLine(vertices[tris[i + 2]], vertices[tris[i]]);
        }
    }

    float Calculate(float x, float z)
    {
        float y = 0;

        for (int i = 0; i < octaves.Length; i++)
        {
            var octave = octaves[i];
            y += octave.amplitude * Mathf.PerlinNoise(
                     (octave.octave * x / size.x + noiseOffset.x) * Scale,
                     (octave.octave * z / size.y + noiseOffset.y) * Scale);

        }

        return y;
    }
}
