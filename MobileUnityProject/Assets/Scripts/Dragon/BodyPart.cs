using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private List<DamageablePart> m_DamageableParts;

    [SerializeField]
    private Transform[] m_MoveToPositions;

    private int m_Direction;

    private bool m_MoveLeft;

    public void Initialize(Sprite damageableSprite, Sprite nonDamageableSprite)
    {
        for (int i = 0; i < m_DamageableParts.Count; i++)
        {
            m_DamageableParts[i].Initialize(damageableSprite, nonDamageableSprite);
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
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(Mathf.SmoothStep(transform.position.x, m_MoveToPositions[m_Direction].position.x, 2f), Mathf.SmoothStep(transform.position.y, m_MoveToPositions[m_Direction].position.y, 2f)), 0.01f);
        //Vector2 velocity = m_MoveToPositions[m_Direction].position;
        //transform.position = Vector2.Lerp(transform.position, m_MoveToPositions[m_Direction].position, 0.03f);
        //transform.position = Vector2.SmoothDamp(transform.position, m_MoveToPositions[m_Direction].position, ref velocity, 0.8f);
        transform.position = Vector2.MoveTowards(transform.position, m_MoveToPositions[m_Direction].position, 0.01f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_MoveToPositions[m_Direction].rotation, 0.2f);
        
        if (Vector3.Distance(transform.position, m_MoveToPositions[m_Direction].position) < 0.3f)
        {
            MoveBodyPart();
        }
    }

    public void PartHit()
    {

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
