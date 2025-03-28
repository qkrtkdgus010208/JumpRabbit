using UnityEngine;

public class Item_Scripts : MonoBehaviour
{
    // 아이템 활성화 함수
    public void Activate_Func(Vector2 _pos, float _rangeX)
    {
        // _rangeX 범위 내에서 랜덤한 x 위치를 계산하고, y 위치는 _pos.y + 0.85f로 설정
        float _x = Random.Range(-_rangeX, _rangeX) + _pos.x;
        this.transform.position = new Vector2(_x, _pos.y + 0.85f); // 아이템 위치 설정
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D _col)
    {
        // 충돌한 객체가 Player_Scripts 컴포넌트를 가지고 있는지 확인
        if (_col.transform.TryGetComponent(out Player_Scripts _playerClass))
        {
            // 보너스 증가
            float _itemBonus = DataBase_Manager.Instance.itemBonus; // 아이템 보너스 값 가져오기
            Vector2 _thisPos = this.transform.position; // 현재 아이템 위치
            ScoreSystem_Manager.Instance.AddBonus_Func(_itemBonus, _thisPos, false); // 보너스 추가 함수 호출

            // 아이템 객체 파괴
            GameObject.Destroy(this.gameObject);
        }
    }
}
