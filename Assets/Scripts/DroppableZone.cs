using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Attach this script to the Inventory zone,making it droppable
/// </summary>
public class DroppableZone : MonoBehaviour, IDropHandler
{
    public enum ZoneType
    {
        Inventory,
        ThrowBackZone,
        DiscardZone
    }

    public ZoneType zoneType;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        Slot target = eventData.pointerDrag.GetComponent<Slot>();
        if (target.item == null) return;
        switch (zoneType)
        {
            case ZoneType.Inventory:
                if (target is InventorySlot)
                {
                    target.BackToPlace();
                }
                else
                {
                    Inventory.instance.Add(target.item);
                    target.ClearSlot();
                }
                break;
            case ZoneType.ThrowBackZone:
                target.BackToPlace();
                break;
            case ZoneType.DiscardZone:
                var playerPos = GlobalLibrary.instance.player.transform;
                var spawnPos = playerPos.position + playerPos.forward * 1 + Vector3.down;
                GameObject pickup = Instantiate(target.item.itemPickupPrefab, spawnPos, Quaternion.identity);
                pickup.GetComponent<ItemManager>().itemAmount = "1";
                if (target is InventorySlot)
                {
                    Inventory.instance.Remove(target.item);
                }
                else
                {
                    target.ClearSlot();
                }
                break;
        }

        
    }
}
