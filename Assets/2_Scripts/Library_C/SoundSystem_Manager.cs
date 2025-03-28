using UnityEngine;

public class SoundSystem_Manager : MonoBehaviour
{
    public static SoundSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private AudioSource bgmAS = null; // 배경음악(AudioSource) 컴포넌트
    [SerializeField] private AudioSource sfxAS = null; // 효과음(AudioSource) 컴포넌트

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정
    }

    // 배경음악 재생 함수
    public void PlayBgm_Func(BgmType _bgmType)
    {
        // BGM 데이터를 가져옴
        DataBase_Manager.BgmData _bgmData = DataBase_Manager.Instance.GetBgmData_Func(_bgmType);
        this.bgmAS.clip = _bgmData.clip; // BGM 클립 설정
        this.bgmAS.volume = _bgmData.volume; // BGM 볼륨 설정
        this.bgmAS.Play(); // BGM 재생
    }

    // 효과음 재생 함수
    public void PlaySfx_Func(SfxType _sfxType)
    {
        // SFX 데이터를 가져옴
        DataBase_Manager.SfxData _sfxData = DataBase_Manager.Instance.GetSfxData_Func(_sfxType);
        this.sfxAS.volume = _sfxData.volume; // SFX 볼륨 설정
        this.sfxAS.PlayOneShot(_sfxData.clip); // SFX 재생
    }
}
