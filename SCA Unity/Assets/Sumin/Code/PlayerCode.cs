using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerCode : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    List<Item> ItemsInScope;

    private void Awake()
    {
        ItemsInScope = new List<Item>();
    }

    private void Update()
    {
        // 인식 범위 내에 아이템이 없을 경우
        if (ItemsInScope.Count == 0)
        {
            Player.Instance.LetterF.SetActive(false);
        }
        // 인식 범위 내에 아이템이 하나라도 있을 경우
        else
        {
            Player.Instance.LetterF.SetActive(true);
            // F키를 누르면
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 가장 가까운 아이템을 찾아 그 아이템을 획득한다.
                Vector3 playerPos = Player.Instance.transform.position;
                Item nearestItem = ItemsInScope[0];
                for (int i = 0; i < ItemsInScope.Count; ++i)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
            ItemsInScope.Add(item);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
            ItemsInScope.Remove(item);
    }
}
