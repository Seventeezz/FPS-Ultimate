using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[RequireComponent(typeof(CanvasGroup))]
public partial class Slot : MonoBehaviour
{
    
    public Item item;
    public RawImage icon;
    
    protected CanvasGroup canvasGroup;
    private Vector2 initAnchorPosition;
    
    private void Awake()
    {
        item = null;
        canvasGroup = GetComponent<CanvasGroup>();
        initAnchorPosition = icon.rectTransform.anchoredPosition;
        icon.enabled = false;
    }

    public virtual void AddToSlot(Item newItem)
    {
        item = newItem;
        icon.texture = item.icon;
        icon.enabled = true;
    }

    public virtual void ClearSlot()
    {
        item = null;
        icon.texture = null;
        icon.rectTransform.anchoredPosition = initAnchorPosition;
        icon.enabled = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


}


//Drag Logic
public partial class Slot : IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnPointerDown(PointerEventData eventData) {}


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .8f;
        canvasGroup.blocksRaycasts = false;
        icon.rectTransform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        icon.rectTransform.anchoredPosition += eventData.delta / GlobalLibrary.instance.GetCanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    
    public void BackToPlace()
    {
        icon.rectTransform.anchoredPosition = initAnchorPosition;
    }
}


/// Drop Logic
public partial class Slot : IDropHandler
{
    public Item.ItemType allowedType;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        Slot target = eventData.pointerDrag.GetComponent<Slot>();
        //不能将空的东西拖入
        if(target.item == null) return;
        //如果是仓库中的物品可以随意交换顺序
        //如果是同类物品，且当前slot不为空，则进行交换
        if (GetType() == typeof(InventorySlot) && target.GetType() == typeof(InventorySlot) 
            || item != null && target.item.itemType == item.itemType)
        {
            SwapItem(target);
            return;
        }
        
        var targetType = target.item.itemType;
        if(allowedType == targetType)
        {
            //Add to this slot
            AddToSlot(target.item);
            //Remove from other slot
            if (target is InventorySlot)
            {
                Inventory.instance.Remove(target.item);
            }
            else
            {
                target.ClearSlot();

            }
        }
        else
        {
            target.BackToPlace();
        }
    }



    void SwapItem(Slot targetSlot)
    {
        var myItem = item;
        var targetItem = targetSlot.item;
        ClearSlot();
        targetSlot.ClearSlot();
        AddToSlot(targetItem);
        targetSlot.AddToSlot(myItem);

    }
}