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
        // �ν� ���� ���� �������� ���� ���
        if (ItemsInScope.Count == 0)
        {
            Player.Instance.LetterF.SetActive(false);
        }
        // �ν� ���� ���� �������� �ϳ��� ���� ���
        else
        {
            Player.Instance.LetterF.SetActive(true);
            // FŰ�� ������
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ���� ����� �������� ã�� �� �������� ȹ���Ѵ�.
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
