using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private List<EnemyMovement> m_enemysMovement = new();
    [SerializeField] private List<EnemyBehavior> m_enemysBehavior = new();
    private int m_waveNumber;
    private WaveData m_waveData;
    public static event Action OnWaveStart;
    public static event Action<int> OnWaveStartWithNumber;
    private int m_enemiesAlive;
    private Coroutine m_waveRoutine;
    public static readonly int MaxEnemyAmount = 100;

    private void Start()
    {
        m_waveData = WaveData.Default();
        foreach (RangedEntityBehavior enemy in m_enemysBehavior)
        {
            enemy.BulletData = m_waveData.Bullets;
            enemy.InitializeCreations();
        }
        m_waveRoutine = StartCoroutine(StartWave());
    }

    private void OnEnable()
    {
        EntityBehavior.OnDeath += EnemyDead;
    }

    private void OnDisable()
    {
        EntityBehavior.OnDeath -= EnemyDead;
    }

    private void Update()
    {
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            if (m_waveRoutine != null) StopCoroutine(m_waveRoutine);
            m_waveRoutine = StartCoroutine(StartWave());
            // for testing.
        }

    }

    private void EnemyDead(EntityBehavior entity)
    {
        if (entity is not EnemyBehavior) return;
        m_enemiesAlive -= 1;
        if (m_enemiesAlive == 0) {
            if (m_waveRoutine != null) StopCoroutine(m_waveRoutine);
            m_waveRoutine = StartCoroutine(StartWave()); }
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
        m_waveData.FireRates -= 1;
        //m_waveData.FireRates = 1; // for testing
        m_waveData.MoveSpeeds -= 0.25f;
        m_waveData.Bullets.MoveInterval -= 0.001f;
        ErrorLogger.DebugLog("supposed H: " + m_waveNumber / 10);
        ErrorLogger.DebugLog("current H: " + m_waveData.Healths);
        ErrorLogger.DebugLog("---");
        ErrorLogger.DebugLog("current Amount: " + m_waveData.EnemyAmount);
        ErrorLogger.DebugLog("current Alive: " + m_enemiesAlive);
        ErrorLogger.DebugLog("---");

        yield return new WaitForSeconds(3);

        ErrorLogger.DebugLog("starting wave");

        int enemyCount = m_waveData.EnemyAmount;

        for (int i = 0; i < enemyCount; i++)
        {
            m_enemysBehavior[i].MaxHealth = m_waveData.Healths;
            m_enemysBehavior[i].FireRate = m_waveData.FireRates;
            m_enemysBehavior[i].BulletData = m_waveData.Bullets;
            m_enemysBehavior[i].Initialize();
        }

        for (int i = 0; i < enemyCount; i++) {
            m_enemysBehavior[i].gameObject.SetActive(true);
            m_enemysBehavior[i].StartShooting();
            yield return null;
            m_enemysMovement[i].Initialize(m_waveData.MoveSpeeds);
            yield return new WaitForSeconds(m_waveData.MoveSpeeds);
        }
    }

    [ContextMenu("Get all enemies prefabs")]
    private void GetAllEnemies()
    {
        ErrorLogger.DebugLog("getting all enemies");
        m_enemysMovement.Clear();
        m_enemysBehavior.Clear();

        EnemyBehavior[] enemies = GetComponentsInChildren<EnemyBehavior>(true);

        foreach (EnemyBehavior obj in enemies)
        {
            DestroyImmediate(obj.gameObject);
        }

        for (int i = 0; i < MaxEnemyAmount; i++)
        {
            GameObject obj = Instantiate(m_enemyPrefab, transform);
            obj.name = "Enemy " + (i+1);
        }

        EnemyMovement[] enemiesMov = GetComponentsInChildren<EnemyMovement>();
        EnemyBehavior[] enemiesBeh = GetComponentsInChildren<EnemyBehavior>();

        int j = 1;
        for (int i = 4; i < MaxEnemyAmount; i += 4)
        {
            GameObject obj = enemiesBeh[i].gameObject;
            DestroyImmediate(enemiesBeh[i]);
            obj.name = "Aimbot Enemy " + (j); j++;
            enemiesBeh[i] = obj.AddComponent<AimbotEnemyBehavior>();
        }

        foreach (EnemyMovement enemy in enemiesMov)
        {
            m_enemysMovement.Add(enemy);
        }
        foreach (EnemyBehavior enemy in enemiesBeh)
        {
            m_enemysBehavior.Add(enemy);
            enemy.gameObject.SetActive(false);
        }
    }

    // Yet to further expand.
}
