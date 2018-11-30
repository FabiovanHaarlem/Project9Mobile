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

    public delegate void PrincessFallingEvent();
    public event PrincessFallingEvent m_PrincessFallingEvent;

    public delegate void HitScaleEvent();
    public event HitScaleEvent m_HitScaleEvent;

    public delegate void ChristmassEvent();
    public event ChristmassEvent m_ChristmassEvent;

    public void StartGame()
    {
        m_StartGameEvent.Invoke();
    }

    public void PrincessCaught()
    {
        m_PrincessCaughtEvent.Invoke();
    }

    public void PrincessFell()
    {
        m_PrincessFellEvent.Invoke();
    }

    public void PrincessFalling()
    {
        m_PrincessFallingEvent.Invoke();
    }

    public void HitScale()
    {
        m_HitScaleEvent.Invoke();
    }

    public void Christmass()
    {
        m_ChristmassEvent.Invoke();
    }
}
