using UnityEngine;
using System.Collections;

public static class VectorUtils 
{
    public static Vector2 Rotate(this Vector2 _from, Vector2 _to)
    {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(Vector3.zero, Quaternion.FromToRotation(_from, _to), Vector3.one);

        return mat.MultiplyVector(_from.ToXZ()).ToPoint_XZ();
    }
    public static Vector3 Rotate(this Vector3 _from , Vector3 _to) {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(Vector3.zero,Quaternion.FromToRotation(_from,_to),Vector3.one);

        return mat.MultiplyVector(_from);
    }
    public static Vector2 Rotate(this Vector2 _from, float _angle)
    {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(Vector3.zero, Quaternion.AngleAxis(_angle, Vector3.up), Vector3.one);

        return mat.MultiplyVector(_from.ToXZ()).ToPoint_XZ();
    }
    public static Vector3 Rotate(this Vector3 _from, float _angle)
    {
        Matrix4x4 mat = new Matrix4x4();
        mat.SetTRS(Vector3.zero, Quaternion.AngleAxis(_angle, Vector3.up), Vector3.one);

        return mat.MultiplyVector(_from);
    }
    /// <summary>
    /// 传入两个点，以及一个原点，求当前当前与原点构成的向量是否在向量A 和向量 B 之间
    /// </summary>
    /// <param name="p">点p</param>
    /// <param name="o">原点o</param>
    /// <param name="a">a点</param>
    /// <param name="b">b点</param>
    /// <returns></returns>
    public static bool Between(this Vector2 p, Vector2 o,Vector2 a_point,Vector2 b_point) {

        var a = a_point - o;
        var b = b_point - o;


        //【定比分点公式】
        //使用内分公式,计算 向量a和向量b 之间比例为 t : 1- t的位置的 
        // p = (1 - t) * a + t * b,( 0 <= t <= 1)，该公式表示 向量 a-b 上的任意一点
        //用α  表示 （1 - t）,β 表示 t 
        //则有 p = αa + βb  (α >= 0,β>= 0 ,且α+β= 1)

        //将公式经过变换后得到最简公式
        // delta = a.x * b,y - b.x * a.y
        // α = dx * (b.y - -b.x) / delta
        // β = dy * (-a.y + a.x) / delta

        var dx = p.x - o.x;
        var dy = p.y - o.y;

        var delta = a.x * b.y - b.x * a.y;
        var alpha = (dx * b.y - dy * b.x) / delta;
        var beta = (-dx * a.y + dy * a.x) / delta;

        if (alpha >= 0 && beta >= 0)
        {
            // 向量p 在 向量a 和 b 之间
            return true;
        }
        return false;
    }
}
