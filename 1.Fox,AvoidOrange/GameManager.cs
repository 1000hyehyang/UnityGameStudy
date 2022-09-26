using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject orange;

    private int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Transform objbox;

    [SerializeField]
    private Text bestScore;

    [SerializeField]
    private GameObject panel;

    void Start()
    {
        Screen.SetResolution(768, 1024, false);
    }

    void Update()
    {
        
    }

    public bool stopTrigger = true;
    public void GameOver()
    {
        stopTrigger = false;
        StopCoroutine(CreateorangeRoutine());

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        panel.SetActive(true);
    }

    public void GameStart()
    {
        score = 0;
        scoreTxt.text = "Score : " + score;
        stopTrigger = true;
        StartCoroutine(CreateorangeRoutine());
        panel.SetActive(false);
    }
    public void Score()
    {
        if(stopTrigger)
        score++;
        scoreTxt.text = "Score : " + score;
    }

    IEnumerator CreateorangeRoutine()
    {
        while(stopTrigger)
        {
            CreateOrange();
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void CreateOrange()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(orange, pos, Quaternion.identity);
    }
}
