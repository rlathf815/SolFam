using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Item : MonoBehaviour
{
    public GameData gameData;
    public void PickUp()
    {
        Debug.Log($"{gameObject.name} has been picked up!");

        //gameData.AddItem(gameObject.name);
        Destroy(gameObject);
    }

}
