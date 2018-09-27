using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField]
    private Sprite m_DamageableSprite;
    [SerializeField]
    private Sprite m_NonDamageableSprite;

    [SerializeField]
    private DragonHead m_DragonHead;
    [SerializeField]
    private List<BodyPart> m_BodyParts;

    [SerializeField]
    private List<Transform> m_DragonPartsPositions;
    [SerializeField]
    private Transform m_DragonPosition;
    [SerializeField]
    private Transform m_DragonStartPosition;

    private int m_ActiveBodyPart;

    private float m_MakeDamageablePartTimer;

    private void Awake()
    {
        transform.position = m_DragonStartPosition.position;
        InitializeDragon();
        m_ActiveBodyPart = 2;
        m_BodyParts[m_ActiveBodyPart].Initialize(m_DamageableSprite, m_NonDamageableSprite, true);
    }


    private void InitializeDragon()
    {
        m_DragonHead.Initialize();
        for (int i = 0; i < m_BodyParts.Count - 1; i++)
        {
            m_BodyParts[i].Initialize(m_DamageableSprite, m_NonDamageableSprite, false);
        }
        m_MakeDamageablePartTimer = Random.Range(3f, 6f);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, m_DragonPosition.position) > 0.001f)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_DragonPosition.position, 0.03f);
        }
        else
        {
            m_DragonHead.InPosition();
        }

        m_MakeDamageablePartTimer -= Time.deltaTime;
        if (m_MakeDamageablePartTimer <= 0f)
        {
            if (m_BodyParts[m_ActiveBodyPart].GetIfPartDamageable())
            {
                m_BodyParts[m_ActiveBodyPart].MakePartDamageable();
                m_MakeDamageablePartTimer = Random.Range(3f, 6f);
            }
            else
            {
                if (m_ActiveBodyPart == 0)
                {
                    m_ActiveBodyPart = 2;
                }
                else
                {
                    m_ActiveBodyPart -= 1;  
                }
                m_BodyParts[m_ActiveBodyPart].MakeActiveBodyPart();
                ChangeDragonPartsPosition();
            }
        }
    }

    private void ChangeDragonPartsPosition()
    {
        switch(m_ActiveBodyPart)
        {
            case 0:
                m_BodyParts[0].ChangeParentPosition(m_DragonPartsPositions[2], -6, -5);
                m_BodyParts[1].ChangeParentPosition(m_DragonPartsPositions[0], -10, -9);
                m_BodyParts[2].ChangeParentPosition(m_DragonPartsPositions[1], -8, -7);
                break;
            case 1:
                m_BodyParts[0].ChangeParentPosition(m_DragonPartsPositions[1], -8, -7);
                m_BodyParts[1].ChangeParentPosition(m_DragonPartsPositions[2], -6, -5);
                m_BodyParts[2].ChangeParentPosition(m_DragonPartsPositions[0], -10, -9);
                break;
            case 2:
                m_BodyParts[0].ChangeParentPosition(m_DragonPartsPositions[0], -10, -9);
                m_BodyParts[1].ChangeParentPosition(m_DragonPartsPositions[1], -8, -7);
                m_BodyParts[2].ChangeParentPosition(m_DragonPartsPositions[2], -6, -5);
                break;
        }
    }
}
