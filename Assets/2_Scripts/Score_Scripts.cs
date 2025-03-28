using TMPro;
using UnityEngine;

public class Score_Scripts : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp = null; // 점수를 표시하는 TextMeshPro 컴포넌트

    // 점수 활성화 함수
    public void Activate_Func(string _str, Color _color)
    {
        this.tmp.text = _str; // 점수 텍스트 설정
        this.tmp.color = _color; // 점수 색상 설정
    }

    // 점수 비활성화 함수
    public void Deactivate_Func()
    {
        GameObject.Destroy(this.gameObject); // 점수 객체 파괴
    }

    // 애니메이션 종료 시 호출되는 함수 (점수 비활성화 함수 호출)
    public void CallAni_Deactivate_Func()
    {
        this.Deactivate_Func(); // 점수 비활성화 함수 호출
    }
}