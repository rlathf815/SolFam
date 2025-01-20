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
        Debug.Log("플레이어는 몬스터를 마셨다!");
        FirstPersonMovement.speed = FirstPersonMovement.speed * 1.3f;
        FirstPersonMovement.runSpeed = FirstPersonMovement.runSpeed * 1.3f;
        

        StartCoroutine(DisappearAfterTime(3f,prefab));
    }

    public void Ramen()
    {
        Debug.Log("플레이어는 라면을 먹었다!");
        HP += 1;
    }

    public void Coffee()
    {
        Debug.Log("플레이어는 커피(뇌물)을 사용했다!");
        //미완
    }

    public IEnumerator DisappearAfterTime(float time, GameObject target)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(time);
        FirstPersonMovement.speed = 3;
    }
}
