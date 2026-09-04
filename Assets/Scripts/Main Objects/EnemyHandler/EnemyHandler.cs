using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private GameObject m_aimbotEnemyPrefab;
    [SerializeField] private GameObject m_triEnemyPrefab;
    [SerializeField] private GameObject m_aimbotTriEnemyPrefab;
    [SerializeField] private List<EnemyMovement> m_enemysMovement = new();
    [SerializeField] private List<EnemyBehavior> m_enemysBehavior = new();
    #if UNITY_EDITOR
    [SerializeField] private Transform m_creationHolder;
    #endif
    private int m_waveNumber;
    private WaveData m_waveData;
    public static event Action OnWaveStart;
    public static event Action<int> OnWaveStartWithNumber;
    private int m_enemiesAlive;
    private Coroutine m_waveCoroutine;
    private readonly WaitForSeconds m_spawnEnemyTimer = new(0.15f);
    public static readonly int MaxEnemyAmount = 100;
    private Dictionary<string, int> m_enemyAmount = new();
    private bool m_spawned;

    private void Start()
    {
        m_waveData = WaveData.Default();
        foreach (RangedEntityBehavior enemy in m_enemysBehavior)
        {
            enemy.BulletsData = m_waveData.Bullets;
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

    private void Update()
    {
        #if UNITY_EDITOR
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
        #endif
    }
    
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
        m_waveData.FireRates -= 1;
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
            m_enemysBehavior[i].MaxHealth = m_waveData.Healths;
            m_enemysBehavior[i].FireRate = m_waveData.FireRates;
            m_enemysBehavior[i].BulletsData.MoveInterval = m_waveData.Bullets.MoveInterval;
            m_enemysBehavior[i].Initialize();
        }

        for (int i = 0; i < enemyCount; i++) {
            yield return null;
            m_enemysMovement[i].Initialize(m_waveData.MoveSpeeds);
            yield return m_spawnEnemyTimer;
        }
    }

    [ContextMenu("Regemerate all enemies prefabs")]
    private void CreateAllEnemies()
    {
        ErrorLogger.DebugLog("getting all enemies");
        m_enemysMovement.Clear();
        m_enemysBehavior.Clear();

        EnemyBehavior[] enemies = GetComponentsInChildren<EnemyBehavior>(true);

        foreach (EnemyBehavior obj in enemies)
        {
            DestroyImmediate(obj.gameObject);
        }

        string enemyStrKey = m_enemyPrefab.name;
        string AimbotEnemyStrKey = m_aimbotEnemyPrefab.name;
        string TriEnemyStrKey = m_triEnemyPrefab.name;
        string AimbotTriEnemyStrKey = m_aimbotTriEnemyPrefab.name;

        m_enemyAmount.Clear();
        m_enemyAmount.Add(enemyStrKey, 0);
        m_enemyAmount.Add(AimbotEnemyStrKey, 0);
        m_enemyAmount.Add(TriEnemyStrKey, 0);
        m_enemyAmount.Add(AimbotTriEnemyStrKey, 0);

        int[] j = new int[3];
        int[] whereWillAimbotSpawn = WhereWillSpawn(i: 24, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96);
        int[] whereWillTriSpawn = WhereWillSpawn(i: 16, 6, 10, 18, 22, 30, 34, 42, 46, 54, 58, 66, 70, 78, 82, 90, 94);
        int[] whereWillAimbotTriSpawn = WhereWillSpawn(i: 8, 13, 25, 38, 50, 63, 75, 89, 99);

        m_spawned = false;

        for (int i = 0; i < MaxEnemyAmount; i++)
        {
            if (!m_spawned)
                SpawnSpecialEnemy(i, whereWillAimbotSpawn, m_aimbotEnemyPrefab, AimbotEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, whereWillTriSpawn, m_triEnemyPrefab, TriEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, whereWillAimbotTriSpawn, m_aimbotTriEnemyPrefab, AimbotTriEnemyStrKey);

            if (!m_spawned)
                CreateEnemy(m_enemyPrefab, m_enemyAmount[enemyStrKey]);

            m_spawned = false;
        }

        EnemyMovement[] enemiesMov = GetComponentsInChildren<EnemyMovement>();
        EnemyBehavior[] enemiesBeh = GetComponentsInChildren<EnemyBehavior>();

        foreach (Transform obj in m_creationHolder.gameObject.GetComponentsInChildren<Transform>())
        {
            if (obj == m_creationHolder) continue;
            if (obj != null) DestroyImmediate(obj.gameObject);
        }
        foreach (EnemyMovement enemy in enemiesMov)
        {
            m_enemysMovement.Add(enemy);
        }
        foreach (EnemyBehavior enemy in enemiesBeh)
        {
            m_enemysBehavior.Add(enemy);
            enemy.InitializeCreations(m_creationHolder);
            enemy.gameObject.SetActive(false);
        }
    }

    private void CreateEnemy(GameObject enemy, int i)
    {
        GameObject obj = Instantiate(enemy, transform);
        m_enemyAmount[enemy.name]++;
        obj.name = $"{enemy.name} " + (i + 1);
    }

    private void SpawnSpecialEnemy(int i, int[] whereWillSpawn, GameObject enemyPrefab, string enemyAmount)
    {
        foreach (int j in whereWillSpawn)
        {
            if (j == i)
            {
                CreateEnemy(enemyPrefab, m_enemyAmount[enemyAmount]);
                m_spawned = true;
            }
        }
    }

    /// <summary>
    /// Returns spawn locations. I must be equal to locations lenght and not below 0.
    /// </summary>
    private int[] WhereWillSpawn(int i, params int[] locations)
    {
        if (i < 0)
        {   ErrorLogger.LogError("Error when deciding the spawn locations of a enemy. Make sure the I is positive and not below 0.");
            int[] k = new int[0]; return k; }

        int[] values = new int[i];

        if (i != locations.Length)
        { ErrorLogger.LogError("Error when deciding the spawn locations of a enemy. Make sure the locations lenght is equal to the I");
          return values; }

        for (int j = 0; j < values.Length; j++)
        {
            values[j] = locations[j];
        }
        return values;
    }
}
