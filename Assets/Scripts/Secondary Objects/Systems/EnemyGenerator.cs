#if UNITY_EDITOR || DEVELOPMENT_BUILD
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    [SerializeField] private GameObject m_aimbotEnemyPrefab;
    [SerializeField] private GameObject m_triEnemyPrefab;
    [SerializeField] private GameObject m_triAimbotEnemyPrefab;
    [SerializeField] private GameObject m_fiveEnemyPrefab;
    [SerializeField] private GameObject m_fiveAimbotEnemyPrefab;
    [SerializeField] private GameObject m_sevenEnemyPrefab;
    [SerializeField] private GameObject m_sevenAimbotEnemyPrefab;
    [SerializeField] private GameObject m_tenEnemyPrefab;
    [SerializeField] private GameObject m_tenAimbotEnemyPrefab;
    [SerializeField] private GameObject m_bossEnemyPrefab;
    [SerializeField] private GameObject m_bossAimbotEnemyPrefab;
    [SerializeField] private Transform m_creationHolder;
    [SerializeField] private EnemyHandler m_handler;
    private bool m_spawned;

    [ContextMenu("Regemerate all enemies prefabs")]
    private void CreateAllEnemies()
    {
        ErrorLogger.DebugLog("getting all enemies");
        m_handler.EnemiesMovements.Clear();
        m_handler.EnemiesBehaviors.Clear();

        EnemyBehavior[] enemies = GetComponentsInChildren<EnemyBehavior>(true);

        foreach (EnemyBehavior obj in enemies)
        {
            DestroyImmediate(obj.gameObject);
        }

        // Works but is horrendously repeated/long. Most certainly there is a simpler
        // solution. Probally won't do it as it works and its just for the editor.
        string enemyStrKey = m_enemyPrefab.name;
        string AimbotEnemyStrKey = m_aimbotEnemyPrefab.name;
        string TriEnemyStrKey = m_triEnemyPrefab.name;
        string AimbotTriEnemyStrKey = m_triAimbotEnemyPrefab.name;
        string FiveEnemyStrKey = m_fiveEnemyPrefab.name;
        string AimbotFiveEnemyStrKey = m_fiveAimbotEnemyPrefab.name;
        string SevenEnemyStrKey = m_sevenEnemyPrefab.name;
        string AimbotSevenEnemyStrKey = m_sevenAimbotEnemyPrefab.name;
        string TenEnemyStrKey = m_tenEnemyPrefab.name;
        string AimbotTenEnemyStrKey = m_tenAimbotEnemyPrefab.name;
        string BossEnemyStrKey = m_bossEnemyPrefab.name;
        string AimbotBossEnemyStrKey = m_bossAimbotEnemyPrefab.name;

        m_handler.EnemyAmount.Clear();
        m_handler.EnemyAmount.Add(enemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(TriEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotTriEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(FiveEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotFiveEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(SevenEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotSevenEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(TenEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotTenEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(BossEnemyStrKey, 0);
        m_handler.EnemyAmount.Add(AimbotBossEnemyStrKey, 0);

        int[] j = new int[3];
        int[] aimbotSpawnLocations = WhereWillSpawn(i: 52, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96, 100, 104, 108, 112, 116, 120, 124, 128, 132, 136, 140, 144, 148, 152, 156, 160, 164, 168, 172, 176, 180, 184, 188, 192, 196, 200, 204, 208);
        int[] triSpawnLocations = WhereWillSpawn(i: 34, 6, 10, 18, 22, 30, 34, 42, 46, 54, 58, 66, 70, 78, 82, 90, 94, 102, 106, 114, 118, 126, 130, 138, 142, 150, 154, 162, 166, 174, 178, 186, 190, 198, 202);
        int[] triAimbotSpawnLocations = WhereWillSpawn(i: 17, 13, 25, 38, 51, 63, 75, 89, 98, 110, 122, 134, 146, 158, 170, 182, 194, 206);
        int[] fiveSpawnLocations = WhereWillSpawn(i: 15, 37, 49, 61, 73, 85, 97, 109, 121, 133, 145, 157, 169, 181, 193, 205);
        int[] fiveAimbotSpawnLocations = WhereWillSpawn(i: 10, 39, 57, 77, 93, 111, 129, 147, 165, 183, 201);
        int[] sevenSpawnLocations = WhereWillSpawn(i: 10, 50, 67, 83, 101, 117, 135, 151, 171, 185, 203);
        int[] sevenAimbotSpawnLocations = WhereWillSpawn(i: 8, 53, 74, 95, 115, 137, 159, 179, 199);
        int[] tenSpawnLocations = WhereWillSpawn(i: 8, 62, 81, 103, 123, 141, 161, 187, 207);
        int[] tenAimbotSpawnLocations = WhereWillSpawn(i: 6, 65, 91, 119, 143, 173, 197);
        int[] bossSpawnLocations = WhereWillSpawn(i: 2, 105, 209);
        int[] bossAimbotSpawnLocations = WhereWillSpawn(i: 2, 107, 210);

        m_spawned = false;

        for (int i = 0; i < EnemyHandler.MaxEnemyAmount; i++)
        {
            if (!m_spawned)
                SpawnSpecialEnemy(i, aimbotSpawnLocations, m_aimbotEnemyPrefab, AimbotEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, triSpawnLocations, m_triEnemyPrefab, TriEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, triAimbotSpawnLocations, m_triAimbotEnemyPrefab, AimbotTriEnemyStrKey);
            
            if (!m_spawned)
                SpawnSpecialEnemy(i, fiveSpawnLocations, m_fiveEnemyPrefab, FiveEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, fiveAimbotSpawnLocations, m_fiveAimbotEnemyPrefab, AimbotFiveEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, sevenSpawnLocations, m_sevenEnemyPrefab, SevenEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, sevenAimbotSpawnLocations, m_sevenAimbotEnemyPrefab, AimbotSevenEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, tenSpawnLocations, m_tenEnemyPrefab, TenEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, tenAimbotSpawnLocations, m_tenAimbotEnemyPrefab, AimbotTenEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, bossSpawnLocations, m_bossEnemyPrefab, BossEnemyStrKey);

            if (!m_spawned)
                SpawnSpecialEnemy(i, bossAimbotSpawnLocations, m_bossAimbotEnemyPrefab, AimbotBossEnemyStrKey);
            if (!m_spawned)
                CreateEnemy(m_enemyPrefab, m_handler.EnemyAmount[enemyStrKey]);

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
            m_handler.EnemiesMovements.Add(enemy);
        }
        foreach (EnemyBehavior enemy in enemiesBeh)
        {

            m_handler.EnemiesBehaviors.Add(enemy);
            enemy.InitializeCreations(m_creationHolder);
            enemy.gameObject.SetActive(false);
        }
    }

    private void CreateEnemy(GameObject enemy, int i)
    {
        GameObject obj = Instantiate(enemy, transform);
        m_handler.EnemyAmount[enemy.name]++;
        obj.name = $"{enemy.name} " + (i + 1);
    }

    private void SpawnSpecialEnemy(int i, int[] whereWillSpawn, GameObject enemyPrefab, string enemyAmount)
    {
        foreach (int j in whereWillSpawn)
        {
            if (j == i)
            {
                CreateEnemy(enemyPrefab, m_handler.EnemyAmount[enemyAmount]);
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
        {
            ErrorLogger.LogError("Error when deciding the spawn locations of a enemy. Make sure the I is positive and not below 0.");
            int[] k = new int[0]; return k;
        }

        int[] values = new int[i];

        if (i != locations.Length)
        {
            ErrorLogger.LogError("Error when deciding the spawn locations of a enemy. Make sure the locations lenght is equal to the I");
            return values;
        }

        for (int j = 0; j < values.Length; j++)
        {
            values[j] = locations[j];
        }
        return values;
    }
}
#endif
