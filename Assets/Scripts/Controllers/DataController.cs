using UnityEngine;

// Yet again claude is really great at repetitive work.
public class DataController : MonoBehaviour
{
    private int m_enemiesKilled;
    private int m_timesKilled;
    private int m_damageTaken;
    private int m_damageDealt;
    private int m_basicsKilled;
    private int m_aimbotsKilled;
    private int m_trisKilled;
    private int m_aimbotTrisKilled;
    private int m_fivesKilled;
    private int m_aimbotFivesKilled;
    private int m_sevensKilled;
    private int m_aimbotSevensKilled;
    private int m_tensKilled;
    private int m_aimbotTensKilled;
    private int m_bossesKilled;
    private int m_aimbotBossesKilled;

    public int EnemiesKilled => m_enemiesKilled;
    public int TimesKilled => m_timesKilled;
    public int DamageTaken => m_damageTaken;
    public int DamageDealt => m_damageDealt;
    public int BasicsKilled => m_basicsKilled;
    public int AimbotsKilled => m_aimbotsKilled;
    public int TrisKilled => m_trisKilled;
    public int AimbotTrisKilled => m_aimbotTrisKilled;
    public int FivesKilled => m_fivesKilled;
    public int AimbotFivesKilled => m_aimbotFivesKilled;
    public int SevensKilled => m_sevensKilled;
    public int AimbotSevensKilled => m_aimbotSevensKilled;
    public int TensKilled => m_tensKilled;
    public int AimbotTensKilled => m_aimbotTensKilled;
    public int BossesKilled => m_bossesKilled;
    public int AimbotBossesKilled => m_aimbotBossesKilled;

    public static DataController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
     
    private void Start()
    {
        // Put things when there is a stats to record.
    }

    private void OnEnable()
    {
        PlayerBehavior.OnPlayerDeathAchievement += IncreaseStat;
        PlayerBehavior.OnDamageTakenAchievement += IncreaseStat;
        EnemyBehavior.OnDeathAchievement += IncreaseKillStat;
        EnemyBehavior.OnDamageTaken += IncreaseStat;
    }

    private void OnDisable()
    {
        PlayerBehavior.OnPlayerDeathAchievement -= IncreaseStat;
        PlayerBehavior.OnDamageTakenAchievement -= IncreaseStat;
        EnemyBehavior.OnDeathAchievement -= IncreaseKillStat;
        EnemyBehavior.OnDamageTaken -= IncreaseStat;
    }

    private void IncreaseStat(int amount, AchievementType type)
    {
        switch (type)
        {
            case AchievementType.DamageTaken:
                m_damageTaken++;
                return;

            case AchievementType.DamageDealt:
                m_damageDealt++;
                return;

            default: ErrorLogger.LogError("A invalid achievement type has been received in the data controller. Make sure all indices/names are set right."); return;
        }
    }

    private void IncreaseStat(AchievementType type)
    {
        if (type == AchievementType.TimesKilled) m_timesKilled++;
    }

    private void IncreaseKillStat(AchievementType type)
    {
        m_enemiesKilled++;
        print($"m_enemiesKilled: {m_enemiesKilled}");
        switch (type)
        {
            case AchievementType.BasicKilled:
                m_basicsKilled++;
                return;

            case AchievementType.AimbotKilled:
                m_aimbotsKilled++;
                return;

            case AchievementType.TriKilled:
                m_trisKilled++;
                return;

            case AchievementType.AimbotTriKilled:
                m_aimbotTrisKilled++;
                return;

            case AchievementType.FiveKilled:
                m_fivesKilled++;
                return;

            case AchievementType.AimbotFiveKilled:
                m_aimbotFivesKilled++;
                return;

            case AchievementType.SevenKilled:
                m_sevensKilled++;
                return;

            case AchievementType.AimbotSevenKilled:
                m_aimbotSevensKilled++;
                return;

            case AchievementType.TenKilled:
                m_tensKilled++;
                return;

            case AchievementType.AimbotTenKilled:
                m_aimbotTensKilled++;
                return;

            case AchievementType.BossKilled:
                m_bossesKilled++;
                return;

            case AchievementType.AimbotBossKilled:
                m_aimbotBossesKilled++;
                return;

            default: ErrorLogger.LogError("A invalid achievement type has been received in the data controller. Make sure all indices/names are set right."); return;
        }
    }
}
