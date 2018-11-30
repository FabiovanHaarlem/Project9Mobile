using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Image m_Background;
    [SerializeField]
    private Sprite m_ChristmassSptite;
    private Sprite m_NormaleSprite;
    [SerializeField]
    private Options m_OptionsMenu;
    [SerializeField]
    private GameObject m_DisableOptionsMenuButton;
    [SerializeField]
    private Sprite m_OffScale;
    [SerializeField]
    private Sprite m_OnScale;
    [SerializeField]
    private Image m_ToggleChrismassScale;

    private void Start()
    {
        m_NormaleSprite = m_Background.sprite;
        SwitchToChristmass();
    }

    public void EnableAndDisableChristmass()
    {

        if (StaticDataContainer.m_Holiday == Holidays.Normale)
        {
            StaticDataContainer.m_Holiday = Holidays.Christmass;
        }
        else if (StaticDataContainer.m_Holiday == Holidays.Christmass)
        {
            StaticDataContainer.m_Holiday = Holidays.Normale;
        }

        SwitchToChristmass();
    }


    public void SwitchToChristmass()
    {
        if (StaticDataContainer.m_Holiday == Holidays.Christmass)
        {
            m_Background.sprite = m_ChristmassSptite;
            m_ToggleChrismassScale.sprite = m_OnScale;
        }
        else if (StaticDataContainer.m_Holiday == Holidays.Normale)
        {
            m_Background.sprite = m_NormaleSprite;
            m_ToggleChrismassScale.sprite = m_OffScale;
        }
    }

    public void DisableOptionsMenu()
    {
        m_OptionsMenu.Disable();
        m_DisableOptionsMenuButton.SetActive(false);
    }

    public void EnableOptionsMenu()
    {
        m_OptionsMenu.Enable();
        m_DisableOptionsMenuButton.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void EasterEgg()
    {
        StaticDataContainer.m_EasterEgg = true;
    }

    public void GoToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
