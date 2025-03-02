using UnityEngine;

public class CardClickHandler : MonoBehaviour
{
    private HandManager handManager;

    void Start()
    {
        handManager = FindObjectOfType<HandManager>();
    }

    void OnMouseDown()
    {
        if (handManager != null)
        {
            handManager.SelectCard(gameObject);
        }
    }
}