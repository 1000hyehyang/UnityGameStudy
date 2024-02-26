using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    [SerializeField]
    Text pickupText;
    bool isPickup;
    int charm;

    public GameObject player;
    public GameObject bookContent;

    void Start()
    {
        pickupText.gameObject.SetActive(false);
        bookContent.gameObject.SetActive(false);
        charm = PlayerPrefs.GetInt("Charm", 0);
    }

    void Update()
    {
        if (isPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            pickupText.gameObject.SetActive(true);
            bookContent.gameObject.SetActive(true);
            isPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            pickupText.gameObject.SetActive(false);
            bookContent.gameObject.SetActive(false);
            isPickup = false;
        }
    }

    void PickUp()
    {
        charm++;
        PlayerPrefs.SetInt("Charm", charm);
        PlayerPrefs.Save();

        pickupText.gameObject.SetActive(false);
        bookContent.gameObject.SetActive(false);

    }
}