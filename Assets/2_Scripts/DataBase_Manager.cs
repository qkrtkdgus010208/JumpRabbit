using System.Collections.Generic;
using UnityEngine;

public partial class DataBase_Manager : DB_Manager
{
    [Header("����")]
    public Color scoreColor; // ���� ����
    public Color platformBonusColor; // �÷��� ���ʽ� ����
    public Color ItemBonusColor; // ������ ���ʽ� ����
    public float scorePopInterval = 0.15f; // ���� �˾� ����
    public Effect_Scripts effClass = null; // ����Ʈ Ŭ����

    [Header("�÷��̾�")]
    public float jumpPower = 300f; // ���� �Ŀ�
    public float gameOverConditionHeight = -20f; // ���� ���� ���� ����
    public float minJumpPower = 80f; // �ּ� ���� �Ŀ�
    public float maxJumpPower = 200f; // �ִ� ���� �Ŀ�

    [Header("�÷���")]
    public Platform_Scripts[] largePlatformClassArr = null; // ū �÷��� �迭
    public Platform_Scripts[] middlePlatformClassArr = null; // �߰� �÷��� �迭
    public Platform_Scripts[] smallPlatformClassArr = null; // ���� �÷��� �迭
    public PlatformSystem_Manager.Data[] dataArr = null; // �÷��� ������ �迭
    public float gapIntervalMin = 0.5f; // �÷��� �� �ּ� ����
    public float gapIntervalMax = 1.5f; // �÷��� �� �ִ� ����
    public float bonusValue = 0.05f; // ���ʽ� ��
    public int spawnMinNum = 5; // �ּ� ���� ��
    public float platformSpawnConditionGapPosX = 20f; // �÷��� ���� ���� ����

    [Header("ī�޶�")]
    public float followSpeed = 5f; // ī�޶� ���󰡴� �ӵ�
    public float arriveDist = 0.1f; // ���� �Ÿ�

    [Header("������")]
    public Item_Scripts baseItemClass; // �⺻ ������ Ŭ����
    public float itemSpawnPer = 0.2f; // ������ ���� Ȯ��
    public float itemBonus = 0.25f; // ������ ���ʽ� ��
}

// ������� Ÿ�� ������
public enum BgmType
{
    None = 0,
    Main = 10,
}

// ȿ���� Ÿ�� ������
public enum SfxType
{
    None = 0,
    Jump1 = 10,
    Jump2 = 20,
}