using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Inventory singleton not working!");
        }
        instance = this;
        
    }

    #endregion

    public List<Item> items = new List<Item>();
    public GameObject inventoryUI;
    public UnityAction onItemChanged;
    
    
    public void Add(Item item)
    {
        items.Add(item);
        onItemChanged.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChanged.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Cursor.lockState = inventoryUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
