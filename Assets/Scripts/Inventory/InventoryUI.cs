using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private InventorySlot[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChanged += UpdateUI;
        //Get the slots from Left Content
        slots = transform.GetChild(0).GetComponentsInChildren<InventorySlot>();
        gameObject.SetActive(false);
        UpdateUI();
    }


    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddToSlot(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    
    
}
