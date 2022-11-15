using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardData data;
    private bool isPlayed;
    private int armor;
    private int damage;

    public CardData Data => data;
    public int Armor => armor;
    public int Damage => damage;

    public Action OnDestroy;
    public Action<Card, int> OnGetDamage;

    public void InitializeCard(CardData data)
    {
        this.data = data;
        this.armor = data.armor;
        this.damage = data.damage;
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
    }

    public void DestroyCard()
    {

    }

    public void GetDamage(int damage)
    {
        armor -= damage;
        OnGetDamage?.Invoke(this, damage);

        if (armor <= 0)
        {
            OnDestroy?.Invoke();
        }
    }
}
