using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private float bulletSpeed = 1000.0f;

    private Transform thisTransform;

    // Use this for initialization

    void Start()
    {

        thisTransform = GetComponent<Transform>();

        FireBullet();

    }

    void FireBullet()
    {

        GetComponent<Rigidbody>().AddForce(thisTransform.forward * bulletSpeed);
    }
}