using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject dialogueCam;

    public GameObject startUI;
    public GameObject dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        mainCam.SetActive(false);
        dialogueCam.SetActive(true);
        startUI.SetActive(false);
        dialogue.SetActive(true);
    }
}
