using UnityEngine;

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
        inspectorPanel.SetActive(true);

        currentCard = Instantiate(cardUI.cardDragger.gameObject, container);

        currentCard.GetComponent<DraggableUI>().IsActive = false;
        currentCard.transform.localPosition = Vector3.zero;
        currentCard.transform.rotation = Quaternion.Euler(Vector3.zero);
        currentCard.transform.localScale = Vector3.one * scaleFactor;
    }

    public void CancelInspect()
    {
        if (currentCard != null)
        {
            Destroy(currentCard.gameObject);
            currentCard = null;
        }

        inspectorPanel.SetActive(false);
    }
}
