using UnityEngine;
using System.Collections;

public class Point:MonoBehaviour, I_Storeable
{
    public Material m_mat;
    public Color m_selected;
    public Color m_normal;
    
    Vector2 m_position;

    private void Start()
    {
        GetComponent<Renderer>().material = new Material(m_mat);
    }

    public Vector2 Position { get { return m_position; } set { m_position = value; } }

    public void ToggleSelected(bool _Toggle)
    {
        if (_Toggle)
        {
            GetComponent<Renderer>().material.SetColor("_Color", m_selected);
        }
        else {
            GetComponent<Renderer>().material.SetColor("_Color", m_normal);

        }
    }
}
