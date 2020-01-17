using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeVisualization : MonoBehaviour
{
    [SerializeField,Range(0f, 360f)]
    private float m_angle = 0f;
    [SerializeField, Range(0f, 15f)]
    private float m_lenght = 0f;

    [SerializeField, Range(6f, 20f)]
    
    private int m_triangleAmount = 12;

    public Color m_color = new Color(1.0f, 1.0f, 0.0f, 0.7f);

    public float Angle { get => m_angle; set => m_angle = value; }
    public float Length { get => m_lenght; set => m_lenght = value; }
    public int TriangleAmount { get => m_triangleAmount; set => m_triangleAmount = value; }
    public Mesh Mesh { get => m_mesh; set => m_mesh = value; }

    private Mesh m_mesh;

    void OnValidate() {
        Debug.Log("重建网格");
        m_mesh = null;
    }
}
