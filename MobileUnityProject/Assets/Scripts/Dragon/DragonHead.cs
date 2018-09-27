using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeadParentObject;
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private GameObject m_DragonHeadRotationPoint;

    private SpriteRenderer m_SpriteRenderer;

    private Vector2 m_HeadMoveTowardsPosition;

    private float m_OffSet;
    private float m_FireBallTimer;
    private float m_DropPrincess;
    private bool m_IsInPosition;

    public void Initialize()
    {
        m_IsInPosition = false;
        m_OffSet = 0.5f;
        m_FireBallTimer = 1.5f;
        m_DropPrincess = 5f;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        MoveHead();
    }

    private void Update()
    {
        if (m_IsInPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_HeadMoveTowardsPosition, 0.002f);

            if (m_Player.transform.position.x - transform.position.x <= -1f && m_Player.transform.position.x - transform.position.x >= 1f)
            {
                transform.localScale = new Vector3(1,
                    1, transform.localScale.z);
            }
            else if (m_Player.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(-1,
                    1, transform.localScale.z);
            }
            else if (m_Player.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(1,
                    1, transform.localScale.z);
            }


            RotateHead();

            if (Vector2.Distance(transform.position, m_HeadMoveTowardsPosition) < 0.2f)
            {
                MoveHead();
            }

            m_FireBallTimer -= Time.deltaTime;
            if (m_FireBallTimer <= 0f)
            {
                ShootFireBall();
                m_FireBallTimer = 1.5f;
            }

            m_DropPrincess -= Time.deltaTime;
            if (m_DropPrincess <= 0f)
            {
                DropPrincess();
                m_DropPrincess = 5f;
            }
        }
    }

    private void ShootFireBall()
    {
        Fireball fireBall = GameManager.m_Instance.GetObjectPool.GetFireBall();
        Vector2 playerPos = m_Player.transform.position;
        fireBall.Activate(transform.position, GameManager.m_Instance.GetBulletDirection(transform.position, playerPos));
    }

    private void DropPrincess()
    {
        Princess princess = GameManager.m_Instance.GetObjectPool.GetPrincess();
        Vector2 dropPosHigh = new Vector2(Random.Range(-2f, 2f), transform.position.y);
        Vector2 dropPos = new Vector2(dropPosHigh.x, dropPosHigh.y - 5f);
        princess.Activate(dropPosHigh, GameManager.m_Instance.GetBulletDirection(dropPosHigh, dropPos));
    }

    private void MoveHead()
    {
        if (m_IsInPosition)
        {
            Vector3 pos = m_HeadParentObject.transform.position;
            m_HeadMoveTowardsPosition = new Vector2(pos.x + Random.Range(-m_OffSet, m_OffSet), pos.y + Random.Range(-m_OffSet, m_OffSet));
        }
    }

    private void RotateHead()
    {
        Vector3 moveDirection = m_Player.transform.position - gameObject.transform.position;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            m_DragonHeadRotationPoint.transform.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);
        }
    }

    public void InPosition()
    {
        if (!m_IsInPosition)
        {
            m_IsInPosition = true;
            MoveHead();
        }
    }
}
