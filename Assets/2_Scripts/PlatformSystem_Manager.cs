using System.Collections.Generic;
using UnityEngine;

public class PlatformSystem_Manager : MonoBehaviour
{
    public static PlatformSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private Transform spawnPosTrf = null; // 플랫폼 스폰 위치
    private Dictionary<int, Platform_Scripts[]> platformClassArrDic; // 플랫폼 클래스 배열 딕셔너리
    private int platformNum = 0; // 생성된 플랫폼 수
    private Vector2 spawnPos; // 스폰 위치

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정

        this.platformClassArrDic = new Dictionary<int, Platform_Scripts[]>(); // 딕셔너리 초기화

        // 플랫폼 클래스 배열 딕셔너리에 추가
        this.platformClassArrDic.Add(0, DataBase_Manager.Instance.smallPlatformClassArr);
        this.platformClassArrDic.Add(1, DataBase_Manager.Instance.middlePlatformClassArr);
        this.platformClassArrDic.Add(2, DataBase_Manager.Instance.largePlatformClassArr);
    }

    // 플랫폼 시스템 활성화 함수
    public void Activate_Func()
    {
        this.spawnPos = this.spawnPosTrf.position; // 처음 위치를 저장

        for (int i = 0; i < DataBase_Manager.Instance.spawnMinNum; i++)
        {
            OnSpawn_Func(); // 플랫폼 스폰 함수 호출
        }
    }

    // 플랫폼 스폰 함수    
    private void OnSpawn_Func()
    {
        int _platformID = -1;

        // 조건에 맞는 플랫폼 ID를 찾음
        foreach (var _data in DataBase_Manager.Instance.dataArr)
        {
            if (_data.TryGetPlatformID_Func(this.platformNum, out _platformID))
                break;
        }

        Platform_Scripts[] _platformClassArr = this.platformClassArrDic[_platformID]; // 플랫폼 클래스 배열 가져오기

        int _randID = Random.Range(0, _platformClassArr.Length); // 랜덤으로 플랫폼 선택
        Platform_Scripts _randPlatformClass = _platformClassArr[_randID];

        Platform_Scripts _platformClass = GameObject.Instantiate<Platform_Scripts>(_randPlatformClass); // 플랫폼 인스턴스 생성

        if (0 < this.platformNum)
            spawnPos += Vector2.right * _platformClass.GetHalfSizeX; // 이전 플랫폼의 절반 크기만큼 위치 이동

        _platformClass.Activate_Func(this.spawnPos, this.platformNum == 0); // 플랫폼 활성화

        float _gap = Random.Range(DataBase_Manager.Instance.gapIntervalMin, DataBase_Manager.Instance.gapIntervalMax); // 플랫폼 간격 설정

        spawnPos += new Vector2(_gap + _platformClass.GetHalfSizeX, 0f); // 다음 플랫폼 위치 설정

        this.platformNum++; // 생성된 플랫폼 수 증가
    }

    // 매 프레임마다 호출되는 업데이트 함수
    private void Update()
    {
        // 플레이어가 일정 위치에 도달하면 새로운 플랫폼을 스폰
        if (this.spawnPos.x - DataBase_Manager.Instance.platformSpawnConditionGapPosX < GameSystem_Manager.Instance.GetPlayerPosX)
        {
            this.OnSpawn_Func();
        }
    }

    // 플랫폼 데이터 클래스
    [System.Serializable]
    public class Data
    {
        [SerializeField] private int conditionNum; // 조건 번호
        [SerializeField] private float[] percentArr = new float[3]; // 확률 배열

        // 플랫폼 ID를 시도하여 가져오는 함수
        public bool TryGetPlatformID_Func(int _platformnum, out int _platformID)
        {
            if (this.conditionNum <= _platformnum)
            {
                float _randValue = Random.value;

                for (int i = 0; i < this.percentArr.Length; i++)
                {
                    if (_randValue < this.percentArr[i])
                    {
                        _platformID = i;
                        return true;
                    }
                    else
                    {
                        _randValue -= this.percentArr[i];
                    }
                }

                _platformID = 0;
                return true;
            }
            else
            {
                _platformID = -1;
                return false;
            }
        }
    }
}