using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadTreeNode
{
    Rectangle m_rect;
    List<I_Storeable> m_objects;
    List<I_Storeable> m_shareObjects;

    public QuadTreeNode(Vector2 _center, Vector2 _size)
    {
        m_rect = new Rectangle(_center,_size);
        m_objects = new List<I_Storeable>();
        m_shareObjects = new List<I_Storeable>();
    }
    public QuadTreeNode(float _x,float _y,float _width,float _height) {
        m_rect = new Rectangle (new Vector2(_x, _y), new Vector2(_width,_height));
        m_objects = new List<I_Storeable>();
        m_shareObjects = new List<I_Storeable>();
    }
    public QuadTreeNode(Rectangle _rect)
    {
        m_rect = _rect;
        m_objects = new List<I_Storeable>();

    }

    public Rectangle Rect { get { return m_rect; } }

    public QuadTreeNode Parent { get; set; }

    public QuadTreeNode[] Children { get; set; }

    public List<I_Storeable> Objects { get { return m_objects; } }
    public List<I_Storeable> ShareObjects { get { return m_shareObjects; } }


}
