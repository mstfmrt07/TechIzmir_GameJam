using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI infoText;

    private Vector3 initialInfoPos;
    private void Awake()
    {
        initialInfoPos = infoText.transform.localPosition;
        infoText.gameObject.SetActive(false);
    }

    public void UpdateUI(string data, string info = "")
    {
        if (valueText.text == data)
            return;

        icon.transform.DOKill();
        icon.transform.DOPunchScale(Vector3.one * 0.2f, 0.4f, 1).OnKill(() => icon.transform.localScale = Vector3.one);
        valueText.text = data;

        if (info != "")
        {
            infoText.gameObject.SetActive(true);
            infoText.text = info;
            infoText.transform.DOLocalMove(initialInfoPos + Vector3.up * 100f, 2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                infoText.gameObject.SetActive(false);
                infoText.transform.localPosition = initialInfoPos;
            });
        }
    }
}
