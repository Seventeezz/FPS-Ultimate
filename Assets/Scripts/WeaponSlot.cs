using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSlot : Slot
{
    public TextMeshProUGUI weaponName;
    public GameObject[] gameObjectsToDisable;
    
    
    private void Start()
    {
        SetActive(false);
    }

    
    public override void AddToSlot(Item newItem)
    {
        base.AddToSlot(newItem);
        icon.texture = newItem.portrait;
        weaponName.text = item.name;
        SetActive(true);
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        SetActive(false);
    }
    

    void SetActive(bool value)
    {
        foreach (var go in gameObjectsToDisable)
        {
            go.SetActive(value);
        }
    }
}
