using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CertificateManager : MonoBehaviour
{
    public List<ComputerInteraction> computers; // 모든 컴퓨터 목록
    public string[] certificateTypes = { "COSpro", "ITQ", "컴활", "정처기", "FAT" };

    void Start()
    {
        AssignRandomCertificates();
        Debug.Log("1");
    }

    void AssignRandomCertificates()
    {
        Debug.Log("2");

        List<ComputerInteraction> shuffledComputers = new List<ComputerInteraction>(computers);
        Debug.Log("3");

        ShuffleList(shuffledComputers);
        Debug.Log("4");

        for (int i = 0; i < 5; i++)
        {
            shuffledComputers[i].hasCertificateFile = true;
            shuffledComputers[i].certificateType = certificateTypes[i];
        }

        Debug.Log("랜덤으로 자격증 배치 완료!");
    }

    void ShuffleList(List<ComputerInteraction> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
