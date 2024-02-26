using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    public CanvasGroup recordCanvasGroup;
    public Text recordText;
    public Text bestRecordText;

    private const int maxRecordsToShow = 10;

    private List<float> allTimes = new List<float>();
    private List<float> sortedTimes = new List<float>();

    void Start()
    {
        recordCanvasGroup.alpha = 0;
        LoadAllTimes();
    }

    public void OnClickBtn()
    {
        recordCanvasGroup.alpha = 1;
    }

    public void OnExitBtn()
    {
        recordCanvasGroup.alpha = 0;
    }

    public void LoadAllTimes()
    {
        string timesString = PlayerPrefs.GetString("AllTimes", "");

        // 시간 문자열이 비어 있지 않으면 처리
        if (!string.IsNullOrEmpty(timesString))
        {
            // 시간 문자열을 쉼표로 분할
            string[] timeStrings = timesString.Split(',');

            // 유효한 시간을 allTimes 리스트에 추가
            allTimes.AddRange(timeStrings
                .Select(timeStr => float.TryParse(timeStr, out float time) ? time : 0)
                .Where(time => time > 0));
        }

        // 중복된 시간을 제거하고 정렬
        allTimes = allTimes.Distinct().OrderBy(time => time).ToList();

        sortedTimes = allTimes.Take(Mathf.Min(allTimes.Count, maxRecordsToShow)).ToList();

        UpdateRecordText();
    }

    private void UpdateRecordText()
    {
        float bestRecord = PlayerPrefs.GetFloat("BestTime", 0f);
        string bestRecordString = FormatTime(bestRecord);

        string recordString = "[Escape Records]\n";

        for (int i = 0; i < sortedTimes.Count; i++)
        {
            recordString += (i + 1) + ". " + FormatTime(sortedTimes[i]) + "\n";
        }

        recordText.text = recordString;
        bestRecordText.text = bestRecordString;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}