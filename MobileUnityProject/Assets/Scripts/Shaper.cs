using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Shaper : MonoBehaviour
{
    SpriteShapeController m_Controller;

    private void Update()
    {
        m_Controller = GetComponent<SpriteShapeController>();
        m_Controller.spline.SetPosition(1, new Vector3(m_Controller.spline.GetPosition(1).x, Mathf.PingPong(Time.time, 3f)));
        m_Controller.spline.SetPosition(2, new Vector3(m_Controller.spline.GetPosition(2).x, Mathf.PingPong(Time.time, 1f)));
    }

}
