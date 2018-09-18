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

    private float m_MakeDamageablePartTimer;

    private void Awake()
    {
        InitializeDragon();
    }


    private void InitializeDragon()
    {
        m_DragonHead.Initialize();
        for (int i = 0; i < m_BodyParts.Count; i++)
        {
            m_BodyParts[i].Initialize(m_DamageableSprite, m_NonDamageableSprite);
        }
        m_MakeDamageablePartTimer = Random.Range(3f, 6f);
    }

    private void Update()
    {
        m_MakeDamageablePartTimer -= Time.deltaTime;
        if (m_MakeDamageablePartTimer <= 0f)
        {
            m_BodyParts[Random.Range(0, m_BodyParts.Count)].MakePartDamageable();
            m_MakeDamageablePartTimer = Random.Range(3f, 6f);
        }
    }
}
