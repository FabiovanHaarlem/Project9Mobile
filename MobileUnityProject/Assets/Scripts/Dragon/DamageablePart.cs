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

    private BodyPart m_BodyPart;

    private float m_NonDamageableTimer;
    private float m_SwitchSpriteTimer;

    private bool m_Damageable;

    public void Initialize(Sprite damageableSprite, Sprite nonDamageableSprite, BodyPart bodyPart)
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_DamageableSprite = damageableSprite;
        m_NonDamageableSprite = nonDamageableSprite;
        m_DamageableLayer = 16;
        m_NonDamageableLayer = 17;
        m_SwitchSpriteTimer = 0.1f;
        m_Damageable = false;
        this.gameObject.layer = m_NonDamageableLayer;
        m_BodyPart = bodyPart;
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

        if (m_Damageable)
        {
            m_SwitchSpriteTimer -= Time.deltaTime;
            if (m_SwitchSpriteTimer <= 0)
            {
                if (m_SpriteRenderer.sprite == m_NonDamageableSprite)
                {
                    m_SpriteRenderer.sprite = m_DamageableSprite;
                }
                else
                {
                    m_SpriteRenderer.sprite = m_NonDamageableSprite;
                }

                m_SwitchSpriteTimer = 0.1f;
            }
        }
    }

    public void ChangeLayer(int layer)
    {
        m_SpriteRenderer.sortingOrder = layer;
    }

    public void MakeDamageable()
    {
        m_SpriteRenderer.sprite = m_DamageableSprite;
        m_Damageable = true;
        m_NonDamageableTimer = Random.Range(4f, 7f);
        this.gameObject.layer = m_DamageableLayer;
    }

    public void MakeNonDamageable()
    {
        m_Damageable = false;
        m_SpriteRenderer.sprite = m_NonDamageableSprite;
        this.gameObject.layer = m_NonDamageableLayer;
    }

    private void Hit(GameObject sword)
    {
        MakeNonDamageable();
        GameManager.m_Instance.GetScoreSystem.AddPoints();
        m_BodyPart.PartHit();
        GameObject scale = GameManager.m_Instance.GetObjectPool.GetScale();
        scale.transform.position = transform.position;
        scale.transform.rotation = transform.rotation;
        scale.SetActive(true);
        scale.GetComponent<Rigidbody2D>().AddForce(transform.position - sword.transform.position * 200);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_Damageable)
        {
            Hit(collision.gameObject);
        }
    }
}
