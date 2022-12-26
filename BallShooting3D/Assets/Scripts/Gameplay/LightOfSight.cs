using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LightOfSight : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainCharacter;
    Vector3 mcDir = new Vector3(0, 0, 1);

    bool _isShow = false;

    Mesh mesh;

    Vector3[] vertices = new Vector3[4];
    Vector2[] uv = new Vector2[4];
    int[] triangles = new int[6];
    float lightDistance = 5.5f;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;
    }

    void Update()
    {
        if (_isShow)
        {
            float angle = GetAngleFromVectorFloat(mcDir);

            vertices[0] = RotatePoint(new Vector3(-0.05f, 1, 0), new Vector3(), angle);
            vertices[1] = RotatePoint(new Vector3(-0.025f, 1, lightDistance), new Vector3(), angle);
            vertices[2] = RotatePoint(new Vector3(0.05f, 1, 0), new Vector3(), angle);
            vertices[3] = RotatePoint(new Vector3(0.025f, 1, lightDistance), new Vector3(), angle);

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        } else
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg - 90f;
        if (n < 0) n += 360;

        return n;
    }
    Vector3 RotatePoint(Vector3 pointToRotate, Vector3 centerPoint, double angleInDegrees)
    {
        double angleInRadians = angleInDegrees * (Math.PI / 180);
        double cosTheta = Math.Cos(angleInRadians);
        double sinTheta = Math.Sin(angleInRadians);
        return new Vector3
        {
            x =
                (float)
                (cosTheta * (pointToRotate.x - centerPoint.x) -
                sinTheta * (pointToRotate.z - centerPoint.z) + centerPoint.x),
            y = 0,
            z =
                (float)
                (sinTheta * (pointToRotate.x - centerPoint.x) +
                cosTheta * (pointToRotate.z - centerPoint.z) + centerPoint.z)
        };
    }

    public void SetIsShow(bool isShow)
    {
        this._isShow = isShow;
    }
}
