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
    [SerializeField]
    private GameObject m_Head;
    [SerializeField]
    private GameObject m_FireBallStartPosition;
    [SerializeField]
    private Sprite m_EasterEggDrekon;
    [SerializeField]
    private AudioSource m_Scream;
    [SerializeField]
    private Sprite m_ChristmassDragonHead;

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
        m_FireBallTimer = 1.1f;
        m_DropPrincess = 7f;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if (StaticDataContainer.m_EasterEgg == true)
        {
            m_Head.GetComponent<SpriteRenderer>().sprite = m_EasterEggDrekon;
        }
        else
        {
            GameManager.m_Instance.GetEventSystem.m_ChristmassEvent += ActivateChristmassSprite;
        }
        MoveHead();
    }

    private void ActivateChristmassSprite()
    {
        m_Head.GetComponent<SpriteRenderer>().sprite = m_ChristmassDragonHead;
    }

    private void Update()
    {
        if (m_IsInPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_HeadMoveTowardsPosition, 0.007f);

            if (m_Player.transform.position.x < transform.position.x)
            {
                MoveHead();
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
                m_FireBallTimer = 1.1f;
            }

            m_DropPrincess -= Time.deltaTime;
            if (m_DropPrincess <= 0f)
            {
                DropPrincess();
                m_DropPrincess = 7f;
            }
        }
    }

    private void ShootFireBall()
    {
        Fireball fireBall = GameManager.m_Instance.GetObjectPool.GetFireBall();
        Vector2 playerPos = m_Player.transform.position;
        fireBall.Activate(m_FireBallStartPosition.transform.position, GameManager.m_Instance.GetBulletDirection(transform.position, playerPos));
    }

    private void DropPrincess()
    {
        m_Scream.Play();
        m_FireBallTimer = 2f;
        Princess princess = GameManager.m_Instance.GetObjectPool.GetPrincess();
        Vector2 dropPosHigh = new Vector2(Random.Range(-2f, 2f), transform.position.y);
        Vector2 dropPos = new Vector2(dropPosHigh.x, dropPosHigh.y - 5f);
        GameManager.m_Instance.PrincessFalling(dropPosHigh);
        princess.Activate(dropPosHigh, GameManager.m_Instance.GetBulletDirection(dropPosHigh, dropPos));

        for (int i = 0; i < 12; i++)
        {
            Fireball fireBall = GameManager.m_Instance.GetObjectPool.GetFireBall();
            if (i < 6)
            {
                Vector2 startpos = dropPosHigh;
                startpos.x -= 1.5f;
                Vector2 pos = new Vector2(startpos.x - (1.2f * i), princess.transform.position.y);
                fireBall.Activate(pos, GameManager.m_Instance.GetBulletDirection(pos, new Vector2(pos.x, pos.y - 1)));
            }
            else
            {
                Vector2 startpos = dropPosHigh;
                startpos.x -= 5.5f;
                Vector2 pos = new Vector2(startpos.x + (1.2f * i), princess.transform.position.y);
                fireBall.Activate(pos, GameManager.m_Instance.GetBulletDirection(pos, new Vector2(pos.x, pos.y - 1)));
            }
        }
    }

    private void MoveHead()
    {
        if (m_IsInPosition)
        {
            Vector3 pos = m_HeadParentObject.transform.position;
            m_HeadMoveTowardsPosition = new Vector2(
                Mathf.Clamp(m_Player.transform.position.x - 0.5f + Random.Range(-m_OffSet, m_OffSet), -10, 0f),
                pos.y + Random.Range(-m_OffSet, m_OffSet));
        }
    }

    private void RotateHead()
    {
        Vector3 moveDirectionHead = m_Player.transform.position - m_Head.transform.position;
        if (moveDirectionHead != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirectionHead.y, moveDirectionHead.x) * Mathf.Rad2Deg;
            m_Head.transform.rotation = Quaternion.AngleAxis(angle + 70f, Vector3.forward);
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
