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

        // �ð� ���ڿ��� ��� ���� ������ ó��
        if (!string.IsNullOrEmpty(timesString))
        {
            // �ð� ���ڿ��� ��ǥ�� ����
            string[] timeStrings = timesString.Split(',');

            // ��ȿ�� �ð��� allTimes ����Ʈ�� �߰�
            allTimes.AddRange(timeStrings
                .Select(timeStr => float.TryParse(timeStr, out float time) ? time : 0)
                .Where(time => time > 0));
        }

        // �ߺ��� �ð��� �����ϰ� ����
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