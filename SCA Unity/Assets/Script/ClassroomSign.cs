using System.Collections;
using UnityEngine;
using TMPro;

public class ClassroomSign : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // ���ǽ� ��ȣ�� ǥ���ϴ� TMP ������Ʈ
    public int roomNumber; // �⺻ ���ǽ� ��ȣ (��: 1, 2, 3...)
    public float changeProbability = 0.05f; // ���ǽ� ��ȣ�� 13���� �ٲ� Ȯ�� (5%)
    public GameObject blood;
    public GameObject TriggerCube;

    private void Start()
    {
        // Ȯ���� ���� ���ǽ� ��ȣ ����
        if (Random.value < changeProbability)
        {
            SetClassroomNumber(13); // 13���ǽǷ� ����
        }
        else
        {
            SetClassroomNumber(roomNumber); // �⺻ ���ǽ� ��ȣ ����
        }
    }

    private void SetClassroomNumber(int number)
    {
        if (number == 13)
        {
            textMesh.text = "13���ǽ�";
            blood.SetActive(true);
            TriggerCube.SetActive(true);
        }
        else
        {

            textMesh.text = number + "���ǽ�";
            blood.SetActive(false);
        }
    }
}
