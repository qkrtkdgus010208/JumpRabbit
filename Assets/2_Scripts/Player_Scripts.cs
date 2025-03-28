using UnityEngine;

public class Player_Scripts : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid = null; // 플레이어의 Rigidbody2D 컴포넌트
    [SerializeField] private Animator anim = null; // 플레이어의 Animator 컴포넌트
    private float currentJumpPower = 1f; // 현재 점프 파워
    private Platform_Scripts landingPlatformClass; // 착지한 플랫폼 클래스
    private bool isFirstLanding = true; // 처음 착지 여부
    private bool isJumpReady; // 점프 준비 상태

    // 초기화 함수
    public void Init_Func()
    {
        // 초기화 로직 (필요 시 추가)
    }

    // 플레이어 활성화 함수
    public void Activate_Func()
    {
        isFirstLanding = true; // 처음 착지 여부 초기화
    }

    // 매 프레임마다 호출되는 업데이트 함수
    private void Update()
    {
        if (!this.isJumpReady)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 캐릭터가 준비 자세를 취함
            {
                this.isJumpReady = true; // 점프 준비 상태로 설정
                this.anim.SetInteger("StateID", 1); // 준비 자세 애니메이션 설정
            }
        }
        else
        {
            this.currentJumpPower += DataBase_Manager.Instance.jumpPower * Time.deltaTime; // 점프 파워 증가

            if (this.currentJumpPower < DataBase_Manager.Instance.maxJumpPower)
            {
                if (Input.GetKeyUp(KeyCode.Space)) // 스페이스바를 떼면 점프
                {
                    if (this.currentJumpPower < DataBase_Manager.Instance.minJumpPower)
                    {
                        this.SetState_Idle_Func(); // 점프 파워가 최소값보다 작으면 기본 상태로 설정
                    }
                    else
                    {
                        this.isJumpReady = false; // 점프 준비 상태 해제
                        this.rigid.AddForce(Vector2.one * 5f * this.currentJumpPower); // 점프 힘을 가함
                        this.currentJumpPower = 0; // 점프 파워 초기화
                        this.anim.SetInteger("StateID", 2); // 점프 애니메이션 설정

                        // 랜덤으로 점프 사운드 재생
                        SfxType _sfxType = Random.value < 0.5f ? SfxType.Jump1 : SfxType.Jump2;
                        SoundSystem_Manager.Instance.PlaySfx_Func(_sfxType);

                        // 점프 이펙트 생성
                        Effect_Scripts _effClass = GameObject.Instantiate<Effect_Scripts>(DataBase_Manager.Instance.effClass);
                        _effClass.Activate_Func(this.transform.position);
                    }
                }
            }
            else
            {
                this.SetState_Idle_Func(); // 점프 파워가 최대값을 초과하면 기본 상태로 설정
            }
        }

        // 플레이어가 일정 높이 이하로 떨어지면 게임 오버 처리
        Vector3 _playerPos = this.transform.position;
        if (_playerPos.y < DataBase_Manager.Instance.gameOverConditionHeight)
        {
            GameSystem_Manager.Instance.OnGameOver_Func();
        }
    }

    // 2D 충돌 시 호출되는 함수
    private void OnCollisionEnter2D(Collision2D _col)
    {
        this.SetState_Idle_Func(); // 충돌 시 기본 상태로 설정

        if (_col.transform.TryGetComponent(out Platform_Scripts _platformClass))
        {
            Vector2 _targetPos = new Vector2(this.transform.position.x + 5f, _platformClass.transform.position.y + 2.8f); // 카메라 위치 세부 조정을 위한 값 추가
            CameraSystem_Manager.Instance.OnFollow_Func(_targetPos); // 카메라 위치 조정

            if (isFirstLanding) // 맨 처음 플랫폼에 착지한 경우 점수를 추가하지 않음
            {
                isFirstLanding = false; // 처음 착지 여부를 false로 변경
                return;
            }

            if (this.landingPlatformClass != _platformClass) // 새로운 플랫폼에 착지한 경우
            {
                this.landingPlatformClass = _platformClass;

                // 보너스 추가
                ScoreSystem_Manager.Instance.AddBonus_Func(DataBase_Manager.Instance.bonusValue, this.transform.position, true);
            }
            else // 같은 플랫폼에 착지한 경우
            {
                // 보너스 초기화
                ScoreSystem_Manager.Instance.OnResetBonus_Func(this.transform.position);
                return;
            }

            _platformClass.Onlanding_Func(); // 플랫폼 착지 함수 호출
        }
    }

    // 플레이어 상태를 기본 상태로 설정하는 함수
    private void SetState_Idle_Func()
    {
        this.isJumpReady = false; // 점프 준비 상태 해제
        this.rigid.linearVelocity = Vector2.zero; // 충돌 시 캐릭터를 멈춤
        this.anim.SetInteger("StateID", 0); // 기본 자세로 애니메이션 설정
        this.currentJumpPower = 0f; // 점프 파워 초기화
    }
}




