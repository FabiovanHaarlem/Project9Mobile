using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : FallingObjects
{
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private Sprite m_ChristmassSprite;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.m_Instance.GetEventSystem.m_ChristmassEvent += ActivateChristmassSprite;
    }

    public override void Collision(GameObject hitObject)
    {
        base.Collision(hitObject);

        if (hitObject.CompareTag("Ground"))
        {
            LosePrincess();
        }
    }

    private void LosePrincess()
    {
        GameManager.m_Instance.GetScoreSystem.LosePrincess();
        GameManager.m_Instance.PrincessIconDisable();
    }
    private void ActivateChristmassSprite()
    {
        m_SpriteRenderer.sprite = m_ChristmassSprite;
    }
}
