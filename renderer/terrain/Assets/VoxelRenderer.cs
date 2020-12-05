using UnityEngine;

public class VoxelRenderer : MonoBehaviour
{
    public Vector2Int size;
    public GameObject voxelPrefab;

    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, 1, size.y));
    }

}
