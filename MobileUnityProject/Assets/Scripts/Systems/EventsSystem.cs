using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSystem : MonoBehaviour
{
    public delegate void StartGameEvent();
    public event StartGameEvent m_StartGameEvent;

    public delegate void PrincessCaughtEvent();
    public event PrincessCaughtEvent m_PrincessCaughtEvent;

    public delegate void PrincessFellEvent();
    public event PrincessFellEvent m_PrincessFellEvent;

    public delegate void HitScaleEvent();
    public event HitScaleEvent m_HitScaleEvent;

    public void StartGame()
    {
        m_StartGameEvent.Invoke();
    }


}
