using UnityEngine;

public class Platform_Scripts : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col = null; // 플랫폼의 BoxCollider2D 컴포넌트
    [SerializeField] private SpriteRenderer srdr = null; // 플랫폼의 SpriteRenderer 컴포넌트
    [SerializeField] private Animation anim = null; // 플랫폼의 Animation 컴포넌트
    [SerializeField] private int score; // 플랫폼의 점수

    // 플랫폼 x 길이의 절반을 반환하는 프로퍼티
    public float GetHalfSizeX => this.col.size.x * 0.5f;

    // 플랫폼 위치를 지정하는 함수
    public void Activate_Func(Vector2 _pos, bool _isFirst)
    {
        this.transform.position = _pos; // 플랫폼 위치 설정

        // 첫 번째 플랫폼이 아니고, 랜덤 확률에 따라 아이템 생성
        if (!_isFirst && Random.value < DataBase_Manager.Instance.itemSpawnPer)
        {
            Item_Scripts _baseItemClass = DataBase_Manager.Instance.baseItemClass; // 기본 아이템 클래스
            Item_Scripts _itemClass = GameObject.Instantiate<Item_Scripts>(_baseItemClass); // 아이템 인스턴스 생성
            _itemClass.Activate_Func(this.transform.position, this.GetHalfSizeX); // 아이템 활성화
        }
    }

    // 해당 플랫폼의 점수만큼을 추가하라고 알리는 함수
    public void Onlanding_Func()
    {
        ScoreSystem_Manager.Instance.AddScore_Func(this.score, this.transform.position); // 점수 추가

        this.anim.Play(); // 애니메이션 재생
    }
}