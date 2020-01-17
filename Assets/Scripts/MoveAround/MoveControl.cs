using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour
{
    public float m_speed;

    Vector2 m_offsetDelat;
    // Use this for initialization
    

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey) {
            return;
        }

        m_offsetDelat.x = Input.GetAxis("Horizontal");
        m_offsetDelat.y = Input.GetAxis("Vertical");

        transform.Translate(m_offsetDelat.ToXZ() * Time.deltaTime * m_speed);

    }
}
