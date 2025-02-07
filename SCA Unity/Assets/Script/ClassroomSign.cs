using System.Collections;
using UnityEngine;
using TMPro;

public class ClassroomSign : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // 강의실 번호를 표시하는 TMP 오브젝트
    public int roomNumber; // 기본 강의실 번호 (예: 1, 2, 3...)
    public float changeProbability = 0.05f; // 강의실 번호가 13으로 바뀔 확률 (5%)
    public GameObject blood;
    public GameObject TriggerCube;

    private void Start()
    {
        // 확률에 따라 강의실 번호 변경
        if (Random.value < changeProbability)
        {
            SetClassroomNumber(13); // 13강의실로 변경
        }
        else
        {
            SetClassroomNumber(roomNumber); // 기본 강의실 번호 유지
        }
    }

    private void SetClassroomNumber(int number)
    {
        if (number == 13)
        {
            textMesh.text = "13강의실";
            blood.SetActive(true);
            TriggerCube.SetActive(true);
        }
        else
        {

            textMesh.text = number + "강의실";
            blood.SetActive(false);
        }
    }
}
