using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_ui : MonoBehaviour
{
    public GameObject Epress;
    public GameObject Shop;
    public static bool Eopen;
    public static bool Sopen;
    void Update()
    {
        if (Eopen)
        {
            Epress.SetActive(true);
        }
        else
        {
            Epress.SetActive(false);
        }
        if (Sopen)
        {
            Shop.SetActive(true);
        }
        else
        {
            Shop.SetActive(false);
        }
    }
}
