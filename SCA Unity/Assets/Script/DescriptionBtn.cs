using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBtn : MonoBehaviour
{
    public GameObject description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showDescription()
    {
        description.SetActive(true);
    }
    public void hideDescription() { 
        description.SetActive(false); 
    }
}
