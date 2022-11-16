using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MSingleton<Player>, IGameEventsHandler
{
    public int hp;
    public int mana;
    public CardDeck cardDeck;
    public Transform tableCardsContainer;
    private List<Card> cardsOnTable = new List<Card>();

    private Boss currentBoss;

    public List<Card> CardsOnTable => cardsOnTable;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void StartFight(Boss boss)
    {
        currentBoss = boss;
    }

    public void PlayCard(Card card)
    {
        Debug.Log($"{gameObject.name}, plays the card {card.Data.name}.");
        cardDeck.PlayCard(card);

        cardsOnTable.Add(card);
        card.transform.parent = tableCardsContainer;
        //TODO Implement card order on table
        card.transform.localPosition = Vector3.zero + (Vector3.right * 0.6f * cardsOnTable.Count);
    }

    public void GetDamage(Card card, int damage)
    {
        if (card == null)
        {
            hp -= damage;
            Debug.Log($"{gameObject.name}, takes {damage} damage.");
        }
        else
        {
            card.GetDamage(damage);
            Debug.Log($"{card.Data.cardName}, takes {damage} damage.");
        }
    }

    public void Attack(int damage)
    {
        Debug.Log($"{gameObject.name}, deals {damage} damage to {currentBoss.Data.name}.");
        currentBoss.GetDamage(damage);
    }

    public void SpendMana(int amount)
    {
        if (mana - amount >= 0)
        {
            mana -= amount;
        }
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnLevelLoaded += OnLevelLoaded;
        GameEvents.OnLevelStarted += OnLevelStarted;
        GameEvents.OnLevelFailed += OnLevelFailed;
        GameEvents.OnLevelSucceeded += OnLevelSucceeded;
    }

    public void OnLevelLoaded()
    {
    }

    public void OnLevelStarted()
    {
        cardDeck.ShuffleCards();
    }

    public void OnLevelFailed()
    {
    }

    public void OnLevelSucceeded()
    {
    }
}
