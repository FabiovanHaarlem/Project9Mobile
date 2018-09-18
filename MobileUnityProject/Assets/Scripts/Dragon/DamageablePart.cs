using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageablePart : MonoBehaviour
{
    private int m_DamageableLayer;
    private int m_NonDamageableLayer;

    private Sprite m_DamageableSprite;
    private Sprite m_NonDamageableSprite;

    private SpriteRenderer m_SpriteRenderer;

    private float m_NonDamageableTimer;

    private bool m_Damageable;

    public void Initialize(Sprite damageableSprite, Sprite nonDamageableSprite)
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_DamageableSprite = damageableSprite;
        m_NonDamageableSprite = nonDamageableSprite;
        m_DamageableLayer = 16;
        m_NonDamageableLayer = 17;
        this.gameObject.layer = m_NonDamageableLayer;
    }

    private void Update()
    {
        if (m_NonDamageableTimer > 0f)
        {
            m_NonDamageableTimer -= Time.deltaTime;
        }
        else if (m_NonDamageableTimer < 0f)
        {
            MakeNonDamageable();
        }
    }

    public void MakeDamageable()
    {
        m_SpriteRenderer.sprite = m_DamageableSprite;
        m_NonDamageableTimer = Random.Range(4f, 7f);
        this.gameObject.layer = m_DamageableLayer;
    }

    public void MakeNonDamageable()
    {
        m_SpriteRenderer.sprite = m_NonDamageableSprite;
        this.gameObject.layer = m_NonDamageableLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MakeNonDamageable();
    }
}
