using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private List<Text> m_TextOnScales;
    [SerializeField]
    private List<int> m_Values;

    [SerializeField]
    private Text m_ScoreText;

    [SerializeField]
    private Image m_LifeGainedIcon;
    [SerializeField]
    private List<Sprite> m_LifeGainedIcons;
    private Color m_Resetvalue;

    [SerializeField]
    private Image m_LifeLostIcon;
    private Color m_ResetvalueLostIcon;

    [SerializeField]
    private List<Image> m_ScoreScales;
    [SerializeField]
    private Sprite m_GoldScale;
    [SerializeField]
    private Sprite m_JadeScale;

    [SerializeField]
    private List<RectTransform> m_Princesses;
    private List<Image> m_PrincessImages;
    [SerializeField]
    private List<Sprite> m_CrownProgress;
    [SerializeField]
    private AudioSource m_LoseLife;
    [SerializeField]
    private AudioSource m_PrincessCaught;
    [SerializeField]
    private AudioSource m_CrownGainded;

    private int m_PrincessesLeft;
    private int m_Score;
    private int m_PointsStillToAdd;

    private int m_ProgressToNewCrown;

    private int m_ScaleToColor;
    private bool m_ColorScalesGold;

    private void Awake()
    {
        m_ColorScalesGold = true;
        m_ScaleToColor = 0;
        m_ProgressToNewCrown = 0;
        m_PointsStillToAdd = 0;
        m_PrincessesLeft = 2;
        m_Resetvalue = m_LifeGainedIcon.color;
        m_ScoreText.text = m_Score.ToString();
        m_PrincessImages = new List<Image>();
        m_Values = new List<int>();
        for (int i = 0; i < m_TextOnScales.Count; i++)
        {
            m_Values.Add(0);
            m_TextOnScales[i].text = m_Values[i].ToString();
        }

        for (int i = 0; i < m_Princesses.Count; i++)
        {
            m_PrincessImages.Add(m_Princesses[i].GetComponent<Image>());
        }
    }

    public RectTransform GetCurrentLife()
    {
        if (m_PrincessesLeft >= 2)
        {
            if (!StaticDataContainer.m_MuteSound)
            {
                m_PrincessCaught.Play();
            }
            ActivateLifeIcon();
            return m_Princesses[2];
        }
        else
        {
            if (!StaticDataContainer.m_MuteSound)
            {
                m_PrincessCaught.Play();
            }
            ActivateLifeIcon();
            return m_Princesses[m_PrincessesLeft];
        }
    }

    public void AddProgressForNextCrown()
    {
        if (m_PrincessesLeft <= 1)
        {
            if (m_ProgressToNewCrown != 3)
            {

                m_Princesses[m_PrincessesLeft + 1].gameObject.SetActive(true);
                m_PrincessImages[m_PrincessesLeft + 1].sprite = m_CrownProgress[m_ProgressToNewCrown];

                m_ProgressToNewCrown++;

                if (m_ProgressToNewCrown == 3)
                {
                    m_CrownGainded.Play();
                    m_PrincessesLeft++;
                    m_ProgressToNewCrown = 0;
                }
            }
        }
    }

    public void MakeScaleNewColor()
    {
        if (m_ColorScalesGold)
        {
            m_ScoreScales[m_ScaleToColor].sprite = m_GoldScale;
            m_ScaleToColor++;
            if (m_ScaleToColor == m_ScoreScales.Count)
            {
                m_ScaleToColor = 0;
                m_ColorScalesGold = false;
            }
        }
        else if (!m_ColorScalesGold)
        {
            if (m_ScaleToColor < m_ScoreScales.Count)
            {
                m_ScoreScales[m_ScaleToColor].sprite = m_JadeScale;
                m_ScaleToColor++;
            }
        }
    }

    public void AddPoints()
    {
        m_PointsStillToAdd += 50;
    }

    public void LosePrincess()
    {
        if (!StaticDataContainer.m_MuteSound)
        {
            m_LoseLife.Play();
        }
        m_Princesses[m_PrincessesLeft].gameObject.SetActive(false);
        ActivateLifeLostIcon();
        if (m_PrincessesLeft < 2 && m_ProgressToNewCrown < 3)
        {
            m_Princesses[m_PrincessesLeft + 1].gameObject.SetActive(false);
            m_ProgressToNewCrown = 0;
        }
        m_PrincessesLeft -= 1;

        if (m_PrincessesLeft == -1)
        {
            SaveScore();
            GameManager.m_Instance.GameOver();
        }
    }

    private void AnimateLifeIcon()
    {
        Vector2 scale = m_LifeGainedIcon.transform.localScale;
        Color fadeColor = new Vector4(m_LifeGainedIcon.color.r, m_LifeGainedIcon.color.g, m_LifeGainedIcon.color.b, 0f);
        m_LifeGainedIcon.transform.localScale = new Vector2(scale.x + 2f *Time.deltaTime, scale.y + 2f * Time.deltaTime);
        m_LifeGainedIcon.color = Color.Lerp(m_LifeGainedIcon.color, fadeColor, 2.5f * Time.deltaTime);

        if (m_LifeGainedIcon.color.a < 0.05f)
        {
            m_LifeGainedIcon.color = m_Resetvalue;
            m_LifeGainedIcon.transform.localScale = new Vector2(2f, 2f);
            m_LifeGainedIcon.gameObject.SetActive(false);
        }
    }

    private void ActivateLifeIcon()
    {
        m_LifeGainedIcon.sprite = m_LifeGainedIcons[m_ProgressToNewCrown];
        m_LifeGainedIcon.gameObject.SetActive(true);
    }

    private void AnimateLifeLostIcon()
    {
        Vector2 scale = m_LifeLostIcon.transform.localScale;
        Color fadeColor = new Vector4(m_LifeLostIcon.color.r, m_LifeLostIcon.color.g, m_LifeLostIcon.color.b, 0f);
        m_LifeLostIcon.transform.localScale = new Vector2(scale.x + 2f * Time.deltaTime, scale.y + 2f * Time.deltaTime);
        m_LifeLostIcon.color = Color.Lerp(m_LifeLostIcon.color, fadeColor, 2.5f * Time.deltaTime);

        if (m_LifeLostIcon.color.a < 0.05f)
        {
            m_LifeLostIcon.color = m_Resetvalue;
            m_LifeLostIcon.transform.localScale = new Vector2(2f, 2f);
            m_LifeLostIcon.gameObject.SetActive(false);
        }
    }

    private void ActivateLifeLostIcon()
    {
        m_LifeLostIcon.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (m_LifeGainedIcon.gameObject.activeInHierarchy)
        {
            AnimateLifeIcon();
        }

        if (m_LifeLostIcon.gameObject.activeInHierarchy)
        {
            AnimateLifeLostIcon();
        }

        if (m_PointsStillToAdd != 0)
        {
            m_Score += 1;
            m_Values[0] += 1;
            m_TextOnScales[0].text = m_Values[0].ToString();
            m_PointsStillToAdd -= 1;
            for (int i = 0; i < m_Values.Count; i++)
            {
                if (m_Values[i] == 10)
                {
                    m_Values[i] = 0;
                    m_TextOnScales[i].text = m_Values[i].ToString();
                    if (i != m_Values.Count)
                    {
                        m_Values[i + 1] += 1;
                        m_TextOnScales[i + 1].text = m_Values[i + 1].ToString();
                    }
                }
            }
        }
    }  

    private void SaveScore()
    {
        StaticDataContainer.m_Score = m_Score;
    }
}
