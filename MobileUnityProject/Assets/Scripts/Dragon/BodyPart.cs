using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private List<DamageablePart> m_DamageableParts;
    [SerializeField]
    private List<SpriteRenderer> m_DecoScales;

    [SerializeField]
    private Transform[] m_MoveToPositions;

    private Transform m_CurrentParentPosition;
    [SerializeField]
    private GameObject m_ParentObject;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    private int m_ScalesLeft;
    private int m_Direction;
    private int m_ScalesLayer;
    private int m_BodyPartLayer;
    private bool m_MoveLeft;
    private bool m_PartStillDamageable;

    public void Initialize(Sprite damageableSprite, Sprite nonDamageableSprite, bool isDamageable)
    {
        m_ScalesLeft = m_DamageableParts.Count;
        m_PartStillDamageable = isDamageable;
        m_CurrentParentPosition = m_ParentObject.transform;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < m_DamageableParts.Count; i++)
        {
            m_DamageableParts[i].Initialize(damageableSprite, nonDamageableSprite, this);
        }

        m_Direction = 1;
        if (Random.Range(0, 100) < 50)
        {
            m_MoveLeft = false;
        }
        else
        {
            m_MoveLeft = true;
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_MoveToPositions[m_Direction].position, 0.001f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_MoveToPositions[m_Direction].rotation, 0.001f);
        
        if (Vector3.Distance(transform.position, m_MoveToPositions[m_Direction].position) < 0.3f)
        {
            MoveBodyPart();
        }

        if (Vector3.Distance(m_ParentObject.transform.position, m_CurrentParentPosition.position) > 0.001f)
        {
            m_ParentObject.transform.position = Vector2.MoveTowards(m_ParentObject.transform.position, m_CurrentParentPosition.position, 0.004f);
        }
    }

    public void ChangeParentPosition(Transform position, int bodyLayer, int scalesLayer)
    {
        m_CurrentParentPosition = position;
        m_BodyPartLayer = bodyLayer;
        m_ScalesLayer = scalesLayer;
        m_SpriteRenderer.sortingOrder = m_BodyPartLayer;
        for (int i = 0; i < m_DamageableParts.Count; i++)
        {
            m_DamageableParts[i].ChangeLayer(m_ScalesLayer);
        }

        for (int i = 0; i < m_DecoScales.Count; i++)
        {
            m_DecoScales[i].sortingOrder = m_ScalesLayer;
        }
    }

    public void MakeActiveBodyPart()
    {
        m_PartStillDamageable = true;
        m_ScalesLeft = m_DamageableParts.Count;
    }

    public bool GetIfPartDamageable()
    {
        return m_PartStillDamageable;
    }

    public void PartHit()
    {
        m_ScalesLeft -= 1;
        if (m_ScalesLeft == 0)
        {
            m_PartStillDamageable = false;
        }
    }

    public void MakePartDamageable()
    {
        m_DamageableParts[Random.Range(0, m_DamageableParts.Count)].MakeDamageable();
    }

    private void MoveBodyPart()
    {
        if (m_MoveLeft)
        {
            m_Direction -= 1;
            if (m_Direction == 0)
            {
                m_MoveLeft = !m_MoveLeft;
            }
        }
        else
        {
            m_Direction += 1;
            if (m_Direction == 2)
            {
                m_MoveLeft = !m_MoveLeft;
            }
        }


    }
}
