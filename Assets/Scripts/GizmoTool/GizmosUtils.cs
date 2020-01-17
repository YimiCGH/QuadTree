using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GizmosUtils
{
    public static void DrawRect(Rectangle _rect, Color _color)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = _color;

        var conners = _rect.Coners;
        Gizmos.DrawLine(conners[0].ToXZ(), conners[1].ToXZ());
        Gizmos.DrawLine(conners[1].ToXZ(), conners[2].ToXZ());
        Gizmos.DrawLine(conners[2].ToXZ(), conners[3].ToXZ());
        Gizmos.DrawLine(conners[3].ToXZ(), conners[0].ToXZ());
        Gizmos.color = oldColor;

    }
    public static void DrawRectSolid(Rectangle _rect,Color _color)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = _color;
        Gizmos.DrawCube(_rect.m_center.ToXZ(), _rect.m_size.ToXZ());
        Gizmos.color = oldColor;

    }
    public static void DrawPoint(Vector2 _point)
    {
        Gizmos.DrawSphere(_point.ToXZ(), 0.2f);
    }
  
    public static void Draw2DLine(Vector2 from, Vector2 to)
    {
        Gizmos.DrawLine(new Vector3(from.x, 0, from.y), new Vector3(to.x, 0, to.y));
    }

    #region 绘制圆形，扇形
    public static void DrawCircle(Circle _circle, Color _color)
    {
        Gizmos.color = _color;

        Vector3 pos = _circle.m_center.ToXZ() + Vector3.up * 0.01f; // 0.01fは地面と高さだと見づらいので調整用。

        Quaternion rot = Quaternion.identity;
        Vector3 scale = Vector3.one * _circle.m_radius;

        Mesh fanMesh = VisualRange_GizmosEditor.CreateFanMesh(360f, 18);
        Gizmos.DrawMesh(fanMesh, pos, rot, scale);
    }
    public static void DrawFan(Fan _fan, Color _color)
    {
        Gizmos.color = _color;

        Vector3 pos = _fan.m_center.ToXZ() + Vector3.up * 0.01f; // 0.01fは地面と高さだと見づらいので調整用。
        Vector3 forward = _fan.forward.ToXZ();
        //Debug.Log(forward);
        Quaternion rot = Quaternion.identity;
        if (forward != Vector3.forward) {
            rot = Quaternion.LookRotation(forward, Vector3.up);
        }
        
        Vector3 scale = Vector3.one * _fan.Length;

        Mesh fanMesh = VisualRange_GizmosEditor.CreateFanMesh(_fan.OpenAngle, 16);
        Gizmos.DrawMesh(fanMesh, pos, rot, scale);

        Gizmos.DrawSphere(_fan.Left.ToXZ(),0.1f);
        Gizmos.DrawSphere(_fan.Right.ToXZ(), 0.1f);

    }


    #endregion
}
