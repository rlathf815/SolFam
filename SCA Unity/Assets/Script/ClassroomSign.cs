using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ClassroomSign : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // 강의실 번호를 표시하는 TMP 오브젝트
    public int roomNumber; // 기본 강의실 번호 (예: 1, 2, 3...)
    public float changeProbability; // 강의실 번호가 13으로 바뀔 확률 (5%)
    public GameObject blood;
    public GameObject TriggerCube;
    public float waitTime;
    private void Start()
    {
        textMesh.text = roomNumber + "강의실";
        blood.SetActive(false);
        TriggerCube.SetActive(false);
        StartCoroutine(RandomizeRoomNumber());
    }
    private IEnumerator RandomizeRoomNumber()
    {
        while (true)
        {
            // 30초 대기
            yield return new WaitForSeconds(waitTime);

            // 낮은 확률로 13강의실로 변경
            if (Random.value < changeProbability)  // 10% 확률 (원하면 조절 가능)
            {
                SetClassroomNumber(13);
                Debug.Log("!! 13강의실이 생성되었습니다 !!");
            }
            else
            {
                // 랜덤한 강의실 번호로 변경
                SetClassroomNumber(roomNumber);
                Debug.Log("13강 생성 안됨");
            }
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
            TriggerCube.SetActive(false);
        }
    }
}
