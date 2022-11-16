using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class DraggableUI : Activatable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float dragThreshold = 10f;

    private bool isDragging = false;
    private bool isInSnapRange = false;

    private float dragTimeTreshold = 0.25f;
    private float dragTimer = 0f;
    private Vector2 dragOffset;
    private Vector3 originalPos;
    private float currentDistance;

    public Action OnDragStarted;
    public Action OnDragEnded;
    public Action OnDragCancelled;

    public bool IsDragging => isDragging;
    public bool IsInSnapRange => isInSnapRange;

    public bool ExceededDragTimeThreshold => isDragging && dragTimer <= 0f;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsActive)
            return;

        isDragging = true;
        OnDragStarted?.Invoke();

        dragTimer = dragTimeTreshold;
        Vector2 currentPos = transform.position;
        dragOffset = currentPos - eventData.position;

        originalPos = transform.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!IsActive)
            return;

        if (isDragging)
        {
            var dragPos = eventData.position;

            transform.position = dragPos;
            currentDistance = Vector2.Distance(dragPos, originalPos);
            isInSnapRange = currentDistance > dragThreshold;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (!IsActive)
            return;

        if (isInSnapRange && dragTimer <= 0f)
        {
            isInSnapRange = false;
            OnDragEnded?.Invoke();
        }
        else
        {
            CancelDrag();
        }

        isDragging = false;
        currentDistance = 0f;
        dragOffset = Vector3.zero;
        dragTimer = 0f;
    }

    protected override void Tick()
    {
        if (isDragging)
        {
            dragTimer -= Time.deltaTime;
        }
    }

    public void CancelDrag()
    {
        OnDragCancelled?.Invoke();
        transform.DOMove(originalPos, 0.15f);

        isDragging = false;
        currentDistance = 0f;
        dragOffset = Vector3.zero;
        dragTimer = 0f;
    }
}
