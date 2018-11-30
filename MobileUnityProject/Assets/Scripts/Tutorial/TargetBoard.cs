using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetBoard : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_HitEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_HitEvent.Invoke();
        gameObject.SetActive(false);
    }

}
