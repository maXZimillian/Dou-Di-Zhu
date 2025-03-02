using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public HandManager handManager;
    public List<GameObject> cards;
    public Button takeCardButton;

    void Start()
    {
        takeCardButton.onClick.AddListener(TakeRandomCard);
    }

    void TakeRandomCard()
    {
        if (cards.Count > 0)
        {
            int randomPosition = Random.Range(0, cards.Count);
            GameObject randomCard = cards[randomPosition];
            cards.Remove(cards[randomPosition]);
            handManager.AddCard(randomCard);
        }
    }
}