using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    private Vector3 m_Direction;

    protected float m_Speed;

    public void Initialize()
    {
        m_Speed = 5f;
        this.gameObject.SetActive(false);
    }

    public virtual void Activate(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        m_Direction = direction;
        this.gameObject.SetActive(true);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        
    }

    public virtual void Update()
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

    public virtual void Collision(GameObject hitObject)
    {
        Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision.gameObject);
    }
}
