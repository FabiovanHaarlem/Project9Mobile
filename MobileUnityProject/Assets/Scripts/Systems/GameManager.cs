using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;

    public ScoreSystem GetScoreSystem { get; private set; }
    public ObjectPool GetObjectPool { get; private set; }

    [SerializeField]
    private RectTransform m_UI;
    [SerializeField]
    private RectTransform m_UIPosition;
    [SerializeField]
    private RectTransform m_UIStartPosition;

    private void Awake()
    {
        m_UI.transform.position = m_UIStartPosition.position;
        m_Instance = this;
        GetObjectPool = GetComponent<ObjectPool>();
        GetScoreSystem = GetComponent<ScoreSystem>();
    }

    private void Update()
    {
        if (Vector2.Distance(m_UI.transform.position, m_UIPosition.transform.position) > 0.001f)
        {
            m_UI.position = Vector2.MoveTowards(m_UI.transform.position, m_UIPosition.transform.position, 0.99f);
        }

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
