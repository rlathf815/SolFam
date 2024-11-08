using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템이 획득될 때 호출되는 메서드
    public void PickUp()
    {
        // 아이템 획득 로직을 여기에 작성
        Debug.Log($"{gameObject.name} has been picked up!");

        // 아이템을 획득한 후 사라지게 함
        Destroy(gameObject);
    }
}
