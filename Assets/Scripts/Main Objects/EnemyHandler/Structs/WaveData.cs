using UnityEngine;

public class WaveData
{
    public int MaxEnemyAmount;
    private int m_enemyAmount;
    public int EnemyAmount 
    {
        get => m_enemyAmount;
        set { m_enemyAmount = value; m_enemyAmount = Mathf.Clamp(m_enemyAmount, 0, MaxEnemyAmount); }
    }
    
    private const int m_maxHealthAmount = 999;
    private const int m_minHealthAmount = 1;
    
    private int m_healths;
    public int Healths
    {
        get => m_healths;
        set { m_healths = value; m_healths = Mathf.Clamp(m_healths, m_minHealthAmount, m_maxHealthAmount); }
    }

    private const float m_maxMoveSpeed = 30f;
    public float m_minMoveSpeed = 1f;
    public float MinMoveSpeed
    {   get => m_minMoveSpeed;
        set { m_minMoveSpeed = value; m_minMoveSpeed = Mathf.Clamp(m_minMoveSpeed, 0.001f, m_maxMoveSpeed); } }
    
    private float m_moveSpeeds;
    public float MoveSpeeds
    {
        get => m_moveSpeeds;
        set { m_moveSpeeds = value; m_moveSpeeds = Mathf.Clamp(m_moveSpeeds, MinMoveSpeed, m_maxMoveSpeed); }
    }

    private const float m_maxFireRate = 30f;
    public float MaxFireRate => m_maxFireRate;
    private float m_minFireRate = 2f;
    public float MinFireRate
    { get => m_minFireRate;
    set { m_minFireRate = value; m_minFireRate = Mathf.Clamp(m_minFireRate, 0.001f, m_maxFireRate); } }
    
    private float m_fireRates;
    public float FireRates
    {
        get => m_fireRates;
        set { m_fireRates = value; m_fireRates = Mathf.Clamp(m_fireRates, MinFireRate, m_maxFireRate); }
    }

    public BulletData Bullets;

    public WaveData(int maxAmount, int amount, int healths, float moveSpeeds, float fireRates, BulletData bullets)
    {
        MaxEnemyAmount = maxAmount;
        m_enemyAmount = Mathf.Clamp(amount, 0, MaxEnemyAmount);
        m_healths = Mathf.Clamp(healths, m_minHealthAmount, m_maxHealthAmount);
        m_moveSpeeds = Mathf.Clamp(moveSpeeds, m_minMoveSpeed, m_maxMoveSpeed);
        m_fireRates = Mathf.Clamp(fireRates, m_minFireRate, m_maxFireRate);
        Bullets = bullets;
    }

    public static WaveData Default() => new(EnemyHandler.MaxEnemyAmount, 0, 1, 5, 15, BulletData.Default());
}