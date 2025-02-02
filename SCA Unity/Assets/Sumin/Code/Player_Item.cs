using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Player_Item : MonoBehaviour
{

    public static int HP = 3;
    public int Coffee_I = 0;
    public int MenSeter_I = 0;
    public int Noudell_I = 0;
    public float speedMultiplier = 2.0f;
    public float throwForce = 5f;

    public Transform player;
    public GameObject prefab;

    public CoolDownUI coolDownUI;
    public GameObject speedUpIcon;

    public GameObject coffeePrefab;

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
        if (HP < 3)
        {
            PlayerStats.Instance.Heal(1);
            HP++; 
        }
    }

    public void Coffee()
    {
        Debug.Log("플레이어는 커피(뇌물)을 사용했다!");

        if (coffeePrefab != null)
        {
            
            Vector3 spawnPosition = player.position + player.forward * 1.0f + Vector3.up * 1.0f; 
           
            GameObject spawnedCoffee = Instantiate(coffeePrefab, spawnPosition, Quaternion.identity);
            spawnedCoffee.tag = "activeCoffee"; 

          
            Rigidbody coffeeRb = spawnedCoffee.GetComponent<Rigidbody>();
            if (coffeeRb != null)
            {
                coffeeRb.AddForce(player.forward * throwForce + Vector3.up * 2f, ForceMode.Impulse); 
            }
        }

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
