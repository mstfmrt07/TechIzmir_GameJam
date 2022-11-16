using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MSingleton<Player>, IGameEventsHandler
{
    public PlayerUI playerUI;
    public int initialHP;
    public int initialMana;
    public CardDeck cardDeck;
    public Transform tableCardsContainer;

    private List<Card> cardsOnTable = new List<Card>();
    private Boss currentBoss;

    private int currentHP;
    private int currentMana;

    public int CurrentHP => currentHP;
    public int CurrentMana => currentMana;

    public int CurrentArmor
    {
        get
        {
            int armor = 0;
            cardsOnTable.ForEach(x => armor += x.CurrentHP);
            return armor;
        }
    }

    public int CurrentDamage
    {
        get
        {
            int damage = 0;
            cardsOnTable.ForEach(x => damage += x.Data.damage);
            return damage;
        }
    }

    public List<Card> CardsOnTable => cardsOnTable;
    public Action OnPlayerUpdated;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void StartFight(Boss boss)
    {
        currentBoss = boss;
        currentHP = initialHP;
        currentMana = initialMana;

        playerUI.Initialize();
        OnPlayerUpdated?.Invoke();
    }

    public bool AttemptPlayCard(Card card)
    {
        bool hasEnoughMana = SpendMana(card.Data.requiredMana);
        if (hasEnoughMana)
        {
            cardDeck.PlayCard(card);

            cardsOnTable.Add(card);
            card.transform.parent = tableCardsContainer;
            //TODO Implement card order on table
            card.transform.localPosition = Vector3.zero + (Vector3.right * 0.6f * cardsOnTable.Count);
            OnPlayerUpdated?.Invoke();
            Debug.Log($"{gameObject.name}, plays the card {card.Data.name}.");
        }
        else
        {
            WarningMessage.Instance.Show("Not enough mana!");
        }
        return hasEnoughMana;
    }

    public void GetDamage(Card card, int damage)
    {
        if (card == null)
        {
            currentHP -= damage;
            Debug.Log($"{gameObject.name}, takes {damage} damage.");
        }
        else
        {
            card.GetDamage(damage);
            Debug.Log($"{card.Data.cardName}, takes {damage} damage.");
        }
        OnPlayerUpdated?.Invoke();
    }

    public void Attack(int damage)
    {
        Debug.Log($"{gameObject.name}, deals {damage} damage to {currentBoss.Data.name}.");
        currentBoss.GetDamage(damage);
    }

    public bool SpendMana(int amount)
    {
        bool hasEnoughMana = (currentMana - amount >= 0);
        if (hasEnoughMana)
        {
            currentMana -= amount;
            OnPlayerUpdated?.Invoke();
        }

        return hasEnoughMana;
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