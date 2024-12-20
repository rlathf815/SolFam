using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    private List<Item> ItemsInScope;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        ItemsInScope = new List<Item>();
    }

    private void Update()
    {
        if (ItemsInScope.Count == 0)
        {
            Player.Instance.LetterE.SetActive(false);
        }
        else
        {
            Player.Instance.LetterE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = Player.Instance.transform.position;
                Item nearestItem = ItemsInScope[0];

                for (int i = 1; i < ItemsInScope.Count; ++i)
                {
                    if (Vector3.Distance(playerPos, nearestItem.transform.position) >
                        Vector3.Distance(playerPos, ItemsInScope[i].transform.position))
                    {
                        nearestItem = ItemsInScope[i];
                    }
                }

                // ������ �̸� ���� �� �κ��丮�� �߰�
                string tmp = nearestItem.gameObject.name;
               
                // ItemsInScope ����Ʈ���� ������ ����
                ItemsInScope.Remove(nearestItem);
                // ������ �ı�
                nearestItem.PickUp();
                inventoryManager.AddItemToInventory(tmp);
                // UI ������Ʈ
                Player.Instance.LetterE.SetActive(ItemsInScope.Count > 0);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            ItemsInScope.Add(item);
            Debug.Log("����������");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            ItemsInScope.Remove(item);
            Player.Instance.LetterE.SetActive(ItemsInScope.Count > 0);
        }
    }
}
