using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<Fireball> m_FireBalls;
    private List<Princess> m_Princess;
    private List<Sword> m_Swords;
    private List<GameObject> m_BrokenScales;

    private void Awake()
    {
        m_FireBalls = new List<Fireball>();
        m_Princess = new List<Princess>();
        m_Swords = new List<Sword>();
        m_BrokenScales = new List<GameObject>();

        for (int i = 0; i < 6; i++)
        {
            GameObject fireBall = Instantiate(Resources.Load("Prefabs/FireBall") as GameObject);
            Fireball fireBallScript = fireBall.GetComponent<Fireball>();
            fireBallScript.Initialize();
            m_FireBalls.Add(fireBallScript);
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject princess = Instantiate(Resources.Load("Prefabs/Princess") as GameObject);
            Princess princesScript = princess.GetComponent<Princess>();
            princesScript.Initialize();
            m_Princess.Add(princesScript);
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject sword = Instantiate(Resources.Load("Prefabs/Sword") as GameObject);
            Sword swordScript = sword.GetComponent<Sword>();
            swordScript.Initialize();
            m_Swords.Add(swordScript);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject scale = Instantiate(Resources.Load("Prefabs/BrokenScale") as GameObject);
            scale.SetActive(false);
            m_BrokenScales.Add(scale);
        }
    }

    public Fireball GetFireBall()
    {
        Fireball fireBall = m_FireBalls[0];

        for (int i = 0; i < m_FireBalls.Count; i++)
        {
            if (!m_FireBalls[i].gameObject.activeInHierarchy)
            {
                fireBall = m_FireBalls[i];
                break;
            }
        }

        return fireBall;
    }

    public Princess GetPrincess()
    {
        Princess princess = m_Princess[0];

        for (int i = 0; i < m_Princess.Count; i++)
        {
            if (!m_Princess[i].gameObject.activeInHierarchy)
            {
                princess = m_Princess[i];
                break;
            }
        }

        return princess;
    }

    public Sword GetSword()
    {
        Sword sword = m_Swords[0];

        for (int i = 0; i < m_Swords.Count; i++)
        {
            if (!m_Swords[i].gameObject.activeInHierarchy)
            {
                sword = m_Swords[i];
                break;
            }
        }

        return sword;
    }


    public GameObject GetScale()
    {
        GameObject scale = m_BrokenScales[0];

        for (int i = 0; i < m_BrokenScales.Count; i++)
        {
            if (!m_BrokenScales[i].gameObject.activeInHierarchy)
            {
                scale = m_BrokenScales[i];
                break;
            }
        }

        return scale;
    }
}
