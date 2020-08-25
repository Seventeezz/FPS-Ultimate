using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : Slot
{
    public RawImage itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;

    public override void AddToSlot(Item newItem)
    {
        base.AddToSlot(newItem);
        itemIcon.texture = newItem.icon;
        itemName.text = newItem.name;
        itemAmount.text = newItem.amount.ToString();
        BackToPlace();
        gameObject.SetActive(true);
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        gameObject.SetActive(false);
    }


    
    /// <summary>
    /// 需要被调用
    /// </summary>
    public void OnItemUsed()
    {
        
    }
    
    
}
