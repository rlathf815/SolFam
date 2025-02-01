using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{

    public static int HP = 3;
    public int Coffee_I = 0;
    public int MenSeter_I = 0;
    public int Noudell_I = 0;
    public float speedMultiplier = 2.0f;

    public Transform player;
    public GameObject prefab;

    public CoolDownUI coolDownUI;
    public GameObject speedUpIcon;

    void Start()
    {
        speedUpIcon.SetActive(false);
    }
    public void EnergyDrink()
    {
        Debug.Log("�÷��̾�� ���͸� ���̴�!");
        FirstPersonMovement.speed = FirstPersonMovement.speed * speedMultiplier;
        FirstPersonMovement.runSpeed = FirstPersonMovement.runSpeed * speedMultiplier;
        speedUpIcon.SetActive(true);
        coolDownUI.StartCooldown(5f);
        StartCoroutine(DisappearAfterTime(5f,prefab));
    }

    public void Ramen()
    {
        Debug.Log("�÷��̾�� ����� �Ծ���!");
        PlayerStats.Instance.Heal(1);
        if(HP<3) HP++;
    }

    public void Coffee()
    {
        Debug.Log("�÷��̾�� Ŀ��(����)�� ����ߴ�!");
        //�̿�
        PlayerStats.Instance.TakeDamage(2);
        yun_ui.openui = false;
    }

    public IEnumerator DisappearAfterTime(float time, GameObject target)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(time);
        FirstPersonMovement.speed = 3;
        FirstPersonMovement.runSpeed = 4;
        speedUpIcon.SetActive(false);
    }
}
