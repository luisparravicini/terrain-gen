using UnityEngine;

public class VoxelRenderer : MonoBehaviour
{
    public Vector2Int size;
    public GameObject voxelPrefab;
    public Texture2D heightmap;
    public Texture2D colormap;
    public float maxHeight;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        Transform containerTransform = transform;

        //RemoveChildren(containerTransform);

        var centerPos = containerTransform.position - new Vector3(size.x / 2, 0, size.y / 2);
        centerPos += new Vector3(0.5f, 0, 0.5f);

        var heights = heightmap.GetPixels();
        Debug.Log("heightmap: " + heights.Length + " pixels");
        var colors = colormap.GetPixels();
        Debug.Log("colormap: " + colors.Length + " pixels");

        //Dictionary<Color,int> materials = new Dictionary<Color, int>();
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                var position = centerPos + new Vector3(x, 0, z);
                GameObject obj = Instantiate(voxelPrefab, position, voxelPrefab.transform.rotation, containerTransform);

                var height = heights[x + z * x].grayscale;
                var scale = obj.transform.localScale;
                scale.y = height * maxHeight;
                obj.transform.localScale = scale;

                var color = colors[x + z * x];
                //if (!materials.ContainsKey(color))
                //    materials[color] = 1;
                obj.GetComponentInChildren<MeshRenderer>().material.color = color;
            }

        }
        //Debug.Log("different colors: "+materials.Count);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, 1, size.y));
    }

}
