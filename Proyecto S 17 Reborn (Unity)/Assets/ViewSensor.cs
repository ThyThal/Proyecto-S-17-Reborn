using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSensor : MonoBehaviour
{
    [SerializeField] public float _distance = 10f;
    [SerializeField] [Range(0f, 180f)] public float _angle = 45f;
    [SerializeField] public float _height = 1f;

    [SerializeField] public Color _sensorColor = Color.red;
    [SerializeField] public Mesh _sensorMesh;

    private Mesh CreateWedgeMesh()
    {
        _sensorMesh = new Mesh();

        int segments = 10;
        int trianglesNumber = (segments * 4) + 2 + 2;
        int verticesNumber = trianglesNumber * 3;

        Vector3[] vertices = new Vector3[verticesNumber];
        int[] triangles = new int[verticesNumber];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;

        int vert = 0;

        // Left
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // Right
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -_angle;
        float deltaAngle = (_angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;

            topLeft = bottomLeft + Vector3.up * _height;
            topRight = bottomRight + Vector3.up * _height;

            // Farside
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // Top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // Bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }




        for (int i = 0; i < verticesNumber; i++)
        {
            triangles[i] = i;
        }

        _sensorMesh.vertices = vertices;
        _sensorMesh.triangles = triangles;
        _sensorMesh.RecalculateNormals();

        return _sensorMesh;
    }

    private void OnValidate()
    {
        _sensorMesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (_sensorMesh)
        {
            Gizmos.color = _sensorColor;
            Gizmos.DrawMesh(_sensorMesh, transform.position, transform.rotation);
        }
    }
}
