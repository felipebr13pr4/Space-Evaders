using UnityEngine;

public struct WaveData
{
    public int MaxAmount;
    private int m_enemyAmount;
    public int EnemyAmount {
        readonly get => m_enemyAmount;
        set { m_enemyAmount = value; m_enemyAmount = Mathf.Clamp(m_enemyAmount, 0, MaxAmount); } }
    public int m_healths;
    public int Healths
    {
        readonly get => m_healths;
        set { m_healths = value; m_healths = Mathf.Clamp(m_healths, 1, 10); }
    }
    public float m_moveSpeeds;
    public float MoveSpeeds
    {
        readonly get => m_moveSpeeds;
        set { m_moveSpeeds = value; m_moveSpeeds = Mathf.Clamp(m_moveSpeeds, 0.1f, 20); }
    }
    public float FireRates;
    public BulletData Bullets;

    public WaveData(int maxAmount, int amount, int health, float moveSpeed, float fireRate, BulletData bullets)
    {
        MaxAmount = maxAmount;
        m_enemyAmount = Mathf.Clamp(amount, 0, MaxAmount);
        m_healths = Mathf.Clamp(health, 1, 10);
        FireRates = fireRate;
        m_moveSpeeds = Mathf.Clamp(moveSpeed, 0.1f, 20);
        Bullets = bullets;
    }

    public static WaveData Default() => new(40, 0, 1, 5, 15, BulletData.Default());
}