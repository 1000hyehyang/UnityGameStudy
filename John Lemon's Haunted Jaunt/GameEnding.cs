using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    float maxTime = 120f;

    float timeLeft;

    [SerializeField]
    Text text;

    [SerializeField]
    Image timeBar;

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;

    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    public CanvasGroup timeOverBackgroundImageCanvasGroup;

    public CanvasGroup mainMenuCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    bool m_IsGameEnded = false;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        text.gameObject.SetActive(true);
        timeBar.gameObject.SetActive(true);
        timeLeft = maxTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        if (!m_IsPlayerAtExit && !m_IsPlayerCaught)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timeBar.fillAmount = timeLeft / maxTime;
            }
            else
            {
                Time.timeScale = 0;
                EndLevel(timeOverBackgroundImageCanvasGroup, true, caughtAudio);
            }
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        text.text = "Timer:  " + minutes.ToString("00") + "  :  " + seconds.ToString("00");

        if (m_IsPlayerAtExit)
        {
            if (!m_IsGameEnded) 
            {
                gameManager.UpdateBestTime(maxTime - timeLeft); 
                m_IsGameEnded = true; 
            }
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasCroup, bool doRestart, AudioSource audioSource)
    {
        text.gameObject.SetActive(false);
        timeBar.gameObject.SetActive(false);
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        imageCanvasCroup.alpha = m_Timer / fadeDuration;
        mainMenuCanvasGroup.alpha = m_Timer / fadeDuration;

    }
}
