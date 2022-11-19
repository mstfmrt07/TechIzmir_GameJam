using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MSingleton<Player>, IGameEventsHandler, IRoundPlayer
{
    public PlayerUI playerUI;
    public int initialHP;
    public int initialMana;
    public CardDeck cardDeck;
    public Transform tableCardsContainer;

    private List<Card> cardsOnTable = new List<Card>();

    private Boss currentEnemy;

    private bool canPlayCard = false;
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
    public Action OnDestroy;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    public void StartFight(IRoundPlayer enemy)
    {
        currentEnemy = (Boss)enemy;
        currentHP = initialHP;
        currentMana = initialMana;

        playerUI.Initialize();
        Debug.Log($"{gameObject.name}, started the fight with {currentEnemy.gameObject.name}");
        OnPlayerUpdated?.Invoke();
    }

    public bool AttemptPlayCard(Card card)
    {
        if (!canPlayCard)
            return false;

        bool hasEnoughMana = SpendMana(card.Data.requiredMana);
        if (hasEnoughMana)
        {
            cardDeck.PlayCard(card);
            SoundManager.Instance.PlaySound(SoundManager.Instance.playCard);

            card.transform.parent = tableCardsContainer;
            //TODO Implement card order on table
            card.transform.localPosition = new Vector3(-6f , 0f , 6f) + (Vector3.right * 6f * (cardsOnTable.Count % 4) - (Vector3.forward * 12f * (cardsOnTable.Count / 4)));

            AddCardToTable(card);

            OnPlayerUpdated?.Invoke();
            Debug.Log($"{gameObject.name}, plays the card {card.Data.name}.");
        }
        else
        {
            WarningMessage.Instance.Show("Yetersiz kudret!");
        }
        return hasEnoughMana;
    }

    public void AddCardToTable(Card card)
    {
        cardsOnTable.Add(card);
        card.OnDestroy += RemoveCardFromTable;
    }

    private void RemoveCardFromTable(Card card)
    {
        card.OnDestroy -= RemoveCardFromTable;
        cardsOnTable.Remove(card);
    }

    public void GetDamage(Card card, int damage)
    {
        if (card == null)
        {
            currentHP -= damage;
            WarningMessage.Instance.Show($"{gameObject.name}, {damage} hasar aldý.");

            if (currentHP <= 0)
            {
                OnDestroy?.Invoke();
            }
        }
        else
        {
            card.GetDamage(damage);
            WarningMessage.Instance.Show($"{card.Data.cardName}, {damage} hasar aldý.");
        }
        OnPlayerUpdated?.Invoke();
    }

    public void Attack(int damage)
    {
        Debug.Log($"{gameObject.name}, deals {damage} damage to {currentEnemy.Data.name}.");
        currentEnemy.GetDamage(damage);
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
    }

    public void OnLevelFailed()
    {
    }

    public void OnLevelSucceeded()
    {
    }

    public void TakeTurn()
    {
        canPlayCard = true;
        currentMana = initialMana;
        cardDeck.DrawCard();
        OnPlayerUpdated?.Invoke();
    }

    public void GiveTurn()
    {
        cardDeck.ClearDeck();
        canPlayCard = false;
        RoundManager.Instance.GiveTurn(currentEnemy);
        OnPlayerUpdated?.Invoke();
    }
}
