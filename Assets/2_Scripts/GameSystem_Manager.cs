using UnityEngine;

public class GameSystem_Manager : MonoBehaviour
{
    public static GameSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private DataBase_Manager dataBase_Manager = null; // �����ͺ��̽� �Ŵ���
    [SerializeField] private Player_Scripts playerClass = null; // �÷��̾� ��ũ��Ʈ
    [SerializeField] private PlatformSystem_Manager platformSystem_Manager = null; // �÷��� �ý��� �Ŵ���
    [SerializeField] private CameraSystem_Manager cameraSystem_Manager = null; // ī�޶� �ý��� �Ŵ���
    [SerializeField] private ScoreSystem_Manager scoreSystem_Manager = null; // ���ھ� �ý��� �Ŵ���
    [SerializeField] private SoundSystem_Manager soundSystem_Manager = null; // ���� �ý��� �Ŵ���
    [SerializeField] private SpriteRenderer bgSrdr = null; // ��� ��������Ʈ ������
    [SerializeField] private GameObject retryBtnObj = null; // ��õ� ��ư ��ü

    // �÷��̾��� x ��ġ�� ��ȯ�ϴ� ������Ƽ
    public float GetPlayerPosX => this.playerClass.transform.position.x;

    // ���� ���� �� ȣ��Ǵ� �Լ�
    private void Awake()
    {
        Application.targetFrameRate = 60; // ������ ����Ʈ ����
        QualitySettings.vSyncCount = 0; // V-Sync ���� (������ ������ ��Ȯ�ϰ� �����Ϸ��� �ʿ�)

        Instance = this; // �̱��� �ν��Ͻ� ����

        this.dataBase_Manager.Init_Func(); // �����ͺ��̽� �ʱ�ȭ

        this.playerClass.Init_Func(); // �÷��̾� �ʱ�ȭ
        this.platformSystem_Manager.Init_Func(); // �÷��� �ý��� �ʱ�ȭ
        this.cameraSystem_Manager.Init_Func(); // ī�޶� �ý��� �ʱ�ȭ
        this.scoreSystem_Manager.Init_Func(); // ���ھ� �ý��� �ʱ�ȭ
        this.soundSystem_Manager.Init_Func(); // ���� �ý��� �ʱ�ȭ
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    private void Start()
    {
        this.platformSystem_Manager.Activate_Func(); // �÷��� �ý��� Ȱ��ȭ
        this.scoreSystem_Manager.Activate_Func(); // ���ھ� �ý��� Ȱ��ȭ
        this.playerClass.Activate_Func(); // �÷��̾� Ȱ��ȭ

        SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.Main); // ���� ������� ���
    }

    // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�
    public void Update()
    {
        float _cameraPosX = CameraSystem_Manager.Instance.transform.position.x;
        this.bgSrdr.transform.position = new Vector2(_cameraPosX, this.bgSrdr.transform.position.y); // ��� ��ġ ������Ʈ
        this.bgSrdr.size = new Vector2(30f +_cameraPosX * 2f, this.bgSrdr.size.y); // ��� ũ�� ������Ʈ
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    public void OnGameOver_Func()
    {
        this.retryBtnObj.SetActive(true); // ��õ� ��ư Ȱ��ȭ
    }

    // ��õ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void Callbtn_Retry_Func()
    {
        if (this.retryBtnObj.activeSelf)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // �� �ٽ� �ε�
    }
}