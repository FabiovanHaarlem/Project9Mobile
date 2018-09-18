using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public void MoveRight()
    {
        if (transform.position.x <= 2.5f)
        {
            Vector2 pos = transform.position;
            transform.position = new Vector3(pos.x + (2 * Time.deltaTime), pos.y);
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x >= -2.5f)
        {
            Vector2 pos = transform.position;
            transform.position = new Vector3(pos.x - (2 * Time.deltaTime), pos.y);
        }
    }

    public void ThrowSword()
    {
        Sword sword = GameManager.m_Instance.m_ObjectPool.GetSword();
        Vector2 direction = GameManager.m_Instance.GetBulletDirection(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        sword.Activate(transform.position, direction);
    }
}
