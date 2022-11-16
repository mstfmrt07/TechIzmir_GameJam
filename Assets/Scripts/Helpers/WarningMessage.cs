using UnityEngine;
using DG.Tweening;
using TMPro;

public class WarningMessage : MSingleton<WarningMessage>
{
    public TextMeshProUGUI warningText;
    public float warningDuration;
    public float initialY;
    public float targetY;

    public void Show(string message)
    {
        DOTween.Kill(GetInstanceID());

        warningText.text = message;
        var rect = GetComponent<RectTransform>();

        rect.DOAnchorPosY(targetY, 0.5f).SetEase(Ease.OutBack).SetId(GetInstanceID()).OnComplete(
            () => 
        rect.DOAnchorPosY(initialY, 0.5f).SetEase(Ease.InBack).SetId(GetInstanceID()).SetDelay(warningDuration));
    }
}
