using UnityEngine;

public class Card : MonoBehaviour
{
    private CardData data;
    private bool isPlayed;

    public CardData Data => data;

    public void InitializeCard(CardData data)
    {
        this.data = data;
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
                player.armor.AddModifier(data.armor);
                break;
            case CardType.Attack:
                player.armor.AddModifier(data.damage);
                break;
            case CardType.Spell:
                player.armor.AddModifier(data.armor);
                player.mana.AddModifier(data.damage);
                break;
            default:
                break;
        }

        player.damage.RemoveModifier(data.requiredMana);
        isPlayed = true;
    }

    public void DestroyCard()
    {

    }
}
