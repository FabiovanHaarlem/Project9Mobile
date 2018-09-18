using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public ObjectPool m_ObjectPool;
    public static GameManager m_Instance;

    

    private void Awake()
    {
        m_Instance = this;
        //m_ObjectPool.GetComponent<ObjectPool>();
    }

    //public ObjectPool GetObjectPool
    //{ get { return m_ObjectPool; } }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public Vector2 GetBulletDirection(Vector2 startingPosition, Vector2 targetPosition)
    {
        Vector2 bulletDirection;

        bulletDirection.x = targetPosition.x - startingPosition.x;
        bulletDirection.y = targetPosition.y - startingPosition.y;

        return bulletDirection.normalized;
    }

    public Vector2 GetMousePosition()
    {
        RaycastHit hit;

        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        Physics.Raycast(ray, out hit, Mathf.Infinity);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);
        Debug.Log(hit.collider.name);
        return hit.point;
    }
}
