using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ClassroomSign : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // ���ǽ� ��ȣ�� ǥ���ϴ� TMP ������Ʈ
    public int roomNumber; // �⺻ ���ǽ� ��ȣ (��: 1, 2, 3...)
    public float changeProbability; // ���ǽ� ��ȣ�� 13���� �ٲ� Ȯ�� (5%)
    public GameObject blood;
    public GameObject TriggerCube;
    public float waitTime;
    private void Start()
    {
        textMesh.text = roomNumber + "���ǽ�";
        blood.SetActive(false);
        TriggerCube.SetActive(false);
        StartCoroutine(RandomizeRoomNumber());
    }
    private IEnumerator RandomizeRoomNumber()
    {
        while (true)
        {
            // 30�� ���
            yield return new WaitForSeconds(waitTime);

            // ���� Ȯ���� 13���ǽǷ� ����
            if (Random.value < changeProbability)  // 10% Ȯ�� (���ϸ� ���� ����)
            {
                SetClassroomNumber(13);
                Debug.Log("!! 13���ǽ��� �����Ǿ����ϴ� !!");
            }
            else
            {
                // ������ ���ǽ� ��ȣ�� ����
                SetClassroomNumber(roomNumber);
                Debug.Log("13�� ���� �ȵ�");
            }
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
            TriggerCube.SetActive(false);
        }
    }
}
