using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CurrencyUI : MonoBehaviour
{
    public CurrencyData data;
    public Image currencyImage;
    public TextMeshProUGUI valueText;

    private void Awake()
    {
        UpdateUI();
        data.OnCurrencyUpdated += OnCurrencyUpdated;
    }

    private void OnCurrencyUpdated(int amount)
    {
        currencyImage.transform.DOKill();
        currencyImage.transform.DOPunchScale(Vector3.one * 0.1f, 0.25f, 1).OnKill(() => currencyImage.transform.localScale = Vector3.one);
        UpdateUI();
    }

    private void UpdateUI()
    {
        currencyImage.sprite = data.sprite;
        valueText.text = data.Amount.ToString();
    }
}
