using UnityEngine;
using System.Collections;
[System.Serializable]
public struct Circle:I_Range
{
    public Vector2 m_center;
    public float m_radius;

    public Circle(Vector2 _center,float _radius) {
        m_center = _center;
        m_radius = _radius;
    }

    public float left { get { return m_center.x - m_radius; } }
    public float right { get { return m_center.x + m_radius; } }
    public float top { get { return m_center.y + m_radius; } }
    public float bottom { get { return m_center.y - m_radius; } }

    public bool ContainPoint(Vector2 _point)
    {
        return IntersetUtils.ContainPoint(this,_point);
    }

    public bool IntersectWith(Rectangle _rect)
    {
        return IntersetUtils.IntersectWith(this, _rect);
    }
}
