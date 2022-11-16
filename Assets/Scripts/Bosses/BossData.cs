using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Data", menuName = "Bosses/Boss Data")]
public class BossData : ScriptableObject
{
    public string bossName;
    public Sprite bossImg;
    [TextArea(0, 25)] public string description;
    public int hp;
    public int mana;
    public Range<int> damageRange;
}

