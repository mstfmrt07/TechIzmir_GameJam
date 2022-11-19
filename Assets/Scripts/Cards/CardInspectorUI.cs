using UnityEngine;
using DG.Tweening;

public class CardInspectorUI : MSingleton<CardInspectorUI>
{
    public GameObject inspectorPanel;
    public Transform container;
    private GameObject currentCard;
    public float scaleFactor;

    private void Awake()
    {
        CancelInspect();
    }

    public void InspectCard(CardUI cardUI)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.inspectCard);
        inspectorPanel.transform.localScale = Vector3.one;
        inspectorPanel.SetActive(true);

        currentCard = Instantiate(cardUI.cardDragger.gameObject, container);

        currentCard.GetComponent<DraggableUI>().IsActive = false;
        currentCard.GetComponent<ScalableButton>().interactable = false;
        currentCard.transform.localPosition = Vector3.zero;
        currentCard.transform.rotation = Quaternion.Euler(Vector3.forward * 180f);
        currentCard.transform.localScale = Vector3.zero;
        currentCard.transform.DOScale(scaleFactor, 0.5f).SetEase(Ease.OutBounce);
        currentCard.transform.DOLocalRotate(Vector3.forward * 180f, 0.8f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBounce);
    }

    public void CancelInspect()
    {
        if (currentCard != null)
        {
            Destroy(currentCard.gameObject);
            currentCard = null;
        }

        inspectorPanel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => inspectorPanel.SetActive(false));
    }
}
