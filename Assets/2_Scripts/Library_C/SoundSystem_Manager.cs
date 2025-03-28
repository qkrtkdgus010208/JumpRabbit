using UnityEngine;

public class SoundSystem_Manager : MonoBehaviour
{
    public static SoundSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private AudioSource bgmAS = null; // �������(AudioSource) ������Ʈ
    [SerializeField] private AudioSource sfxAS = null; // ȿ����(AudioSource) ������Ʈ

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
    }

    // ������� ��� �Լ�
    public void PlayBgm_Func(BgmType _bgmType)
    {
        // BGM �����͸� ������
        DataBase_Manager.BgmData _bgmData = DataBase_Manager.Instance.GetBgmData_Func(_bgmType);
        this.bgmAS.clip = _bgmData.clip; // BGM Ŭ�� ����
        this.bgmAS.volume = _bgmData.volume; // BGM ���� ����
        this.bgmAS.Play(); // BGM ���
    }

    // ȿ���� ��� �Լ�
    public void PlaySfx_Func(SfxType _sfxType)
    {
        // SFX �����͸� ������
        DataBase_Manager.SfxData _sfxData = DataBase_Manager.Instance.GetSfxData_Func(_sfxType);
        this.sfxAS.volume = _sfxData.volume; // SFX ���� ����
        this.sfxAS.PlayOneShot(_sfxData.clip); // SFX ���
    }
}
