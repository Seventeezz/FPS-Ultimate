using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Animations;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;
    public bool canPickup = true;
    
    
    private readonly GameObject[] weapons = new GameObject[4];
    private GameObject curWeapon = null;
    private int count = 0;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(weapons[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(weapons[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(weapons[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(weapons[3]);
        }
        
        
    }


    public void PickupWeapon(GameObject weapon)
    {
        //将武器放入包中
        GameObject newWeapon = Instantiate(weapon, weaponHolder);
        SetWeaponState(newWeapon, false);
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = newWeapon;
                count++;
                if (count >= weapons.Length)
                    canPickup = false;
                break;
            }
        }

        //如果手上没有武器就装备
        if (curWeapon == null)
        {
            curWeapon = newWeapon;
            SetWeaponState(curWeapon, true);
        }

        
        
    }



    private void SwitchWeapon(GameObject newWeapon)
    {
        if (curWeapon == newWeapon || newWeapon == null)
            return;
        SetWeaponState(curWeapon, false);
        curWeapon = newWeapon;
        SetWeaponState(curWeapon, true);

    }

    private void SetWeaponState(GameObject weapon,bool isActive)
    {
        weapon.GetComponent<WeaponController>().enabled = isActive;
        weapon.GetComponent<WeaponGraphics>().weaponGFX.SetActive(isActive);
    }




}
