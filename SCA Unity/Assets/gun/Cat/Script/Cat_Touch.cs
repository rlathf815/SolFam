using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private bool isClose;
    public 

    //������ Ȯ�������� �������ϰ� �ƴҰ��(�÷��̾�� ���� �ִ´�) ȣ�������� �Ͽ� ȣ�������� �׾ƾ��Ѵ� ȣ����Ű[E]Ű 
    void Start()
    {
        isClose = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E)) {
            if(Random.value < 0.5f)
            {

            }
        }
    }
}
