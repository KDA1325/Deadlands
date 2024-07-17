using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const string nullToken = "null";
    public const string googleToken = "Google";
    public const string appleToken = "Apple";

    public enum ObjectType
    {
        None,
        Player,
        Enemy,
        Bullet,
    }

    public enum Scene
    {
        None,
        GameScene,
        MainScene,
        Title,
    }

    public enum Layer
    {
        None,
        Player = 3,
        Enemy = 6,
        Bullet = 7,
        Effect,
        SingleEffect,
        WorldsEdge = 31,
    }

    public enum PlayerInGameStat
    {
        MaxHp,
        Att_Range,
        Max_Exp,
        Att_Damage,
        Att_Speed,
        CriPercent,
        CriDamage,
        Defense
    }

    public enum OutGameStat
    {
        PlayerAttDamage = 5,
        PlayerAttSpeed,
        PlayerAttRange,
        PlayerCriPercent,
        PlayerCriDamage,
        MultiShotPercent,
        MultiShotCount,
        DualShotPercent,
        DualShotCount,
        PlayerHp,
        PlayerHpRecover,
        LifeDrainPercent,
        PlayerReflectPercent,
        PlayerEnvasionPercent,
        PlayerPushPercent,
        PlayerResurrectionPercent,
        CoinObtainPercent,
        CoinObtainRatio,
        ExpObtainRatio,
        PlaySpeedUp,
        CoinInterestPercent,
        FlameDamage,
        FreezeSlowEffect,
        BoomDamage,
        RangedTurretDamage,
    }

    public enum SaveFile
    {
        AttUnlock,
        DefUnlock,
        UtilUnlock,
        Gold,
        Diamond,
        PlayerAttDamage,
        PlayerAttSpeed,
        PlayerAttRange,
        PlayerCriPercent,
        PlayerCriDamage,
        MultiShotPercent,
        MultiShotCount,
        DualShotPercent,
        DualShotCount,
        PlayerHp,
        PlayerHpRecover,
        PlayerVampirPercent,
        PlayerReflectPercent,
        PlayerEnvasionPercent,
        PlayerPushPercent,
        PlayerResurrectionPercent,
        CoinObtainPercent,
        CoinObtainRatio,
        ExpObtainRatio,
        PlaySpeedUp,
        CoinInterestPercent,
        ClearStage,
        MaxStage1,
        MaxStage2,
        MaxStage3,
        MaxStage4,
        MaxStage5,
        MaxStage6,



        End
    }

    public enum Stage
    {
        Gold,
        StageLevel,
        MaxWave,
    }

    public enum AfterTreatmentStat
    {
        AcquiredGold,
        ClearWave
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
    }

    public enum Effect
    {
        Auto,
        Menual
    }

    public enum EnemyType
    {
        A = 1,
        B,
        C,
        D,
        Thief,
        Sniper
    }
    public enum Federation
    {
        None = 0,
        Google,
        Apple
    }

}