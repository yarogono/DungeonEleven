using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UISlot ParentSlot;
    private Image _image;
    private Vector2 _initialPosition;
    private CanvasGroup _canvasGroup;
    private Transform _previousParent;
    private Canvas _canvas;
    private int _siblingIndex;
    public bool isDraggable;
    private void Awake()
    {
        ParentSlot = GetComponentInParent<UISlot>();
        _canvas = GetComponentInParent<Canvas>();
        _image = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        _previousParent = transform.parent;
        _siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(_canvas.transform);
        transform.SetAsLastSibling();
        _initialPosition = transform.position;
        _canvasGroup.alpha = 0.7f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        transform.SetParent(_previousParent);
        transform.SetSiblingIndex(_siblingIndex);
        transform.position = _initialPosition;
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
