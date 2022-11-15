using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckUI : MonoBehaviour
{
    public CardUI cardUI_Prefab;
    public Transform cardsContainer;
    public CardDeck cardDeck;
    public float rotationOffset;
    
    public void InitializeCards()
    {
        ClearCards();
        if (cardDeck.Cards.Count == 0)
            return;

        float baseRotation = (cardDeck.Cards.Count / 2f) * rotationOffset;
        for (int i = 0; i < cardDeck.Cards.Count; i++)
        {
            var cardUI = Instantiate(cardUI_Prefab, cardsContainer);
            cardUI.Initialize(cardDeck.Cards[i]);

            cardUI.transform.RotateAround(cardsContainer.position, Vector3.forward, (baseRotation - rotationOffset * i));
        }
    }

    public void UpdateUI()
    {
        //TODO
    }

    public void ClearCards()
    {
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
