using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    [SerializeField]
    Text pickupText;
    bool isPickup;

    public GameObject player;
    public GameObject bookContent;
    GameManager gameManager;


    void Start()
    {
        pickupText.gameObject.SetActive(false);
        bookContent.gameObject.SetActive(false);
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
        gameManager.IncreaseCharm();

        pickupText.gameObject.SetActive(false);
        bookContent.gameObject.SetActive(false);

    }
}
