using System.Collections;
using UnityEngine;

public class CameraSystem_Manager : MonoBehaviour
{
    public static CameraSystem_Manager Instance; // 싱글톤 인스턴스

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정
    }

    // 카메라가 특정 위치를 따라가도록 하는 함수
    public void OnFollow_Func(Vector2 _targetPos)
    {
        StartCoroutine(OnFollow_Cor(_targetPos)); // 코루틴 실행
    }

    // 카메라를 이동시키는 코루틴
    private IEnumerator OnFollow_Cor(Vector2 _targetPos)
    {
        // 카메라와 목표 위치의 거리가 일정 거리보다 작아질 때까지 반복
        while (DataBase_Manager.Instance.arriveDist <= Vector3.Distance(this.transform.position, _targetPos))
        {
            // 카메라의 위치를 목표 위치로 부드럽게 이동시킴
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, Time.deltaTime * DataBase_Manager.Instance.followSpeed);

            yield return null; // 다음 프레임까지 대기
        }
    }
}