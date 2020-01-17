using UnityEngine;
using System.Collections;

public static class PointUtil 
{
    //转为XZ平面的点，z轴默认为0
    public static Vector3 ToXZ(this Vector2 _point,float _z = 0) {
        return new Vector3(_point.x,_z,_point.y);
    }
    
    public static Vector2 ToPoint_XZ(this Vector3 _point) {
        return new Vector2(_point.x,_point.z);
    }
}
