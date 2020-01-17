using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FanRange : MonoBehaviour
{
    public float m_updateRate = 0.5f;
    public QuadTree m_quadTree;
        
    [SerializeField]
    private Fan m_fan;

    List<Rectangle> m_selectedRects;
    List<I_Storeable> m_targets;


    [Range(0f, 360f)]
    public float m_fanAngle = 90f;
    [Range(0.1f, 2f)]
    public float m_viewLength = 1;
    [Range(0f,360f)]
    public float m_rotateAngle = 0;
    // Use this for initialization
    void Start()
    {
        m_fan = new Fan(transform.position.ToPoint_XZ(), transform.forward, m_fanAngle, m_viewLength);
    }
    private void OnValidate()
    {
        m_fan = new Fan(transform.position.ToPoint_XZ(), transform.forward, m_fanAngle, m_viewLength);
        m_fan = m_fan.Rotate(m_rotateAngle);
    }

    float m_timer;
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer < m_updateRate)
        {
            return;
        }
        else
        {
            m_timer = 0;
        }
        ToggleTargets(false);
        m_targets = m_quadTree.SelectInRange(m_fan, out m_selectedRects);

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

    public Color m_color;
    private void OnDrawGizmos()
    {
        GizmosUtils.DrawFan(m_fan, m_color);
    }
}
