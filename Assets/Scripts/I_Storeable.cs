using UnityEngine;
using System.Collections;

public interface I_Storeable
{
    Vector2 Position { get; set; }

    void ToggleSelected(bool _Toggle);
}
