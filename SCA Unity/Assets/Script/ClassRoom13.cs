using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Leguar.LowHealth;
using System.Drawing;

public class ClassRoom13 : MonoBehaviour
{
    //public event Action onPlayerEnter; // �÷��̾� ���� �̺�Ʈ
    public LowHealthController shaderControllerScript;
    public FastGlitch glitch;
    public new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("�κ� ������!");
            StartCoroutine(Death());
        }
    }
    private IEnumerator Death()
    {
        VirusGUI.PopupVariables vars = new VirusGUI.PopupVariables("��Ģ", "13���ǽ��� �������� �ʽ��ϴ�.", gameObject, "OpenDoor", "", "CancelWindow", "");
        VirusGUI.MultiWindow multi = new VirusGUI.MultiWindow(100, 80, vars,audio);

        shaderControllerScript.SetPlayerHealthSmoothly(0, 3f);
        float duration = 3f; // 2�� ���� ���̵�ƿ�
        float elapsedTime = 0f;
        glitch.PixelGlitch = 1f;
        glitch.FrameGlitch = 1f;
        glitch.ChromaticGlitch = 1f;
        

        yield return new WaitForSeconds(5f);
        PlayerStats.Instance.TakeDamage(3);

    }

}
