using UnityEngine;
using System.Collections;

public static class FanUtils
{

    public static Fan LookForwar(this Fan _fan, Vector3 _forward) {
        return new Fan(_fan.m_center, _forward, _fan.OpenAngle, _fan.Length);
    }
    public static Fan Rotate(this Fan _fan,float _angle) {
        var forward = _fan.forward.Rotate(_angle);
        return new Fan(_fan.m_center, forward, _fan.OpenAngle, _fan.Length);
    }
}
