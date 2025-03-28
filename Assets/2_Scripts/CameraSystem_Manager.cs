using System.Collections;
using UnityEngine;

public class CameraSystem_Manager : MonoBehaviour
{
    public static CameraSystem_Manager Instance; // �̱��� �ν��Ͻ�

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
    }

    // ī�޶� Ư�� ��ġ�� ���󰡵��� �ϴ� �Լ�
    public void OnFollow_Func(Vector2 _targetPos)
    {
        StartCoroutine(OnFollow_Cor(_targetPos)); // �ڷ�ƾ ����
    }

    // ī�޶� �̵���Ű�� �ڷ�ƾ
    private IEnumerator OnFollow_Cor(Vector2 _targetPos)
    {
        // ī�޶�� ��ǥ ��ġ�� �Ÿ��� ���� �Ÿ����� �۾��� ������ �ݺ�
        while (DataBase_Manager.Instance.arriveDist <= Vector3.Distance(this.transform.position, _targetPos))
        {
            // ī�޶��� ��ġ�� ��ǥ ��ġ�� �ε巴�� �̵���Ŵ
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, Time.deltaTime * DataBase_Manager.Instance.followSpeed);

            yield return null; // ���� �����ӱ��� ���
        }
    }
}