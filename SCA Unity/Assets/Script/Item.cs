using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // �������� ȹ��� �� ȣ��Ǵ� �޼���
    public void PickUp()
    {
        // ������ ȹ�� ������ ���⿡ �ۼ�
        Debug.Log($"{gameObject.name} has been picked up!");

        // �������� ȹ���� �� ������� ��
        Destroy(gameObject);
    }
}
