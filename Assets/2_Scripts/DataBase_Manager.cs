using System.Collections.Generic;
using UnityEngine;

public partial class DataBase_Manager : DB_Manager
{
    [Header("연출")]
    public Color scoreColor; // 점수 색상
    public Color platformBonusColor; // 플랫폼 보너스 색상
    public Color ItemBonusColor; // 아이템 보너스 색상
    public float scorePopInterval = 0.15f; // 점수 팝업 간격
    public Effect_Scripts effClass = null; // 이펙트 클래스

    [Header("플레이어")]
    public float jumpPower = 300f; // 점프 파워
    public float gameOverConditionHeight = -20f; // 게임 오버 조건 높이
    public float minJumpPower = 80f; // 최소 점프 파워
    public float maxJumpPower = 200f; // 최대 점프 파워

    [Header("플랫폼")]
    public Platform_Scripts[] largePlatformClassArr = null; // 큰 플랫폼 배열
    public Platform_Scripts[] middlePlatformClassArr = null; // 중간 플랫폼 배열
    public Platform_Scripts[] smallPlatformClassArr = null; // 작은 플랫폼 배열
    public PlatformSystem_Manager.Data[] dataArr = null; // 플랫폼 데이터 배열
    public float gapIntervalMin = 0.5f; // 플랫폼 간 최소 간격
    public float gapIntervalMax = 1.5f; // 플랫폼 간 최대 간격
    public float bonusValue = 0.05f; // 보너스 값
    public int spawnMinNum = 5; // 최소 스폰 수
    public float platformSpawnConditionGapPosX = 20f; // 플랫폼 스폰 조건 간격

    [Header("카메라")]
    public float followSpeed = 5f; // 카메라 따라가는 속도
    public float arriveDist = 0.1f; // 도착 거리

    [Header("아이템")]
    public Item_Scripts baseItemClass; // 기본 아이템 클래스
    public float itemSpawnPer = 0.2f; // 아이템 스폰 확률
    public float itemBonus = 0.25f; // 아이템 보너스 값
}

// 배경음악 타입 열거형
public enum BgmType
{
    None = 0,
    Main = 10,
}

// 효과음 타입 열거형
public enum SfxType
{
    None = 0,
    Jump1 = 10,
    Jump2 = 20,
}