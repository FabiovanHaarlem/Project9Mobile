using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HeadParentObject;

    private Vector2 m_HeadMoveTowardsPosition;

    private float m_OffSet;
    private float m_FireBallTimer;

    public void Initialize()
    {
        m_OffSet = 0.5f;
        m_FireBallTimer = 1.5f;
        MoveHead();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_HeadMoveTowardsPosition, 0.004f);

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
    }

    private void ShootFireBall()
    {
        Fireball fireBall = GameManager.m_Instance.m_ObjectPool.GetFireBall();
        Vector2 locationUnderHead = new Vector2(transform.position.x, transform.position.y - 5f);
        fireBall.Activate(transform.position, GameManager.m_Instance.GetBulletDirection(transform.position, locationUnderHead));
    }

    private void MoveHead()
    {
        Vector3 pos = m_HeadParentObject.transform.position;
        m_HeadMoveTowardsPosition = new Vector2(pos.x + Random.Range(-m_OffSet, m_OffSet), pos.y + Random.Range(-m_OffSet, m_OffSet));
    }
}
