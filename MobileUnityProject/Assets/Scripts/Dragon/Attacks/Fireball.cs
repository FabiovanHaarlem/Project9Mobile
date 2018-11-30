using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : FallingObjects
{
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private Sprite m_ChristmassSprite;

    public override void Activate(Vector2 position, Vector2 direction)
    {
        base.Activate(position, direction);
        transform.localScale = new Vector3(0.13f, 0.13f, 1f);
    }

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.m_Instance.GetEventSystem.m_ChristmassEvent += ActivateChristmassSprite;
    }

    public override void Update()
    {
        base.Update();
        Grow();
    }

    private void Grow()
    {
        if (transform.localScale.x < 0.22f)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * 0.2f, transform.localScale.y + Time.deltaTime * 0.2f, 1f);
        }
    }

    public override void Collision(GameObject hitObject)
    {
        base.Collision(hitObject);
    }

    private void ActivateChristmassSprite()
    {
        GetComponent<Animator>().enabled = false;
        m_SpriteRenderer.sprite = m_ChristmassSprite;
    }
}
