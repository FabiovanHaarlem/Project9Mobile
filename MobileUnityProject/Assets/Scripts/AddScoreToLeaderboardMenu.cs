using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddScoreToLeaderboardMenu : MonoBehaviour
{
    [SerializeField]
    private InputField m_InputField;
    [SerializeField]
    private Text m_InputFieldText;
    [SerializeField]
    private Text m_ScoreText;
    [SerializeField]
    private GameObject m_UpperText;
    [SerializeField]
    private GameObject m_Button;
    [SerializeField]
    private GameObject m_Layout;
    [SerializeField]
    private GameObject m_DelayLayer;

    private API m_API;

    private float m_DelayTimer;
    private bool m_NameChanged;

    private void Awake()
    {
        m_API = GetComponent<API>();
        m_NameChanged = false;
        m_DelayTimer = 1f;
    }

    private void Start()
    {
        m_InputFieldText.text = "Name";
        m_ScoreText.text = StaticDataContainer.m_Score.ToString();
    }

    private void Update()
    {
        if (m_DelayLayer.activeInHierarchy)
        {
            m_DelayTimer -= Time.deltaTime;
            if (m_DelayTimer <= 0f)
            {
                m_DelayLayer.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }


    public void NameChanged()
    {
        m_NameChanged = true;
    }

    public void SendDataToDatabase()
    {
        if (!m_NameChanged || m_InputField.text == "Enter a name" || m_InputField.text == "Please enter name")
        {
            m_InputFieldText.text = "Please enter name";
            m_NameChanged = false;
        }
        else
        {
            m_InputField.gameObject.SetActive(false);
            m_Button.SetActive(false);
            m_UpperText.SetActive(false);
            m_Layout.SetActive(false);
            m_API.SendScoreToDatabase(m_InputField.text, StaticDataContainer.m_Score, this);
        }
    }

    public void ReactToServerResponse(ScoreAddedResponse scoreAdded)
    {
        if (scoreAdded.GetIfScoreAdded())
        {
            m_ScoreText.text = "Score added to top 5";
        }
        else
        {
            m_ScoreText.text = "Score to low, not added to top 5";
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
