using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameData gameData; 
    public GameObject inventoryUI; 
    public Transform itemSlotContainer; 
    public GameObject itemSlotPrefab; 

    private void Start()
    {
        UpdateInventoryUI(); 
    }

    public void AddItemToInventory(string itemName)
    {
        gameData.AddItem(itemName);
        UpdateInventoryUI();
    }

    public void UseItemFromInventory(string itemName)
    {
        if (gameData.itemList.Contains(itemName))
        {
            gameData.UseItem(itemName);
            UpdateInventoryUI();
        }
        else
        {
            Debug.Log("아이템이 인벤토리에 없습니다.");
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject); 
        }

        foreach (string itemName in gameData.itemList)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            itemSlot.GetComponentInChildren<Text>().text = itemName;
        }
    }
}
