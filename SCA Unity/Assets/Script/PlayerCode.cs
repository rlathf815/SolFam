using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    private List<Item> ItemsInScope;

    private void Awake()
    {
        ItemsInScope = new List<Item>();
    }

    private void Update()
    {
        // 인식 범위 내에 아이템이 없을 경우
        if (ItemsInScope.Count == 0)
        {
            Player.Instance.LetterE.SetActive(false);
        }
        // 인식 범위 내에 아이템이 하나라도 있을 경우
        else
        {
            Player.Instance.LetterE.SetActive(true);
            // F키를 누르면
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 가장 가까운 아이템을 찾아 그 아이템을 획득한다.
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
                nearestItem.PickUp();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            ItemsInScope.Add(item);
            Debug.Log("아이템있음");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            ItemsInScope.Remove(item);
        }
    }
}
