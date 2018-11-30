using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenScale : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private Color m_DefaultColor;
    private Vector3 m_DefaultScale;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_DefaultColor = m_SpriteRenderer.color;
        m_DefaultScale = transform.localScale; ;
    }

    private void Update()
    {
        AnimateIcon();
    }

    private void AnimateIcon()
    {
        Vector2 scale = transform.localScale;
        Color fadeColor = new Vector4(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 0f);
        transform.localScale = new Vector2(scale.x - 0.1f * Time.deltaTime, scale.y - 0.1f * Time.deltaTime);
        m_SpriteRenderer.color = Color.Lerp(m_SpriteRenderer.color, fadeColor, 0.9f * Time.deltaTime);

        if (m_SpriteRenderer.color.a < 0.05f)
        {
            m_SpriteRenderer.color = m_DefaultColor;
            transform.localScale = m_DefaultScale;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DisableCollider"))
        {
            m_SpriteRenderer.color = m_DefaultColor;
            transform.localScale = m_DefaultScale;
            gameObject.SetActive(false);
        }
    }
}
