using UnityEngine;

public class checkCamera : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Camera.main == null) // 메인 카메라가 없을 때
        {
            Debug.LogError("플레이어 오브젝트에 카메라가 없습니다! 자동으로 할당합니다.");

            GameObject playerCamera = GameObject.FindWithTag("MainCamera");

            if (playerCamera != null)
            {
                Camera mainCam = playerCamera.GetComponent<Camera>();
                if (mainCam != null)
                {
                    mainCam.tag = "MainCamera"; // 카메라가 있으면 태그 설정
                    Debug.Log("플레이어 카메라를 'MainCamera'로 설정 완료!");
                }
                else
                {
                    Debug.LogError("'MainCamera' 태그가 있지만 카메라 컴포넌트가 없습니다!");
                }
            }
            else
            {
                Debug.LogError(" 씬 내에 'MainCamera' 태그가 있는 카메라 오브젝트가 없습니다!");
            }
        }
        else
        {
            Debug.Log("Main Camera 정상 감지됨!");
            Debug.Log("[SUCCESS] 감지된 Main Camera: " + Camera.main.name);
        }


    }
}
