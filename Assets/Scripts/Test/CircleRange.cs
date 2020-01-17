using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleRange : MonoBehaviour
{
    public float m_updateRate = 0.5f;
    public QuadTree m_quadTree;

    public RangeVisualization m_visualRange;
    private Circle m_circle;

    List<Rectangle> m_selectedRects;
    List<I_Storeable> m_targets;
    // Use this for initialization
    void Start()
    {
        m_circle = new Circle(m_visualRange.transform.position.ToPoint_XZ(), m_visualRange.Length);
    }

    float m_timer;
    void Update()
    {
        m_circle = new Circle(m_visualRange.transform.position.ToPoint_XZ(), m_visualRange.Length);

        m_timer += Time.deltaTime;
        if (m_timer < m_updateRate)
        {
            return;  
        }
        else {
            m_timer = 0;
        }
        ToggleTargets(false);
        m_targets = m_quadTree.SelectInRange(m_circle,out m_selectedRects);

        ToggleTargets(true);
    }
    void ToggleTargets(bool _isSelected)
    {
        if (m_targets != null && m_targets.Count > 0)
        {
            for (int i = 0; i < m_targets.Count; i++)
            {
                var target = m_targets[i];
                target.ToggleSelected(_isSelected);
            }
        }
    }
}
