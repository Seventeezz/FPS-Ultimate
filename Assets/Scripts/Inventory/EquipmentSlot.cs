using System;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slot))]
public class EquipmentSlot : MonoBehaviour
{
    

    // public void OnDrop(PointerEventData eventData)
    // {
    //     if (eventData.pointerDrag != null)
    //     {
    //         draggableSlot = eventData.pointerDrag.GetComponent<DraggableSlot>();
    //         if (draggableSlot.item.itemType != itemType)
    //         {
    //             //如果装备类型不匹配，回归原位
    //             draggableSlot.BackToPlace();
    //         }
    //         else
    //         {
    //             //装备匹配，则置入，并存储信息
    //             item = draggableSlot.item;
    //             itemIcon.enabled = true;
    //             itemIcon.texture = item.icon;
    //             draggableSlot.BackToPlace();
    //             //从仓库中删去此物品
    //             Inventory.instance.Remove(item);
    //         }
    //     }
    // }
}
