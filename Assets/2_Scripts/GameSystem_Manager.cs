using UnityEngine;

public class GameSystem_Manager : MonoBehaviour
{
    public static GameSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private DataBase_Manager dataBase_Manager = null; // 데이터베이스 매니저
    [SerializeField] private Player_Scripts playerClass = null; // 플레이어 스크립트
    [SerializeField] private PlatformSystem_Manager platformSystem_Manager = null; // 플랫폼 시스템 매니저
    [SerializeField] private CameraSystem_Manager cameraSystem_Manager = null; // 카메라 시스템 매니저
    [SerializeField] private ScoreSystem_Manager scoreSystem_Manager = null; // 스코어 시스템 매니저
    [SerializeField] private SoundSystem_Manager soundSystem_Manager = null; // 사운드 시스템 매니저
    [SerializeField] private SpriteRenderer bgSrdr = null; // 배경 스프라이트 렌더러
    [SerializeField] private GameObject retryBtnObj = null; // 재시도 버튼 객체

    // 플레이어의 x 위치를 반환하는 프로퍼티
    public float GetPlayerPosX => this.playerClass.transform.position.x;

    // 게임 시작 시 호출되는 함수
    private void Awake()
    {
        Application.targetFrameRate = 60; // 프레임 레이트 설정
        QualitySettings.vSyncCount = 0; // V-Sync 끄기 (프레임 제한을 정확하게 적용하려면 필요)

        Instance = this; // 싱글톤 인스턴스 설정

        this.dataBase_Manager.Init_Func(); // 데이터베이스 초기화

        this.playerClass.Init_Func(); // 플레이어 초기화
        this.platformSystem_Manager.Init_Func(); // 플랫폼 시스템 초기화
        this.cameraSystem_Manager.Init_Func(); // 카메라 시스템 초기화
        this.scoreSystem_Manager.Init_Func(); // 스코어 시스템 초기화
        this.soundSystem_Manager.Init_Func(); // 사운드 시스템 초기화
    }

    // 게임 시작 후 호출되는 함수
    private void Start()
    {
        this.platformSystem_Manager.Activate_Func(); // 플랫폼 시스템 활성화
        this.scoreSystem_Manager.Activate_Func(); // 스코어 시스템 활성화
        this.playerClass.Activate_Func(); // 플레이어 활성화

        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Main); // 메인 배경음악 재생
    }

    // 매 프레임마다 호출되는 업데이트 함수
    public void Update()
    {
        float _cameraPosX = CameraSystem_Manager.Instance.transform.position.x;
        this.bgSrdr.transform.position = new Vector2(_cameraPosX, this.bgSrdr.transform.position.y); // 배경 위치 업데이트
        this.bgSrdr.size = new Vector2(30f +_cameraPosX * 2f, this.bgSrdr.size.y); // 배경 크기 업데이트
    }

    // 게임 오버 시 호출되는 함수
    public void OnGameOver_Func()
    {
        this.retryBtnObj.SetActive(true); // 재시도 버튼 활성화
    }

    // 재시도 버튼 클릭 시 호출되는 함수
    public void Callbtn_Retry_Func()
    {
        if (this.retryBtnObj.activeSelf)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // 씬 다시 로드
    }
}