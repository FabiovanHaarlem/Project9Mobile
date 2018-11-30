using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeArea : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_SwordThrowEvent;

    [SerializeField]
    private GameObject m_Player;

    private void OnMouseDown()
    {
        PressSwordThrow();
    }

    private void PressSwordThrow()
    {
        if (m_Player.transform.position.y + 1f < Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
        {
            m_SwordThrowEvent.Invoke();
        }
    }
}
