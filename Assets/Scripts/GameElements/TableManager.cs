using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Transform tableTransform;
    [SerializeField] private float cardSpacing = 1.5f;
    [SerializeField] private float tableMaxWidth = 5f;
    [SerializeField] private float moveDuration = 0.5f; 
    [SerializeField] private int cardsSortingOrder = 100;  
    private float tableWidth;
    private List<GameObject> cardsOnTable = new List<GameObject>();

    public void ReceiveCards(List<GameObject> newCards)
    {
        foreach (GameObject card in newCards)
        {
            card.transform.SetParent(tableTransform);
            cardsOnTable.Add(card);
            card.GetComponent<SpriteRenderer>().sortingOrder = cardsSortingOrder + cardsOnTable.Count;
        }
        UpdateCardPositions();
    }

    private void AdjustTableWidth()
    {
        switch (cardsOnTable.Count)
        {
            case 1:
                tableWidth = tableMaxWidth / 100f;
                break;
            case 2:
                tableWidth = tableMaxWidth / 3f;
                break;
            case 3:
                tableWidth = tableMaxWidth / 2f;
                break;
            case 4:
                tableWidth = tableMaxWidth / 1.5f;
                break;
            default:
                tableWidth = tableMaxWidth;
                break;
        }
    }

    private void UpdateCardPositions()
    {
        AdjustTableWidth();
        int cardCount = cardsOnTable.Count;
        if (cardCount == 0) return;
        
        float startX = -tableWidth / 2;
        float spacing = cardCount > 1 ? (tableWidth / (cardCount - 1)) : 0;
        
        for (int i = 0; i < cardCount; i++)
        {
            Vector3 targetPosition = tableTransform.position + new Vector3(startX + i * spacing, 0, 0);
            StartCoroutine(MoveCardSmoothly(cardsOnTable[i], targetPosition));
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
}
