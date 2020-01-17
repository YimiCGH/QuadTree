using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Fan : I_Range
{
    public Vector2 m_center;
    public Vector2 m_forward;
    
    public float m_length;
    public float m_openAngle;


    public Fan(Vector2 _center, Vector3 _forward, float _angle, float _lenght)
    {
        m_center = _center;
        m_forward = _forward.ToPoint_XZ();
        m_openAngle = _angle;
        m_length = _lenght;


        float rotate = Vector3.Angle(Vector3.forward, _forward) * Mathf.Deg2Rad;

        float radian = _angle * Mathf.Deg2Rad;

        float startRed = -radian * 0.5f + rotate;
        float endRed = -startRed + rotate;
     
    }

    public Fan(Vector2 _center, Vector2 _forward, float _openAngle, float _length)
    {
        m_center = _center;
        m_forward = _forward;
        m_openAngle = _openAngle;
        m_length = _length;

    }

    public Vector2 forward {
        get {
            return m_forward;
        }
    }


    public Vector2 Left { get { return (m_forward.Rotate(-m_openAngle * 0.5f) ).normalized * m_length + m_center; } }
    public Vector2 Right { get { return (m_forward.Rotate(m_openAngle * 0.5f) ).normalized * m_length + m_center; } }

    public float Length { get { return m_length; } }

    public float OpenAngle { get { return m_openAngle; } }



    public bool ContainPoint(Vector2 _point)
    {
        return IntersetUtils.ContainPoint(this, _point);
    }

    public bool IntersectWith(Rectangle _rect)
    {
        return IntersetUtils.IntersectWith(this, _rect);
    }
    public bool IntersectWith(Circle circle)
    {
        return IntersetUtils.IntersectWith(this, circle);
    }
}
