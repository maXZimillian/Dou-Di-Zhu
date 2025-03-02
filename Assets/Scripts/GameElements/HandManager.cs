using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float cardSpacing = 1.5f;
    [SerializeField] private float handMaxWidth = 5f;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private Vector3 cardScale = new Vector3(1, 1, 1);
    [SerializeField] private Button chooseButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button placeButton;
    [SerializeField] private TableManager tableManager;
    
    private float handWidth;
    private GameObject selectedCard = null;
    private List<GameObject> chosenCards = new List<GameObject>();
    
    public List<GameObject> cardsInHand = new List<GameObject>();

    private void Start()
    {
        chooseButton.onClick.AddListener(ChooseCard);
        resetButton.onClick.AddListener(ResetChosenCards);
        placeButton.onClick.AddListener(PlaceChosenCards);
    }

    public void AddCard(GameObject cardPrefab)
    {
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, handTransform);
        newCard.transform.localScale = Vector3.zero;
        
        newCard.AddComponent<BoxCollider2D>();
        newCard.AddComponent<CardClickHandler>();
        
        cardsInHand.Add(newCard);
        AdjustHandWidth();
        
        newCard.GetComponent<SpriteRenderer>().sortingOrder += cardsInHand.Count;
        StartCoroutine(MoveCardToHand(newCard));
    }

    private void AdjustHandWidth()
    {
        switch (cardsInHand.Count)
        {
            case 1:
                handWidth = handMaxWidth / 100f;
                break;
            case 2:
                handWidth = handMaxWidth / 3f;
                break;
            case 3:
                handWidth = handMaxWidth / 2f;
                break;
            case 4:
                handWidth = handMaxWidth / 1.5f;
                break;
            default:
                handWidth = handMaxWidth;
                break;
        }
    }

    private IEnumerator MoveCardToHand(GameObject card)
    {
        Vector3 targetPosition = GetCardPosition(cardsInHand.IndexOf(card));
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = cardScale;
        
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            card.transform.position = Vector3.Lerp(spawnPoint.position, targetPosition, t);
            card.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        card.transform.position = targetPosition;
        card.transform.localScale = endScale;
        UpdateCardPositions();
    }

    private void UpdateCardPositions()
    {
        AdjustHandWidth();
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;
        
        float startX = -handWidth / 2;
        float spacing = cardsInHand.Count > 1 ? (handWidth / (cardsInHand.Count - 1)) : 0;
        
        for (int i = 0; i < cardCount; i++)
        {
            Vector3 targetPosition = handTransform.position + new Vector3(startX + i * spacing, 0, 0);
            if (cardsInHand[i] == selectedCard || chosenCards.Contains(cardsInHand[i]))
            {
                targetPosition += Vector3.up * 0.3f;
            }
            StartCoroutine(MoveCardSmoothly(cardsInHand[i], targetPosition));
        }
    }

    private IEnumerator MoveCardSmoothly(GameObject card, Vector3 targetPosition)
    {
        Vector3 startPosition = card.transform.position;
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        card.transform.position = targetPosition;
    }

    private Vector3 GetCardPosition(int index)
    {
        float startX = -handWidth / 2;
        float spacing = cardsInHand.Count > 1 ? (handWidth / (cardsInHand.Count - 1)) : 0;
        return handTransform.position + new Vector3(startX + index * spacing, 0, 0);
    }

    public void SelectCard(GameObject card)
    {
        if (selectedCard == card)
        {
            selectedCard = null;
        }
        else
        {
            selectedCard = card;
        }
        UpdateCardPositions();
    }

    public void ChooseCard()
    {
        if (selectedCard != null && !chosenCards.Contains(selectedCard))
        {
            chosenCards.Add(selectedCard);
            selectedCard.GetComponent<SpriteRenderer>().color = Color.green;
            selectedCard = null;
        }
        UpdateCardPositions();
    }

    public void ResetChosenCards()
    {
        foreach (GameObject card in chosenCards)
        {
            card.GetComponent<SpriteRenderer>().color = Color.white;
        }
        chosenCards.Clear();
        UpdateCardPositions();
    }

    public void PlaceChosenCards()
    {
        if (tableManager == null) return;
        
        List<GameObject> placingCards = new List<GameObject>(chosenCards);
        
        foreach (GameObject card in placingCards)
        {
            card.GetComponent<SpriteRenderer>().color = Color.white;
            cardsInHand.Remove(card);
        }
        chosenCards.Clear();
        
        tableManager.ReceiveCards(placingCards);
        UpdateCardPositions();
    }
}
