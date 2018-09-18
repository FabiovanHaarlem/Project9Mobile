using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    private Vector3 m_Direction;

    protected float m_Speed;

    public void Initialize()
    {
        m_Speed = 3f;
        this.gameObject.SetActive(false);
    }

    public void Activate(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        m_Direction = direction;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += (m_Direction * (m_Speed * Time.deltaTime));
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void Collision()
    {
        Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision();
    }
}
