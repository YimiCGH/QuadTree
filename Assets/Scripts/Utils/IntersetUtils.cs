using UnityEngine;
using System.Collections;

public class IntersetUtils
{
    #region 矩形
    public static bool ContainPoint(Rectangle _rect,Vector2 _point) {
        float xoffset = Mathf.Abs(_point.x - _rect.m_center.x);
        float yoffset = Mathf.Abs(_point.y - _rect.m_center.y);

        return xoffset < _rect.m_size.x * 0.5f && yoffset < _rect.m_size.y * 0.5f;
    }
    public static bool ContainRectangle(Rectangle _rect1, Rectangle _rect2) {
        return _rect1.left <= _rect2.left && _rect1.right >= _rect2.right
                && _rect1.top <= _rect2.top && _rect1.bottom >= _rect2.bottom;
    }

    public static bool IntersectWith(Rectangle _rect1, Rectangle _rect2) {
        //逆向思维，不可能相交的情况，取反，就是相交的情况
        //如果rect2 在 rect1 的左边 ，右边，下边，或者上边
        return !(_rect2.left > _rect1.right ||
            _rect2.right < _rect1.left ||
            _rect2.top > _rect1.bottom ||
            _rect2.bottom < _rect1.top);
    }
    #endregion

    #region 圆形
    public static bool ContainPoint(Circle _circle, Vector2 _point)
    {
        return Vector2.SqrMagnitude(_circle.m_center - _point) < _circle.m_radius * _circle.m_radius;
    }
    public static bool IntersectWith(Circle _circle,Rectangle _rect)
    {
        var ex_rect = _rect.Extand(_circle.m_radius, _circle.m_radius);
        var coners = _rect.Coners;

        if (ex_rect.ContainPoint(_circle.m_center))
        { //圆心在扩展矩形内

            //如果在四个角落，只有包含该加点才算相交，否则不算
            var offsetStep = new Vector2(_rect.width + _circle.m_radius, _rect.height + _circle.m_radius) * 0.5f;
            var size = new Vector2(_circle.m_radius, _circle.m_radius);

            var leftTop_rect = new Rectangle(_rect.m_center + offsetStep * new Vector2(-1, 1), size);
            var rightTop_rect = new Rectangle(_rect.m_center + offsetStep * new Vector2(1, 1), size);
            var leftbottom_rect = new Rectangle(_rect.m_center + offsetStep * new Vector2(-1, -1), size);
            var rightbottom = new Rectangle(_rect.m_center + offsetStep * new Vector2(1, -1), size);

            //Gizmos.color = Color.white;

            if (leftTop_rect.ContainPoint(_circle.m_center))
            {
                //CollidsionTest.DrawRectSolid(leftTop_rect);
                //CollidsionTest.DrawPoint(_rect.leftTop);
                return _circle.ContainPoint(_rect.leftTop);
            }

            if (rightTop_rect.ContainPoint(_circle.m_center))
            {
                //CollidsionTest.DrawRectSolid(rightTop_rect);
                //CollidsionTest.DrawPoint(_rect.rightTop);
                return _circle.ContainPoint(_rect.rightTop);
            }

            if (leftbottom_rect.ContainPoint(_circle.m_center))
            {
                //CollidsionTest.DrawRectSolid(leftbottom_rect);
                return _circle.ContainPoint(_rect.leftBottom);
            }

            if (rightbottom.ContainPoint(_circle.m_center))
            {
                //CollidsionTest.DrawRectSolid(rightbottom);
                return _circle.ContainPoint(_rect.rightBottom);
            }

            return true;
        }
        else
        {
            return false;//圆心在扩展矩形外，肯定不相交
        }
    }

