using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI valueText;

    public void UpdateUI(string data)
    {
        if (valueText.text == data)
            return;

        icon.transform.DOKill();
        icon.transform.DOPunchScale(Vector3.one * 0.2f, 0.4f, 1).OnKill(() => icon.transform.localScale = Vector3.one);
        valueText.text = data;
    }
}
