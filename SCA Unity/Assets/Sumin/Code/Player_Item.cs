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
        if (HP < 3)
        {
            PlayerStats.Instance.Heal(1);
            HP++; 
        }
    }

    public void Coffee()
    {
        Debug.Log("�÷��̾�� Ŀ��(����)�� ����ߴ�!");

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
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(time);
        FirstPersonMovement.speed = 3;
        FirstPersonMovement.runSpeed = 4;
        speedUpIcon.SetActive(false);
    }
}
