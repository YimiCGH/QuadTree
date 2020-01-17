using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CollidsionTest : MonoBehaviour
{
    [SerializeField]
    public Rectangle m_rect;
    [SerializeField]
    public Circle m_circle;
    [SerializeField]
    public Fan m_fan = new Fan(Vector2.zero,new Vector2(0,1),60,1);
    public Vector2 m_point;


    public Color m_intersectCol = new Color(1,0,0,0.6f);
    public Color[] m_colors = {
        new Color(0, 1, 0, 0.6f),
        new Color(0, 0.6f, 0.6f, 0.6f),
        new Color(0.6f, 1, 0, 0.6f),
        new Color(0, 1, 0, 0.6f),

    };
    private void OnDrawGizmos()
    {
        bool isRectInersctWithCircle = m_circle.IntersectWith( m_rect);
        bool isFanIntersectWithCircle = m_fan.IntersectWith(m_circle);
        bool isFanIntersectWithRect = m_fan.IntersectWith(m_rect);


        Color rectColor;
        if (isRectInersctWithCircle)
            rectColor = m_intersectCol;
        else
            rectColor = m_colors[0];
        GizmosUtils.DrawRectSolid(m_rect, rectColor);
        GizmosUtils.DrawRect(m_rect.Extand(m_circle.m_radius,m_circle.m_radius), rectColor);

        Color circleColor;
        if (isRectInersctWithCircle || isFanIntersectWithCircle)
            circleColor = m_intersectCol;
        else
            circleColor = m_colors[1];
        GizmosUtils.DrawCircle(m_circle, circleColor);


        Color fanColor;
       
        if (isFanIntersectWithCircle || isFanIntersectWithRect)
            fanColor = m_intersectCol;
        else
            fanColor = m_colors[2];
        GizmosUtils.DrawFan(m_fan, fanColor);


        if (m_circle.ContainPoint(m_point) || m_rect.ContainPoint(m_point) || m_fan.ContainPoint(m_point))
        {
            Gizmos.color = m_intersectCol;
        }
        else {
            Gizmos.color = m_colors[3];
        }
        GizmosUtils.DrawPoint(m_point);
    }
}
