using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardData data;
    private bool isPlayed;
    private int currentHP;

    public CardData Data => data;
    public int CurrentHP => currentHP;

    public Action OnDestroy;
    public Action OnCardPlayed;
    public Action<Card, int> OnGetDamage;

    public void InitializeCard(CardData data)
    {
        this.data = data;
        currentHP = data.armor;
        isPlayed = false;
    }

    public void Play()
    {
        if (isPlayed)
            return;

        var player = Player.Instance;

        //Add stat modifiers
        switch (data.type)
        {
            case CardType.Defense:
                //TODO do something with armor
                break;
            case CardType.Attack:
                player.Attack(data.damage);
                break;
            case CardType.Spell:
                //TODO do something with armor
                player.Attack(data.damage);
                break;
            default:
                break;
        }

        player.SpendMana(data.requiredMana);
        isPlayed = true;
        OnCardPlayed?.Invoke();
    }

    public void DestroyCard()
    {

    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        OnGetDamage?.Invoke(this, damage);

        if (currentHP <= 0)
        {
            OnDestroy?.Invoke();
        }
    }
}
