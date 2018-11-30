using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;

    public ScoreSystem GetScoreSystem { get; private set; }
    public ObjectPool GetObjectPool { get; private set; }
    public EventsSystem GetEventSystem { get; private set; }
    private DateChecker m_DateChecker;

    [SerializeField]
    private RectTransform m_UI;
    [SerializeField]
    private RectTransform m_UIPosition;
    [SerializeField]
    private RectTransform m_UIStartPosition;
    [SerializeField]
    private RectTransform m_PrincessIcon;
    [SerializeField]
    private RectTransform m_Canvas;
    [SerializeField]
    private Camera m_Camera;

    private Vector2 m_PrincessIconStartPosition;
    private Vector2 m_PrincessGoToPosition;

    private bool m_MoveToCrown;

    private void Awake()
    {
        m_Canvas.gameObject.SetActive(true);
        m_UI.transform.position = m_UIStartPosition.position;
        m_PrincessIconStartPosition = m_PrincessIcon.position;
        m_PrincessGoToPosition = m_PrincessIconStartPosition;
        m_Instance = this;
        m_MoveToCrown = false;
        GetObjectPool = GetComponent<ObjectPool>();
        GetScoreSystem = GetComponent<ScoreSystem>();
        GetEventSystem = GetComponent<EventsSystem>();
        m_DateChecker = GetComponent<DateChecker>();
        GetEventSystem.m_PrincessCaughtEvent += PrincessCaught;
    }

    private void Start()
    {
        CheckIfChristmas();
    }

    private void Update()
    {
        if (Vector2.Distance(m_UI.transform.position, m_UIPosition.transform.position) > 0.001f)
        {
            m_UI.position = Vector2.MoveTowards(m_UI.transform.position, m_UIPosition.transform.position, 5f);
        }

        if (m_MoveToCrown)
        {
            if (Vector2.Distance(m_PrincessIcon.position, m_PrincessGoToPosition) > 10f)
            {
                m_PrincessIcon.position = Vector2.Lerp(m_PrincessIcon.position, m_PrincessGoToPosition, 0.05f);
            }
            else
            {
                GetScoreSystem.AddProgressForNextCrown();
                PrincessIconDisable();
            }
        }
        else
        {
            if (Vector2.Distance(m_PrincessIcon.position, m_PrincessGoToPosition) > 0.01f)
            {
                m_PrincessIcon.position = Vector2.Lerp(m_PrincessIcon.position, m_PrincessGoToPosition, 0.05f);
            }
        }
        

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            StaticDataContainer.m_EasterEgg = false;
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

    public void PrincessFalling(Vector2 pos)
    {
        Vector2 posInScreenSpace = m_Camera.WorldToScreenPoint(pos);
        m_PrincessGoToPosition = new Vector2(posInScreenSpace.x, m_PrincessIconStartPosition.y);
        m_PrincessIcon.gameObject.SetActive(true);
    }

    public void PrincessCaught()
    {
        m_MoveToCrown = true;
        m_PrincessGoToPosition = GetScoreSystem.GetCurrentLife().position;
    }

    public void PrincessIconDisable()
    {
        m_PrincessIcon.position = m_PrincessIconStartPosition;
        m_PrincessIcon.gameObject.SetActive(false);
        m_MoveToCrown = false;
        m_PrincessGoToPosition = m_PrincessIconStartPosition;
    }

    public void HitByFireBall()
    {
        GetScoreSystem.LosePrincess();
    }

    public void GameOver()
    {
        StaticDataContainer.m_EasterEgg = false;
        SceneManager.LoadScene("EndScreen");
    }

    private void CheckIfChristmas()
    {
        if (StaticDataContainer.m_Holiday == Holidays.Christmass)
        {
            GetEventSystem.Christmass();
        }
    }

    private void CheckDate()
    {
        int date = DateTime.Today.Month;
    }
}
