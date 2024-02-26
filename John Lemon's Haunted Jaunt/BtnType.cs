using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                PlayerPrefs.DeleteKey("Charm");
                SceneManager.LoadScene("MainScene");
                break;
            case BTNType.Quit:
                Application.Quit();
                break;
            case BTNType.Menu:
                SceneManager.LoadScene("TitleScene");
                break;
            case BTNType.Reset:
                PlayerPrefs.DeleteAll();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
