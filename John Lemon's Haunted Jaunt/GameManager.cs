using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Book 관련 로직
    private int charm = 0;

    public GameObject player;

    public GameObject optionMenu;

    public CinemachineVirtualCamera virtualCamera;
    public GameObject redGhost;

    // Time 관련 로직
    private float bestTime = Mathf.Infinity;

    // 모든 기록 불러오기
    private List<float> allTimes = new List<float>();

    void Start()
    {
        optionMenu.SetActive(false);
        charm = PlayerPrefs.GetInt("Charm", 0);
        LoadBestTime();
        LoadAllTimes();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }
    }

    public void ToggleOptionMenu()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);

        Time.timeScale = optionMenu.activeSelf ? 0 : 1;
    }

    public void IncreaseCharm()
    {
        charm++;

        if (charm >= 4)
        {
            StartCoroutine(SwitchToRedGhostCamera());
        }
        PlayerPrefs.SetInt("Charm", charm); // charm 값 저장
        PlayerPrefs.Save();
    }

    IEnumerator SwitchToRedGhostCamera()
    {
        Vector3 currentRotation = virtualCamera.transform.rotation.eulerAngles;

        currentRotation.x = 72f;

        virtualCamera.transform.rotation = Quaternion.Euler(currentRotation);

        virtualCamera.Follow = redGhost.transform;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 10;

        // 2초 후에 RedGhost를 파괴하고 카메라를 다시 JohnLemon으로 이동시키는 로직
        yield return new WaitForSeconds(2f);

        Destroy(redGhost);
        yield return new WaitForSeconds(2f);

        currentRotation.x = 45f;
        virtualCamera.transform.rotation = Quaternion.Euler(currentRotation);
        virtualCamera.Follow = player.transform;
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 8;

        Destroy(gameObject);
    }

    private void LoadAllTimes()
    {
        string timesString = PlayerPrefs.GetString("AllTimes", "");
        if (!string.IsNullOrEmpty(timesString))
        {
            string[] timeStrings = timesString.Split(',');
            foreach (string timeStr in timeStrings)
            {
                float time;
                if (float.TryParse(timeStr, out time))
                {
                    allTimes.Add(time);
                }
            }
        }
    }

    private void SaveTimeToList(float currentTime)
    {
        allTimes.Add(currentTime);
        string timesString = string.Join(",", allTimes); // 리스트를 쉼표로 구분된 하나의 문자열로 변환
        PlayerPrefs.SetString("AllTimes", timesString);
        PlayerPrefs.Save();
    }

    private void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
    }

    public void UpdateBestTime(float currentTime)
    {
        SaveTimeToList(currentTime);

        if (currentTime < bestTime)
        {
            bestTime = currentTime; // 현재 시간이 최고 기록보다 작으면 최고 기록을 업데이트
            PlayerPrefs.SetFloat("BestTime", bestTime); // 최고 기록 저장
            PlayerPrefs.Save();
        }
    }
}