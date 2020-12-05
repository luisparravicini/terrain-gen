using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform transformCenter;
    public float speed;
    public float radius;
    float deltaY;
    float angle;

    void Start()
    {
        if (radius == -1)
            radius = Vector3.Distance(transformCenter.position, transform.position);
        deltaY = transform.position.y;
    }

    void Update()
    {
        angle += Time.deltaTime * speed;

        var pos = transformCenter.position + radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        pos.y += deltaY;
        transform.position = pos;

        transform.LookAt(transformCenter);
    }
}
