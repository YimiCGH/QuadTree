using UnityEngine;
using System.Collections;

public interface I_Range
{
    bool ContainPoint(Vector2 _point);
    bool IntersectWith(Rectangle _rect);
}
