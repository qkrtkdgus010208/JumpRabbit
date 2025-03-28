using System.Collections.Generic;
using UnityEngine;

public class PlatformSystem_Manager : MonoBehaviour
{
    public static PlatformSystem_Manager Instance; // �̱��� �ν��Ͻ�

    [SerializeField] private Transform spawnPosTrf = null; // �÷��� ���� ��ġ
    private Dictionary<int, Platform_Scripts[]> platformClassArrDic; // �÷��� Ŭ���� �迭 ��ųʸ�
    private int platformNum = 0; // ������ �÷��� ��
    private Vector2 spawnPos; // ���� ��ġ

    // �ʱ�ȭ �Լ�
    public void Init_Func()
    {
        Instance = this; // �̱��� �ν��Ͻ� ����

        this.platformClassArrDic = new Dictionary<int, Platform_Scripts[]>(); // ��ųʸ� �ʱ�ȭ

        // �÷��� Ŭ���� �迭 ��ųʸ��� �߰�
        this.platformClassArrDic.Add(0, DataBase_Manager.Instance.smallPlatformClassArr);
        this.platformClassArrDic.Add(1, DataBase_Manager.Instance.middlePlatformClassArr);
        this.platformClassArrDic.Add(2, DataBase_Manager.Instance.largePlatformClassArr);
    }

    // �÷��� �ý��� Ȱ��ȭ �Լ�
    public void Activate_Func()
    {
        this.spawnPos = this.spawnPosTrf.position; // ó�� ��ġ�� ����

        for (int i = 0; i < DataBase_Manager.Instance.spawnMinNum; i++)
        {
            OnSpawn_Func(); // �÷��� ���� �Լ� ȣ��
        }
    }

    // �÷��� ���� �Լ�    
    private void OnSpawn_Func()
    {
        int _platformID = -1;

        // ���ǿ� �´� �÷��� ID�� ã��
        foreach (var _data in DataBase_Manager.Instance.dataArr)
        {
            if (_data.TryGetPlatformID_Func(this.platformNum, out _platformID))
                break;
        }

        Platform_Scripts[] _platformClassArr = this.platformClassArrDic[_platformID]; // �÷��� Ŭ���� �迭 ��������

        int _randID = Random.Range(0, _platformClassArr.Length); // �������� �÷��� ����
        Platform_Scripts _randPlatformClass = _platformClassArr[_randID];

        Platform_Scripts _platformClass = GameObject.Instantiate<Platform_Scripts>(_randPlatformClass); // �÷��� �ν��Ͻ� ����

        if (0 < this.platformNum)
            spawnPos += Vector2.right * _platformClass.GetHalfSizeX; // ���� �÷����� ���� ũ�⸸ŭ ��ġ �̵�

        _platformClass.Activate_Func(this.spawnPos, this.platformNum == 0); // �÷��� Ȱ��ȭ

        float _gap = Random.Range(DataBase_Manager.Instance.gapIntervalMin, DataBase_Manager.Instance.gapIntervalMax); // �÷��� ���� ����

        spawnPos += new Vector2(_gap + _platformClass.GetHalfSizeX, 0f); // ���� �÷��� ��ġ ����

        this.platformNum++; // ������ �÷��� �� ����
    }

    // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �Լ�
    private void Update()
    {
        // �÷��̾ ���� ��ġ�� �����ϸ� ���ο� �÷����� ����
        if (this.spawnPos.x - DataBase_Manager.Instance.platformSpawnConditionGapPosX < GameSystem_Manager.Instance.GetPlayerPosX)
        {
            this.OnSpawn_Func();
        }
    }

    // �÷��� ������ Ŭ����
    [System.Serializable]
    public class Data
    {
        [SerializeField] private int conditionNum; // ���� ��ȣ
        [SerializeField] private float[] percentArr = new float[3]; // Ȯ�� �迭

        // �÷��� ID�� �õ��Ͽ� �������� �Լ�
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