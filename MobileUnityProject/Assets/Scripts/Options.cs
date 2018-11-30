using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private Sprite m_ScaleOn;
    [SerializeField]
    private Sprite m_ScaleOff;
    [SerializeField]
    private Image m_Scale;

    [SerializeField]
    private GameObject m_DisabledPos;
    [SerializeField]
    private GameObject m_EnabledPos;

    private GameObject m_CurrentTarget;

    private bool m_ToggleMute;

    private void Awake()
    {
        m_ToggleMute = StaticDataContainer.m_MuteSound;
        if (m_ToggleMute)
        {
            m_Scale.sprite = m_ScaleOn;
        }
        else if (!m_ToggleMute)
        {
            m_Scale.sprite = m_ScaleOff;
        }
        Disable();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_CurrentTarget.transform.position, 2f * Time.deltaTime);
    }

    public void Disable()
    {
        m_CurrentTarget = m_DisabledPos;
    }

    public void Enable()
    {
        m_CurrentTarget = m_EnabledPos;
    }

    public void MuteSound(bool mute)
    {
        if (!m_ToggleMute)
        {
            m_Scale.sprite = m_ScaleOn;
            m_ToggleMute = true;
        }
        else if (m_ToggleMute)
        {
            m_Scale.sprite = m_ScaleOff;
            m_ToggleMute = false;
        }
        StaticDataContainer.m_MuteSound = m_ToggleMute;
        StaticDataContainer.m_AudioManager.CheckMute(m_ToggleMute);
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
