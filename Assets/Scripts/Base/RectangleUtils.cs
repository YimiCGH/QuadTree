using UnityEngine;
using System.Collections;

public static class RectangleUtils
{
    /// <summary>
    /// 将矩形向外扩展
    /// </summary>
    /// <param name="_rect"></param>
    /// <param name="_width">左右各自扩展的宽度</param>
    /// <param name="_height">上下各自扩展的宽度</param>
    /// <returns></returns>
    public static Rectangle Extand(this Rectangle _rect,float _width,float _height) {
        return new Rectangle(_rect.m_center, new Vector2(_rect.m_size.x + _width * 2,_rect.m_size.y + _height * 2));
    }
}
