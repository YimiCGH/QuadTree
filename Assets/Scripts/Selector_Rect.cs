﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector_Rect : MonoBehaviour
{
    public QuadTree m_quadTree;

    Vector3 begin;
    Vector3 end;
    Rectangle m_rect;

 

    bool drawRect;

    List<Rectangle> m_selectedRects;
    List<I_Storeable> m_targets;


    // Use this for initialization
    void Start()
    {
        m_selectedRects = new List<Rectangle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            begin = Input.mousePosition;
            ToggleTargets(false);
            m_selectedRects.Clear();
            drawRect = true;
        }
        if (drawRect) {
            if (Input.GetMouseButton(1))
            {
                end = Input.mousePosition;

            }
            if (Input.GetMouseButtonUp(1))
            {
                drawRect = false;
                SelectRect();
            }
        }
    }


    void ToggleTargets(bool _isSelected) {
        if (m_targets != null && m_targets.Count > 0) {
            for (int i = 0; i < m_targets.Count; i++)
            { 
                var target = m_targets[i];
                target.ToggleSelected(_isSelected);
            }
        }
    }

    private void SelectRect()
    {
        m_targets = m_quadTree.SelectInRange(m_rect,out m_selectedRects);
        Debug.Log(m_targets.Count);
        ToggleTargets(true);
    }

    Vector3 center;
    Vector3 size;

    private void OnDrawGizmos()
    {
        HighlightSelectedRects();

        if (!drawRect) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(begin);
        Plane plane = new Plane(Vector3.up,Vector3.zero);
        Vector3 p1 = Vector3.zero;
        if (plane.Raycast(ray,out float distance)) {
            p1 = ray.GetPoint(distance);
        }
        Gizmos.DrawLine(ray.origin,p1);

        ray = Camera.main.ScreenPointToRay(end);
        Vector3 p2 = Vector3 .zero;
        if (plane.Raycast(ray, out float distance2))
        {
            p2 = ray.GetPoint(distance2);
        }
        Gizmos.DrawLine(ray.origin,p2);

        center = (p1 + p2) * 0.5f;
        size = new Vector3(Mathf.Abs( p1.x - p2.x),0.1f, Mathf.Abs( p1.z - p2.z));

        m_rect = new Rectangle(center.x, center.z,size.x,size.z);
    }

    void HighlightSelectedRects() {

        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(center, size);

        Gizmos.color = new Color(0,1,0,0.5f);

        if (m_selectedRects!= null && m_selectedRects.Count > 0) {
            for (int i = 0; i < m_selectedRects.Count; i++)
            {
                var rect = m_selectedRects[i];

                Gizmos.DrawCube(new Vector3(rect.m_center.x,0,rect.m_center.y),
                    new Vector3(rect.m_size.x,0,rect.m_size.y));
            }
        }
    }
}
