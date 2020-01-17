using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class VisualRange_GizmosEditor 
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawPointGizmos(RangeVisualization _object,GizmoType _gizmoType) {
        if (_object.Length <= 0f) {
            return;
        }
        Gizmos.color = _object.m_color;

        Transform transform = _object.transform;
        Vector3 pos = transform.position + Vector3.up * 0.01f; // 0.01fは地面と高さだと見づらいので調整用。
        Quaternion rot = transform.rotation;
        Vector3 scale = Vector3.one * _object.Length;
        if (_object.Mesh == null) {
            _object.Mesh = CreateFanMesh(_object.Angle, _object.TriangleAmount);
        }
        Mesh fanMesh = _object.Mesh;
        Gizmos.DrawMesh(fanMesh, pos, rot, scale);
    }

    public static Mesh CreateFanMesh(float _angle,int _triangleAmount) {
        var mesh = new Mesh();

        var vertices = CreateFanVertices(_angle,_triangleAmount);
        var triangleIndexes = new List<int>(_triangleAmount + 3);
        for (int i = 0; i < _triangleAmount; i++)
        {
            triangleIndexes.Add(0);
            triangleIndexes.Add(i+1);
            triangleIndexes.Add(i+2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndexes.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    static Vector3[] CreateFanVertices(float _angle,int _triangleAmount) {
        if (_angle <= 0f) {
            throw new System.ArgumentException(string.Format("角度がおかしい！ _angle={0}",_angle));
        }
        if (_triangleAmount <= 0) { 
            throw new System.ArgumentException(string.Format("数がおかしい！ _triangleAmount={0}", _triangleAmount));
        }

        _angle = Mathf.Min(_angle,360);
        var vertices = new List<Vector3>(_triangleAmount + 2);

        //始点
        vertices.Add(Vector3.zero);

        float radian = _angle * Mathf.Deg2Rad;//角度转为弧度
        float startRad = -radian * 0.5f;//起始弧度
        float incRad = radian / _triangleAmount;//increase amount

        for (int i = 0; i < _triangleAmount + 1; i++)
        {
            float curRad = startRad + (incRad * i);

            Vector3 vertex = new Vector3(Mathf.Sin(curRad),0f,Mathf.Cos(curRad));
            vertices.Add(vertex);
        }

        return vertices.ToArray();
    }
}
