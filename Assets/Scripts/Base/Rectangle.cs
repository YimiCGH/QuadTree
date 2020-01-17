using UnityEngine;
using System.Collections;

/// <summary>
/// 坐标空间 为，左上角为原点，上为y轴正方向，右为x轴正方向
/// </summary>
[System.Serializable]
public struct Rectangle:I_Range
{
    public Vector2 m_center;
    public Vector2 m_size;
    public Vector2 m_halfSize;
    public Rectangle(float _x,float _y,float _width,float _height) {
        m_center = new Vector2(_x,_y);
        m_size = new Vector2(_width,_height);
        m_halfSize = m_size * 0.5f;
    }
    public Rectangle(Vector2 _center,Vector2 _size) {
        m_center = _center;
        m_size = _size;
        m_halfSize = m_size * 0.5f;
    }

    public float width { get { return m_size.x; } }
    public float height { get { return m_size.y; } }

    public Vector2[] Coners { get {
            Vector2[] coners = new Vector2[4];
            Vector2 offsetStep = m_size * 0.5f;
            coners[0] = m_center + new Vector2(-1,  1) * offsetStep;
            coners[1] = m_center + new Vector2( 1,  1) * offsetStep;
            coners[2] = m_center + new Vector2( 1, -1) * offsetStep;
            coners[3] = m_center + new Vector2(-1, -1) * offsetStep;
            return coners;
        }
    }

    public float left { get { return m_center.x - m_halfSize.x; } }
    public float right { get { return m_center.x + m_halfSize.x; } }
    public float top { get { return m_center.y - m_halfSize.y; } }
    public float bottom { get { return m_center.y + m_halfSize.y; } }

    
    public Vector2 rightTop
    {
        get
        {
            return m_center + new Vector2(1, 1) * m_halfSize;
        }
    }
    public Vector2 leftTop
    {
        get
        {
            return m_center + new Vector2(-1, 1) * m_halfSize;
        }
    }
    public Vector2 rightBottom
    {
        get
        {
            return m_center + new Vector2(1, -1) * m_halfSize;
        }
    }
    public Vector2 leftBottom
    {
        get
        {
            return m_center + new Vector2(-1, -1) * m_halfSize;
        }
    }

    public bool ContainPoint(Vector2 _point)
    {
        return IntersetUtils.ContainPoint(this, _point);
    }

    public bool IntersectWith(Rectangle _rect)
    {
        return IntersetUtils.IntersectWith(this, _rect);
    }
}
