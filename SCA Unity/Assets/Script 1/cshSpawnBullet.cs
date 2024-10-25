using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshSpawnBullet : MonoBehaviour
{

    public Transform Bullet;

    public float fireTime = 1.0f;

    public float firePassTime = 0.0f;

    public Transform BulletFirePos;

    public Transform LookatObj;

    // Update is called once per frame

    void Update()
    {

        if (firePassTime >= fireTime)
        {

            Instantiate(Bullet, BulletFirePos.position, BulletFirePos.rotation);

            firePassTime = 0.0f;

        }
        else
        {

            firePassTime += Time.deltaTime;

        }

        transform.LookAt(LookatObj);

    }

}