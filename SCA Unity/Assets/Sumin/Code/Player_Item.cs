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
        Debug.Log("플레이어는 몬스터를 마셨다!");
        FirstPersonMovement.speed = FirstPersonMovement.speed * speedMultiplier;
        FirstPersonMovement.runSpeed = FirstPersonMovement.runSpeed * speedMultiplier;
        speedUpIcon.SetActive(true);
        coolDownUI.StartCooldown(5f);
        StartCoroutine(DisappearAfterTime(5f,prefab));
    }

    public void Ramen()
    {
        Debug.Log("플레이어는 라면을 먹었다!");
        PlayerStats.Instance.Heal(1);
        if(HP<3) HP++;
    }

    public void Coffee()
    {
        Debug.Log("플레이어는 커피(뇌물)을 사용했다!");
        //미완
        PlayerStats.Instance.TakeDamage(2);
        yun_ui.openui = false;
    }

    public IEnumerator DisappearAfterTime(float time, GameObject target)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(time);
        FirstPersonMovement.speed = 3;
        FirstPersonMovement.runSpeed = 4;
        speedUpIcon.SetActive(false);
    }
}
