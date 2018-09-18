using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TouchScript.Gestures;

public class TabArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_TabEvent;

    private PressGesture m_Pressed;
    private ReleaseGesture m_Released;

    private bool m_HoldPress;

    private void Awake()
    {
        m_Pressed = GetComponent<PressGesture>();
        m_Released = GetComponent<ReleaseGesture>();
    }

    private void Start()
    {
        m_Pressed.StateChanged += OnPress;
        m_Released.StateChanged += OnRelease;
    }

    private void Update()
    {
        if (m_HoldPress)
        {
            Tab();
        }
    }

    private void OnPress(object sender, GestureStateChangeEventArgs e)
    {
        m_HoldPress = true;
    }

    private void OnRelease(object sender, GestureStateChangeEventArgs e)
    {
        m_HoldPress = false;
    }

    private void OnMouseExit()
    {
        m_HoldPress = false;
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Tab();
    //    }
    //}

    private void Tab()
    {
        m_TabEvent.Invoke();
    }
}
