using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ScoreSystem_Manager : MonoBehaviour
{
    public static ScoreSystem_Manager Instance; // 싱글톤 인스턴스

    [SerializeField] private TextMeshProUGUI scoreTmp = null; // 게임의 총 점수 텍스트
    [SerializeField] private TextMeshProUGUI bonusTmp = null; // 게임의 총 보너스 텍스트
    [SerializeField] private Score_Scripts baseScoreClass = null; // 스코어 스크립트 프리팹
    [SerializeField] private Animation scoreRenewAnim = null; // 애니메이션 컴포넌트
    [SerializeField] private Animation bonusRenewAnim = null; // 애니메이션 컴포넌트
    private List<ScoreData> scoreDataList; // 스코어 데이터 리스트

    private int totalScore; // 총 점수
    private float totalBonus; // 총 보너스

    // 초기화 함수
    public void Init_Func()
    {
        Instance = this; // 싱글톤 인스턴스 설정
        scoreDataList = new List<ScoreData>(); // 스코어 데이터 리스트 초기화
    }

    // 스코어 시스템 활성화 함수
    public void Activate_Func()
    {
        this.scoreTmp.text = "0"; // 총 점수 초기화
        this.bonusTmp.text = "Bonus 0%"; // 총 보너스 초기화

        StartCoroutine(OnScore_Cor()); // 스코어 애니메이션 코루틴 시작
    }

    // 스코어 애니메이션 코루틴
    private IEnumerator OnScore_Cor()
    {
        while (true)
        {
            if (this.scoreDataList.Count > 0)
            {
                ScoreData _scoreData = this.scoreDataList[0];

                // 스코어 스크립트 인스턴스 생성 및 설정
                Score_Scripts _scoreClass = GameObject.Instantiate<Score_Scripts>(this.baseScoreClass);
                _scoreClass.Activate_Func(_scoreData.str, _scoreData.color);
                _scoreClass.transform.position = _scoreData.pos;

                this.scoreDataList.RemoveAt(0); // 처리된 스코어 데이터 제거

                yield return new WaitForSeconds(DataBase_Manager.Instance.scorePopInterval); // 다음 스코어 팝업까지 대기
            }
            else
            {
                yield return null; // 스코어 데이터가 없으면 다음 프레임까지 대기
            }
        }
    }

    // 점수 추가 함수
    public void AddScore_Func(int _score, Vector2 _scorePos, bool _isCalcBonus = true)
    {
        // 스코어 데이터 생성 및 추가
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = _score.ToString("N0");
        _scoreData.color = DataBase_Manager.Instance.scoreColor;
        _scoreData.pos = _scorePos;
        scoreDataList.Add(_scoreData);

        this.totalScore += _score; // 총 점수에 추가

        this.scoreTmp.text = this.totalScore.ToString("N0"); // 총 점수 텍스트 업데이트
        this.scoreRenewAnim.Play(); // 총 점수 애니메이션 재생

        if (_isCalcBonus) // 보너스 점수 계산
        {
            int _bonusScore = (int)(_score * this.totalBonus);

            if (_bonusScore > 0)
                this.AddScore_Func(_bonusScore, _scorePos, false); // 보너스 점수 추가
        }
    }

    // 보너스 추가 함수
    public void AddBonus_Func(float _bonus, Vector2 _bonusPos, bool isFlatform)
    {
        // 보너스 데이터 생성 및 추가
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = "Bonus " + _bonus.ToString("P0");
        _scoreData.color = isFlatform ? DataBase_Manager.Instance.platformBonusColor : DataBase_Manager.Instance.ItemBonusColor;
        _scoreData.pos = _bonusPos;
        scoreDataList.Add(_scoreData);

        this.totalBonus += _bonus; // 총 보너스에 추가

        this.bonusTmp.text = "Bonus " + this.totalBonus.ToString("P0").Replace(" ", ""); // 총 보너스 텍스트 업데이트
        this.bonusRenewAnim.Play(); // 총 보너스 애니메이션 재생
    }

    // 보너스 초기화 함수
    public void OnResetBonus_Func(Vector2 _pos)
    {
        // 보너스 초기화 데이터 생성 및 추가
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = "Bonus Reset...";
        _scoreData.color = DataBase_Manager.Instance.platformBonusColor;
        _scoreData.pos = _pos;
        scoreDataList.Add(_scoreData);

        this.totalBonus = 0f; // 총 보너스 초기화

        this.bonusTmp.text = "Bonus " + this.totalBonus.ToString("P0").Replace(" ", ""); // 총 보너스 텍스트 업데이트
        this.bonusRenewAnim.Play(); // 총 보너스 애니메이션 재생
    }

    // 스코어 데이터 구조체
    private struct ScoreData
    {
        public string str; // 점수 문자열
        public Color color; // 점수 색상
        public Vector2 pos; // 점수 위치
    }
}