using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerPosition;
    [SerializeField]
    private Transform m_PlayerStartPosition;
    [SerializeField]
    private Transform m_SwordThrowPoint;
    [SerializeField]
    private Sprite m_ChristmassSprite;
    private SpriteRenderer m_SpriteRenderer;

    private Vector2 m_BobUp;
    private Vector2 m_CurrentBobPosition;
    private float m_BobTime;

    private float m_Speed;

    private bool m_IsOnPosition;
    private bool m_BobUpBool;

    private void Awake()
    {
        m_BobUp = new Vector2(transform.position.x, m_PlayerPosition.position.y + 0.1f);
        m_BobUpBool = true;
        m_BobTime = 0.1f;
        m_CurrentBobPosition = m_BobUp;
        m_Speed = 7f;
        m_IsOnPosition = false;
        transform.position = m_PlayerStartPosition.position;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.m_Instance.GetEventSystem.m_ChristmassEvent += ActivateChristmassSprite;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, m_PlayerPosition.position) > 0.001f && !m_IsOnPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_PlayerPosition.position, 0.09f);
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
            BobUpAndDown();
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x >= -2.5f)
        {
            Vector2 pos = transform.position;
            transform.position = new Vector3(pos.x - (m_Speed * Time.deltaTime), pos.y);
            transform.localScale = new Vector3(-0.8f, transform.localScale.y, transform.localScale.z);
            BobUpAndDown();
        }
    }

    private void BobUpAndDown()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_CurrentBobPosition, 2f * Time.deltaTime);
        m_BobTime -= Time.deltaTime;
        if (m_BobTime <= 0)
        {
            if (m_BobUpBool)
            {
                m_CurrentBobPosition = new Vector2(transform.position.x, m_PlayerPosition.position.y);
                m_BobUpBool = false;
            }
            else if (!m_BobUpBool)
            {
                m_CurrentBobPosition = new Vector2(transform.position.x, m_BobUp.y);
                m_BobUpBool = true;
            }
            m_BobTime = 0.1f;
        }
    }

    public void ResetPlayerToGround()
    {
        if (m_PlayerPosition != null)
        {
            transform.position = new Vector2(transform.position.x, m_PlayerPosition.position.y);
        }
    }

    public void ThrowSword()
    {
        Sword sword = GameManager.m_Instance.GetObjectPool.GetSword();
        if (sword != null)
        {
            //Vector2 direction = GameManager.m_Instance.GetBulletDirection(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 direction = GameManager.m_Instance.GetBulletDirection(m_SwordThrowPoint.position, new Vector2(m_SwordThrowPoint.position.x, m_SwordThrowPoint.position.y + 1f));
            sword.Activate(m_SwordThrowPoint.position, direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Attack"))
        {
            GameManager.m_Instance.HitByFireBall();
        }
        else if (collision.collider.CompareTag("Princess"))
        {
            GameManager.m_Instance.GetEventSystem.PrincessCaught();
        }
    }

    private void ActivateChristmassSprite()
    {
        m_SpriteRenderer.sprite = m_ChristmassSprite;
    }
}
