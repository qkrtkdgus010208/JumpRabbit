using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ScoreSystem_Manager : MonoBehaviour
{
    public static ScoreSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private TextMeshProUGUI scoreTmp = null; // ������ �� ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI bonusTmp = null; // ������ �� ���ʽ� �ؽ�Ʈ
    [SerializeField] private Score_Scripts baseScoreClass = null; // ���ھ� ��ũ��Ʈ ������
    [SerializeField] private Animation scoreRenewAnim = null; // �ִϸ��̼� ������Ʈ
    [SerializeField] private Animation bonusRenewAnim = null; // �ִϸ��̼� ������Ʈ
    private List<ScoreData> scoreDataList; // ���ھ� ������ ����Ʈ

    private int totalScore; // �� ����
    private float totalBonus; // �� ���ʽ�

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����
        scoreDataList = new List<ScoreData>(); // ���ھ� ������ ����Ʈ �ʱ�ȭ
    }

    // ���ھ� �ý��� Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        this.scoreTmp.text = "0"; // �� ���� �ʱ�ȭ
        this.bonusTmp.text = "Bonus 0%"; // �� ���ʽ� �ʱ�ȭ

        StartCoroutine(OnScore_Cor()); // ���ھ� �ִϸ��̼� �ڷ�ƾ ����
    }

    // ���ھ� �ִϸ��̼� �ڷ�ƾ
    private IEnumerator OnScore_Cor()
    {
        while (true)
        {
            if (this.scoreDataList.Count > 0)
            {
                ScoreData _scoreData = this.scoreDataList[0];

                // ���ھ� ��ũ��Ʈ �ν��Ͻ� ���� �� ����
                Score_Scripts _scoreClass = GameObject.Instantiate<Score_Scripts>(this.baseScoreClass);
                _scoreClass.Activate_Func(_scoreData.str, _scoreData.color);
                _scoreClass.transform.position = _scoreData.pos;

                this.scoreDataList.RemoveAt(0); // ó���� ���ھ� ������ ����

                yield return new WaitForSeconds(DataBase_Manager.Instance.scorePopInterval); // ���� ���ھ� �˾����� ���
            }
            else
            {
                yield return null; // ���ھ� �����Ͱ� ������ ���� �����ӱ��� ���
            }
        }
    }

    // ���� �߰� �Լ�
    public void AddScore_Func(int _score, Vector2 _scorePos, bool _isCalcBonus = true)
    {
        // ���ھ� ������ ���� �� �߰�
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = _score.ToString("N0");
        _scoreData.color = DataBase_Manager.Instance.scoreColor;
        _scoreData.pos = _scorePos;
        scoreDataList.Add(_scoreData);

        this.totalScore += _score; // �� ������ �߰�

        this.scoreTmp.text = this.totalScore.ToString("N0"); // �� ���� �ؽ�Ʈ ������Ʈ
        this.scoreRenewAnim.Play(); // �� ���� �ִϸ��̼� ���

        if (_isCalcBonus) // ���ʽ� ���� ���
        {
            int _bonusScore = (int)(_score * this.totalBonus);

            if (_bonusScore > 0)
                this.AddScore_Func(_bonusScore, _scorePos, false); // ���ʽ� ���� �߰�
        }
    }

    // ���ʽ� �߰� �Լ�
    public void AddBonus_Func(float _bonus, Vector2 _bonusPos, bool isFlatform)
    {
        // ���ʽ� ������ ���� �� �߰�
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = "Bonus " + _bonus.ToString("P0");
        _scoreData.color = isFlatform ? DataBase_Manager.Instance.platformBonusColor : DataBase_Manager.Instance.ItemBonusColor;
        _scoreData.pos = _bonusPos;
        scoreDataList.Add(_scoreData);

        this.totalBonus += _bonus; // �� ���ʽ��� �߰�

        this.bonusTmp.text = "Bonus " + this.totalBonus.ToString("P0").Replace(" ", ""); // �� ���ʽ� �ؽ�Ʈ ������Ʈ
        this.bonusRenewAnim.Play(); // �� ���ʽ� �ִϸ��̼� ���
    }

    // ���ʽ� �ʱ�ȭ �Լ�
    public void OnResetBonus_Func(Vector2 _pos)
    {
        // ���ʽ� �ʱ�ȭ ������ ���� �� �߰�
        ScoreData _scoreData = new ScoreData();
        _scoreData.str = "Bonus Reset...";
        _scoreData.color = DataBase_Manager.Instance.platformBonusColor;
        _scoreData.pos = _pos;
        scoreDataList.Add(_scoreData);

        this.totalBonus = 0f; // �� ���ʽ� �ʱ�ȭ

        this.bonusTmp.text = "Bonus " + this.totalBonus.ToString("P0").Replace(" ", ""); // �� ���ʽ� �ؽ�Ʈ ������Ʈ
        this.bonusRenewAnim.Play(); // �� ���ʽ� �ִϸ��̼� ���
    }

    // ���ھ� ������ ����ü
    private struct ScoreData
    {
        public string str; // ���� ���ڿ�
        public Color color; // ���� ����
        public Vector2 pos; // ���� ��ġ
    }
}