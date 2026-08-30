using UnityEngine;

public struct WaveData
{
    public int MaxAmount;
    private int m_enemyAmount;
    public int EnemyAmount {
        readonly get => m_enemyAmount;
        set { m_enemyAmount = value; m_enemyAmount = Mathf.Clamp(m_enemyAmount, 0, MaxAmount); } }
    private int m_healths;
    public int Healths
    {
        readonly get => m_healths;
        set { m_healths = value; m_healths = Mathf.Clamp(m_healths, 1, 10); }
    }
    private float m_moveSpeeds;
    public float MoveSpeeds
    {
        readonly get => m_moveSpeeds;
        set { m_moveSpeeds = value; m_moveSpeeds = Mathf.Clamp(m_moveSpeeds, 1f, 20); }
    }
    private float m_fireRates;
    public float FireRates
    {
        readonly get => m_fireRates;
        set { m_fireRates = value; m_fireRates = Mathf.Clamp(m_fireRates, 1f, 20); }
    }
    public BulletData Bullets;

    public WaveData(int maxAmount, int amount, int healths, float moveSpeeds, float fireRates, BulletData bullets)
    {
        MaxAmount = maxAmount;
        m_enemyAmount = Mathf.Clamp(amount, 0, MaxAmount);
        m_healths = Mathf.Clamp(healths, 1, 10);
        m_fireRates = Mathf.Clamp(fireRates, 0.5f, 20);
        m_moveSpeeds = Mathf.Clamp(moveSpeeds, 0.1f, 20);
        Bullets = bullets;
    }

    public static WaveData Default() => new(EnemyHandler.MaxEnemyAmount, 0, 1, 5, 15, BulletData.Default());
}