using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerPosition;
    [SerializeField]
    private Transform m_PlayerStartPosition;
    private float m_Speed;

    private bool m_IsOnPosition;

    private void Awake()
    {
        m_Speed = 3.5f;
        m_IsOnPosition = false;
        transform.position = m_PlayerStartPosition.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, m_PlayerPosition.position) > 0.001f && !m_IsOnPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_PlayerPosition.position, 0.03f);
        }
        else
        {
            m_IsOnPosition = true;
        }

    }

    public void MoveRight()
    {
        if (transform.position.x <= 2.5f)
        {
            Vector2 pos = transform.position;
            transform.position = new Vector3(pos.x + (m_Speed * Time.deltaTime), pos.y);
            transform.localScale = new Vector3(0.8f, transform.localScale.y, transform.localScale.z);
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x >= -2.5f)
        {
            Vector2 pos = transform.position;
            transform.position = new Vector3(pos.x - (m_Speed * Time.deltaTime), pos.y);
            transform.localScale = new Vector3(-0.8f, transform.localScale.y, transform.localScale.z);
        }
    }

    public void ThrowSword()
    {
        Sword sword = GameManager.m_Instance.GetObjectPool.GetSword();
        Vector2 direction = GameManager.m_Instance.GetBulletDirection(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        sword.Activate(transform.position, direction);
    }
}
