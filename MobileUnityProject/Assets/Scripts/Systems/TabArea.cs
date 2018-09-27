using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TouchScript.Gestures;

public class TabArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_MoveRightEvent;
    [SerializeField]
    private UnityEvent m_MoveLeftEvent;
    [SerializeField]
    private GameObject m_Player;

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
            if (m_Player.transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                MovePlayerRight();
            }

            if (m_Player.transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            {
                MovePlayerLeft();
            }
            //Tab();
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

    private void MovePlayerLeft()
    {
        m_MoveLeftEvent.Invoke();
    }

    private void MovePlayerRight()
    {
        m_MoveRightEvent.Invoke();
    }

    //private void Tab()
    //{
    //    m_TabEvent.Invoke();
    //}
}
