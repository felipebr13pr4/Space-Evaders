using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private List<EnemyMovement> m_enemiesMovement = new();
    [SerializeField] private List<EnemyBehavior> m_enemiesBehavior = new();
    public List<EnemyMovement> EnemiesMovements => m_enemiesMovement;
    public List<EnemyBehavior> EnemiesBehaviors => m_enemiesBehavior;
    private int m_waveNumber;
    private WaveData m_waveData;
    public static event Action OnWaveStart;
    public static event Action<int> OnWaveStartWithNumber;
    private int m_enemiesAlive;
    private Coroutine m_waveCoroutine;
    private readonly WaitForSeconds m_spawnEnemyTimer = new(0.15f);
    public const int MaxEnemyAmount = 210;
    private Dictionary<string, int> m_enemyAmount = new();
    public Dictionary<string, int> EnemyAmount => m_enemyAmount;

    private void Start()
    {
        m_waveData = WaveData.Default();
        foreach (RangedEntityBehavior enemy in m_enemiesBehavior)
        {
            enemy.BulletsData.SetMainStats(m_waveData.Bullets);
        }
        m_waveCoroutine = StartCoroutine(StartWave());
    }

    private void OnEnable()
    {
        EntityBehavior.OnDeath += EnemyDead;
    }

    private void OnDisable()
    {
        EntityBehavior.OnDeath -= EnemyDead;
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private void Update()
    {
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            if (m_waveCoroutine != null) StopCoroutine(m_waveCoroutine);
            m_waveCoroutine = StartCoroutine(StartWave());
            // for testing.
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            StartCoroutine(SkipWaves(10));
        }
    }
#endif

    private IEnumerator SkipWaves(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (m_waveCoroutine != null) StopCoroutine(m_waveCoroutine);
            m_waveCoroutine = StartCoroutine(StartWave());
            yield return null;
        }
    }

    private void EnemyDead(EntityBehavior entity)
    {
        if (entity is not EnemyBehavior) return;
        m_enemiesAlive -= 1;
        if (m_enemiesAlive == 0) {
            if (m_waveCoroutine != null) StopCoroutine(m_waveCoroutine);
            m_waveCoroutine = StartCoroutine(StartWave()); }
    }

    private IEnumerator StartWave()
    {
        yield return null;

        m_waveNumber += 1;
        OnWaveStart?.Invoke();
        OnWaveStartWithNumber?.Invoke(m_waveNumber);

        m_waveData.EnemyAmount += 2;
        m_enemiesAlive = m_waveData.EnemyAmount;
        m_waveData.Healths = (m_waveNumber / 10) + 1;
        m_waveData.FireRates -= 0.25f;
        //m_waveData.FireRates = 1; // for testing
        m_waveData.MoveSpeeds -= 0.25f;
        m_waveData.Bullets.MoveInterval -= 0.0002f;
        ErrorLogger.DebugLog("current H: " + m_waveData.Healths);
        ErrorLogger.DebugLog("---");
        ErrorLogger.DebugLog("current Amount: " + m_waveData.EnemyAmount);
        ErrorLogger.DebugLog("current Alive: " + m_enemiesAlive);
        ErrorLogger.DebugLog("---");
        ErrorLogger.DebugLog("current Move Interval: " + m_waveData.Bullets.MoveInterval);

        yield return new WaitForSeconds(3);

        ErrorLogger.DebugLog("starting wave");

        int enemyCount = m_waveData.EnemyAmount;

        for (int i = 0; i < enemyCount; i++)
        {
            m_enemiesBehavior[i].MaxHealth = m_waveData.Healths;
            m_enemiesBehavior[i].FireRate = m_waveData.FireRates;
            m_enemiesBehavior[i].BulletsData.MoveInterval = m_waveData.Bullets.MoveInterval;
            m_enemiesBehavior[i].Initialize();
        }

        for (int i = 0; i < enemyCount; i++) {
            yield return null;
            m_enemiesMovement[i].Initialize(m_waveData.MoveSpeeds);
            yield return m_spawnEnemyTimer;
        }
    }
}
