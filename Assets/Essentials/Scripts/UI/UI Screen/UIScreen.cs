using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour, IScreen
{
    private CanvasGroup canvasGroup;

    public bool IsVisible => canvasGroup.alpha > 0f;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Load()
    {
        canvasGroup.DOFade(1f, 0.4f).OnComplete(
            () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    public virtual void Reset()
    {
    }

    public virtual void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }
}
