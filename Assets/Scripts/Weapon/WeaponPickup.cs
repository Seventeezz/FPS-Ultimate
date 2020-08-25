using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        var weaponManager = other.GetComponent<WeaponManager>();
        if (!weaponManager.canPickup)
        {
            return;
        }
        weaponManager.PickupWeapon(weaponPrefab);
        Destroy(gameObject);
    }
    
    
    
}
