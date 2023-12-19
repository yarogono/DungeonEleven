using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotTypes
{
    All,
    Weapon,
    Armour,
    Accessory,
    Misc,
}
public class UISlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SlotTypes SlotType;
    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            if (IsLocked)
                return;
            _item = value;
            UpdateSlotIcon();
        }
    }
    private SlotDragHandler _dragHandler;
    private Image _foregroundImage;
    private Image _backgroundImage;
    private Image _itemIcon;
    private Image _lockIcon;
    private Color _defaultForegroundColor;
    private Color _defaultBackgroundColor;
    private float _lastClickTime;
    private bool _isChoosed;
    public bool IsChoosed
    {
        get { return _isChoosed; }
        set
        {
            if (IsLocked)
                return;
            if (value == true)
            {
                _isChoosed = true;
                _backgroundImage.color = Color.yellow;
            }
            else
            {
                _isChoosed = false;
                _backgroundImage.color = _defaultBackgroundColor;
            }
        }
    }
    private bool _isLocked;
    public bool IsLocked
    {
        get { return _isLocked; }
        set
        {
            _isLocked = value;
            UpdateLockState();
        }
    }

    private static readonly string FOREGROUND_IMAGE_NAME = "Foreground";
    private static readonly string BACKGROUND_IMAGE_NAME = "Background";
    private static readonly string ITEM_ICON_PATH = "Foreground/ItemIcon";
    private static readonly string LOCK_ICON_PATH = "Foreground/LockIcon";
    private static readonly float DOUBLE_CLICK_TIME = 0.3f;
    public static readonly string SlotPrefabPath = "Prefabs/UI/Slot";

    private void Awake()
    {
        _foregroundImage = transform.Find(FOREGROUND_IMAGE_NAME).GetComponent<Image>();
        _backgroundImage = transform.Find(BACKGROUND_IMAGE_NAME).GetComponent<Image>();
        _itemIcon = transform.Find(ITEM_ICON_PATH).GetComponent<Image>();
        _lockIcon = transform.Find(LOCK_ICON_PATH).GetComponent<Image>();
        _defaultForegroundColor = _foregroundImage.color;
        _defaultBackgroundColor = _backgroundImage.color;
        _dragHandler = GetComponentInChildren<SlotDragHandler>();
        IsChoosed = false;
        IsLocked = true;
    }
    private void Start()
    {
        UpdateSlotIcon();
    }
    private void UpdateSlotIcon()
    {
        if (_item == null)
        {
            SetHideItemIcon(true);
            return;
        }

        // item의 sprite로 슬롯의 sprite 갱신
        // _image.sprite = _item.sprite;
        SetHideItemIcon(false);
    }
    private void UpdateLockState()
    {
        _lockIcon.enabled = IsLocked;
    }

    public void SwapSlot(UISlot otherSlot)
    {
        if (IsLocked)
            return;
        // 스왑 예외 조건
        switch (SlotType)
        {
            case SlotTypes.All:
                break;
            case SlotTypes.Weapon:
                if (otherSlot.IsEmpty())
                {
                    if (!(otherSlot.SlotType == SlotTypes.Weapon || otherSlot.SlotType == SlotTypes.All))
                    {
                        return;
                    }
                }
                else
                {
                    // ItemType 생기면 적용.
                    //if (otherSlot.Item.ItemType != ItemTypes.Weapon)
                    //{
                    //    return;
                    //}
                }
                break;
            case SlotTypes.Armour:
                if (otherSlot.IsEmpty())
                {
                    if (!(otherSlot.SlotType == SlotTypes.Armour || otherSlot.SlotType == SlotTypes.All))
                    {
                        return;
                    }
                }
                else
                {
                    // ItemType 생기면 적용.
                    //if (otherSlot.Item.ItemType != ItemTypes.Armour)
                    //{
                    //    return;
                    //}
                }
                break;
            case SlotTypes.Accessory:
                if (otherSlot.IsEmpty())
                {
                    if (!(otherSlot.SlotType == SlotTypes.Accessory || otherSlot.SlotType == SlotTypes.All))
                    {
                        return;
                    }
                }
                else
                {
                    // ItemType 생기면 적용.
                    //if (otherSlot.Item.ItemType != ItemTypes.Accessory)
                    //{
                    //    return;
                    //}
                }
                break;
            case SlotTypes.Misc:
                if (otherSlot.IsEmpty())
                {
                    if (!(otherSlot.SlotType == SlotTypes.Misc || otherSlot.SlotType == SlotTypes.All))
                    {
                        return;
                    }
                }
                else
                {
                    // ItemType 생기면 적용.
                    //if (otherSlot.Item.ItemType != ItemTypes.Misc)
                    //{
                    //    return;
                    //}
                }
                break;
        }

        Item temp = otherSlot.Item;
        otherSlot.Item = Item;
        Item = temp;

        Sprite tempSprite = otherSlot._itemIcon.sprite;
        otherSlot._itemIcon.sprite = _itemIcon.sprite;
        _itemIcon.sprite = tempSprite;
    }
    public bool IsEmpty()
    {
        return Item == null ? true : false;
    }
    public void SetHideItemIcon(bool isHide)
    {
        if (isHide)
        {
            _itemIcon.enabled = false;
        }
        else
        {
            _itemIcon.enabled = true;
        }
    }
    public void SetDraggable(bool isDraggable)
    {
        _dragHandler.isDraggable = isDraggable;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (IsLocked)
            return;
        SlotDragHandler handler = eventData.pointerDrag.GetComponent<SlotDragHandler>();
        SwapSlot(handler.ParentSlot);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _foregroundImage.color = _foregroundImage.color * 0.7f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _foregroundImage.color = _defaultForegroundColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickTime - _lastClickTime < DOUBLE_CLICK_TIME)
        {
            OnDoubleClicked();
            _lastClickTime = eventData.clickTime;
            return;
        }
        OnClicked();
        _lastClickTime = eventData.clickTime;
    }
    public void OnClicked()
    {
        if (IsLocked)
            return;

        if (IsChoosed)
        {
            IsChoosed = false;
            return;
        }
        IsChoosed = true;
    }
    public void OnDoubleClicked()
    {
        if (IsLocked)
            return;
        // 필요하면 구현
    }
    
}
