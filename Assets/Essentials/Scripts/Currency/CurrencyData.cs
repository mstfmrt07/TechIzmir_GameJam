using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Currency Data", menuName = "Currency/CurrencyData")]
public class CurrencyData: ScriptableObject
{
    public Sprite sprite;
    public int maximumAmount;
    public int Amount { get => amount; set => amount = value; }

    public Action<int> OnCurrencyUpdated;
    [SerializeField] private int amount;

    public void AddAmount(int amountToAdd)
    {
        int newAmount = Amount + amountToAdd;

        if (newAmount >= 0)
        {
            amount = newAmount;

            if (OnCurrencyUpdated != null)
                OnCurrencyUpdated.Invoke(newAmount);
        }
        else
        {
            Debug.Log("Couldn't update amount.");
        }
    }
}
