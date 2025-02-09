using Leguar.LowHealth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storageScript : MonoBehaviour
{
    public GameObject text;
    public Animator animator;
    private bool isPlayerClose = false;
    public AudioSource audio;
    public LowHealthController shaderControllerScript;
    public FastGlitch glitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("트리거 활성화됨 창고");
            animator.SetTrigger("open");
            StartCoroutine(Death());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);          
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false); 
            isPlayerClose = false; 
        }
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        VirusGUI.PopupVariables vars = new VirusGUI.PopupVariables("규칙", "학생은 창고에 들어가서는 안됩니다.", gameObject, "OpenDoor", "", "CancelWindow", "");
        VirusGUI.PopupVariables vars2 = new VirusGUI.PopupVariables("규칙", "7번 규칙은 존재하지 않습니다.", gameObject, "OpenDoor", "", "CancelWindow", "");
        VirusGUI.MultiWindow multi = new VirusGUI.MultiWindow(20, 10, vars, audio);
        VirusGUI.MultiWindow multi2 = new VirusGUI.MultiWindow(20, 10, vars2, audio);

        shaderControllerScript.SetPlayerHealthSmoothly(0, 3f);
        float duration = 3f; // 2초 동안 페이드아웃
        float elapsedTime = 0f;
        glitch.PixelGlitch = 1f;
        glitch.FrameGlitch = 1f;
        glitch.ChromaticGlitch = 1f;


        yield return new WaitForSeconds(5f);
        PlayerStats.Instance.TakeDamage(3);

    }
}
