using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MSingleton<CurrencyController>
{
    public CurrencyData money;

    public void AddToCurrency(CurrencyData data, int amountToAdd)
    {
        data.AddAmount(amountToAdd);
    }

    public void AddMoney(int amountToAdd)
    {
        money.AddAmount(amountToAdd);
    }
}
