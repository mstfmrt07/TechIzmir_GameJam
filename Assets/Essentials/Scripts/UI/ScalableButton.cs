using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ScalableButton : Button
{
    private float selectedScale = 0.9f;
    private float normalScale = 1f;

    private void Select(bool state)
    {
        transform.DOScale(
            state ? selectedScale : normalScale, 0.1f)
            .SetEase(state ? Ease.InBack : Ease.OutBack);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        SoundManager.Instance.PlaySound(SoundManager.Instance.clickClip);
        Select(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        Select(false);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Select(true);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Select(false);
    }
}
