using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_AudioClips;

    [SerializeField]
    private AudioSource m_IngameMusic;
    [SerializeField]
    private AudioSource m_MenuMusic;

    private bool m_MainMenuMusicPlayed;
    private bool m_IngameMusicPlayed;

    private void Start()
    {
        if (StaticDataContainer.m_AudioManager == null)
        {
            CheckScene(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
            SceneManager.activeSceneChanged += CheckScene;
            //m_MenuMusic = GetComponent<AudioSource>();
            //m_MenuMusic.clip = m_AudioClips[Random.Range(0, m_AudioClips.Count)];
            DontDestroyOnLoad(this.gameObject);
            StaticDataContainer.m_AudioManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CheckMute(bool mute)
    {
        if (mute)
        {
            m_MenuMusic.Pause();
        }
        else if (!mute)
        {
            m_MenuMusic.UnPause();
        }
    }

    private void CheckScene(Scene scene, Scene next)
    {
        if (!StaticDataContainer.m_MuteSound)
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                if (!m_IngameMusicPlayed)
                {
                    m_IngameMusic.Play();
                    m_MenuMusic.Pause();
                    m_IngameMusicPlayed = true;
                }
                else
                {
                    m_MenuMusic.Pause();
                    m_IngameMusic.UnPause();
                }
            }
            else
            {
                if (!m_MainMenuMusicPlayed)
                {
                    m_MenuMusic.Play();
                    m_MainMenuMusicPlayed = true;
                }
                else
                {
                    m_IngameMusic.Pause();
                    m_MenuMusic.UnPause();
                }
            }
        }
    }


}
