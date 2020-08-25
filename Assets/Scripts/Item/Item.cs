using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public GameObject itemPickupPrefab;
    public new string name;        
    public ItemType itemType;
    public int amount;
    public Texture icon;
    
    
    [Header("Assign when having portrait")]
    public Texture portrait;
    
    [Header("Assign it with gun type")]
    public string bulletType;

    /// <summary>
    /// 虚方法，需拓展
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("Use Item: " + name);
    }
    
    

    public enum ItemType
    {
        Gun,
        Prop,
        MeleeWeapon,
        Grenade,
        Helmet,
        BodyArmour,
        Scope,
        Clip,
        Grip,
        Nothing
    }

}
