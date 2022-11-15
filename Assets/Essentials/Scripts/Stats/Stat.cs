using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Stat", menuName ="Stats/Stat")]
public class Stat : ScriptableObject
{
    public int baseValue = 0;
    public List<int> modifiers = new List<int>();
    public int GetValue()
    {
        int finalValue = baseValue;

        modifiers.ForEach(x => finalValue += x);

        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
