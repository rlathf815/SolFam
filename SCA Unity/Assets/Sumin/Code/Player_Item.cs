using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{
    public static int HP = 3;
    public int Coffee_I = 0;
    public int MenSeter_I = 0;
    public int Noudell_I = 0;

    public Transform player;
    public GameObject prefab;


    void Start()
    {
        
    }
    public void EnergyDrink()
    {
        Debug.Log("�÷��̾�� ���͸� ���̴�!");
        FirstPersonMovement.speed = FirstPersonMovement.speed * 1.3f;
        FirstPersonMovement.runSpeed = FirstPersonMovement.runSpeed * 1.3f;
        

        StartCoroutine(DisappearAfterTime(3f,prefab));
    }

    public void Ramen()
    {
        Debug.Log("�÷��̾�� ����� �Ծ���!");
        HP += 1;
    }

    public void Coffee()
    {
        Debug.Log("�÷��̾�� Ŀ��(����)�� ����ߴ�!");
        //�̿�
    }

    public IEnumerator DisappearAfterTime(float time, GameObject target)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(time);
        FirstPersonMovement.speed = 3;
    }
}
