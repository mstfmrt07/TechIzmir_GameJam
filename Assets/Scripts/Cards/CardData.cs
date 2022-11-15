using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardType type;
    public CardEra era;
    [TextArea(0, 10)] public string description;
    public int armor;
    public int damage;
    public int requiredMana;
    public Sprite cardIcon;
}

public enum CardEra
{
    Era1, Era2, Era3
}

public enum CardType
{
    Defense, Attack, Spell
}