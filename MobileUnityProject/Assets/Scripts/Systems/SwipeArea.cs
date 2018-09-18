using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TouchScript.Gestures;

public class SwipeArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_FlickEvent;

    private FlickGesture m_Flick;

    private void Awake()
    {
        m_Flick = GetComponent<FlickGesture>();
    }

    private void Start()
    {
        m_Flick.Flicked += Flick;
    }

    private void Flick(object sender, System.EventArgs e)
    {
        m_FlickEvent.Invoke();
    }
}
