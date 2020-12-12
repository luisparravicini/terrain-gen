using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class TerrainRenderer : MonoBehaviour
{
    public Vector2Int size;
    float maxHeight;
    public Gradient heightColors;
    Vector2 noiseOffset;


    [Range(1, 6)]
    public int Scale = 50;

    public float Frequency_01 = 5f;
    public float FreqAmp_01 = 3f;

    public float Frequency_02 = 6f;
    public float FreqAmp_02 = 2.5f;

    public float Frequency_03 = 3f;
    public float FreqAmp_03 = 1.5f;

    public float Frequency_04 = 2.5f;
    public float FreqAmp_04 = 1f;

    //private void Start()
    //{
    //    GenerateMesh();
    //}

    public void GenerateMesh()
    {
        noiseOffset = new Vector2(Random.Range(0, 99999f), Random.Range(0, 99999f));
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

    float Calculate(float x, float z)
    {
        float[] octaveFrequencies = new float[] { Frequency_01, Frequency_02, Frequency_03, Frequency_04   };
        float[] octaveAmplitudes = new float[] { FreqAmp_01, FreqAmp_02, FreqAmp_03, FreqAmp_04 };
        float y = 0;

        for (int i = 0; i < octaveFrequencies.Length; i++)
        {
            y += octaveAmplitudes[i] * Mathf.PerlinNoise(
                     octaveFrequencies[i] * x + noiseOffset.x * Scale,
                     octaveFrequencies[i] * z + noiseOffset.y * Scale);

        }

        return y;
    }
}