    public static bool IntersectWith(Circle _circle, Line2 _line) {
        var circle_center = _circle.m_center;
        var begin = _line.m_point1;
        var end = _line.m_point2;

        //把线段方程代入圆的方程 可以得到一个二次方程
        // a * x^2 + 2 * b * x + c = 0

        //如果 x 有解，说明相交，无解
        //b^2 - a *c >= 0，有解

        var v = end - begin;
        var dx = circle_center.x - begin.x;
        var dy = circle_center.y - begin.y;

        var a = v.x * v.x + v.y * v.y;
        var b = -(dx * v.x + dy * v.y );
        var c = dx * dx + dy * dy - _circle.m_radius * _circle.m_radius;
        var d = b * b - a * c;
        if ( d >= 0) {
            var t = (-b - Mathf.Sqrt(d)) / a;
            if ((t >= 0f)  && (t <= 1f)) {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region 扇形
    public static bool ContainPoint(Fan _fan, Vector2 _point)
    {
        var a =  _fan.Left;//向量a，即扇形的左边
        var b = _fan.Right;//向量b，即扇形的右边

        //判断该点和圆心构成的向量是否在两条边之间
        if (_point.Between(_fan.m_center,a,b)) {
            //判断是否在扇形内
            if (Vector2.SqrMagnitude(_point - _fan.m_center) <= Mathf.Pow( _fan.Length,2))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IntersectWith(Fan _fan, Rectangle _rect) {

        //条件一：矩形四个角落的判断
        var coners = _rect.Coners;
        for (int i = 0; i < 4; i++)
        {
            if (_fan.ContainPoint(coners[i]))
                return true;
        }
        //条件二：扇形的两个边点落在矩形内
        if (_rect.ContainPoint(_fan.Left)) {
            return true;
        }
        if (_rect.ContainPoint(_fan.Right)) {
            return true;
        }
        //条件三：彼此中心点
        if (_rect.ContainPoint(_fan.m_center))
        {
            return true;
        }
        if (_fan.ContainPoint(_rect.m_center))
        {
            return true;
        }

        //条件四，矩形的边和弧相交
        var begin = _fan.m_center;
        var end = _rect.m_center;
        var v = end - begin;
        //圆的方程（x - xc）^2 + ( y - yc ) ^ 2 = r^2 
        //线段的方程 
        // x = v.x * t + begin.x
        // y = v.y * t + begin.y

        //求扇形圆心和矩形的中心线段


        //把线段方程代入可以得到一个二次方程
        //把它化为形如 a * x^2 + 2 * b * x + c = 0

        //如果 x 有解，说明相交，无解
        //b^2 - a *c >= 0，有解

        var dx = _fan.m_center.x - begin.x;
        var dy = _fan.m_center.y - begin.y;
        var sqr_r = _fan.Length * _fan.Length;

        var a = v.x * v.x + v.y * v.y;
        var b = -(dx * v.x + dy * v.y);
        var c = dx * dx + dy * dy - sqr_r;
        var d = b * b - a * c;
        Color old = Gizmos.color;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(begin.ToXZ(), end.ToXZ());
        Gizmos.color = old;
        if (d >= 0) // 有解
        {            
            var t = (-b + Mathf.Sqrt(d)) / a;       

            var x = v.x * t + begin.x;
            var y = v.y * t + begin.y;
            var point = new Vector2(x, y);
            GizmosUtils.DrawPoint(point);
            //扇形只会包含其中一个解
            var left = _fan.Left;
            var right = _fan.Right;


            if (_rect.ContainPoint(point) && point.Between(_fan.m_center, left, right))
            {
               
                return true;
            }

            //另一个方向
            t = (-b + Mathf.Sqrt(d)) / a;
            x = v.x * t + begin.x;
            y = v.y * t + begin.y;
            point = new Vector2(x, y);
            GizmosUtils.DrawPoint(point);

            if ( _rect.ContainPoint(point) && point.Between(_fan.m_center, left, right))
            {
                return true;
            }
        }
        else {
            //无解，说明要么扇形包含矩形中心，要么矩形包含扇形圆心，否则就不相交
        }

        return false;
    }
    public static bool IntersectWith(Fan _fan,Circle _circle) {
        var a = _fan.Left;//向量a，即扇形的左边
        var b = _fan.Right;//向量b，即扇形的右边
        var point = _circle.m_center;

        //条件一：扇形圆心 在 圆内
        if (_circle.ContainPoint(_fan.m_center)) {
            return true;
        }

        //条件二：,判断该点和圆心构成的向量是否在两条边之间
        if (point.Between(_fan.m_center, a, b))
        {
            //圆形的圆心 与 扇形的圆心 的距离 ，小于 “圆形的半径 + 扇形的半径”
            var addRadius = _fan.Length + _circle.m_radius;
            //判断是否在扇形内
            if (Vector2.SqrMagnitude(point - _fan.m_center) <= Mathf.Pow(addRadius, 2))
            {
                return true;
            }
        }

        //条件三：扇形 的边与 圆形相交

        if (IntersectWith(_circle,new Line2(_fan.m_center, _fan.Left))) {
            return true;
        }
        if (IntersectWith(_circle, new Line2(_fan.m_center, _fan.Right))){
            return true;
        }

        return false;
    }
    #endregion

    
}
