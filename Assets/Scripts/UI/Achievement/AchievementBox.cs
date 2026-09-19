using TMPro;
using UnityEngine;

public class AchievementBox : MonoBehaviour
{
    [SerializeField] public string m_name;
    [SerializeField] private AchievementType m_type;
    [SerializeField] private int m_reqAmount;
    [SerializeField] TextMeshProUGUI m_nameText;
    [SerializeField] TextMeshProUGUI m_numberText;
    [SerializeField] GameObject m_transparencyWindow;
    private bool m_hasAchieved;

    private void OnEnable()
    {
        if (m_hasAchieved) return;
        m_hasAchieved = HasAchievedGoal(m_type, m_reqAmount);

        if (m_hasAchieved) m_transparencyWindow.SetActive(false);

        int amount = CheckWhichAmountByType(m_type);

        m_nameText.text = m_name;
        m_numberText.text = m_hasAchieved ?
                            m_reqAmount.ToString() + " / " + m_reqAmount.ToString() :
                            amount.ToString() + " / " + m_reqAmount.ToString();
    }

    private bool HasAchievedGoal(AchievementType type, int reqAmount)
    {
        int amount = CheckWhichAmountByType(type);
        return amount >= reqAmount;
    }

    private int CheckWhichAmountByType(AchievementType type)
    {
        int amount = type switch
        {
            AchievementType.EnemiesKilled => PlayerPrefs.GetInt(PrefKeys.EnemyKilled, 0),
            AchievementType.TimesKilled => PlayerPrefs.GetInt(PrefKeys.TimesKilled, 0),
            AchievementType.DamageTaken => PlayerPrefs.GetInt(PrefKeys.DamageTaken, 0),
            AchievementType.DamageDealt => PlayerPrefs.GetInt(PrefKeys.DamageDealt, 0),
            AchievementType.BasicKilled => PlayerPrefs.GetInt(PrefKeys.BasicsKilled, 0),
            AchievementType.AimbotKilled => PlayerPrefs.GetInt(PrefKeys.AimbotsKilled, 0),
            AchievementType.TriKilled => PlayerPrefs.GetInt(PrefKeys.TrisKilled, 0),
            AchievementType.AimbotTriKilled => PlayerPrefs.GetInt(PrefKeys.AimbotTrisKilled, 0),
            AchievementType.FiveKilled => PlayerPrefs.GetInt(PrefKeys.FivesKilled, 0),
            AchievementType.AimbotFiveKilled => PlayerPrefs.GetInt(PrefKeys.AimbotFivesKilled, 0),
            AchievementType.SevenKilled => PlayerPrefs.GetInt(PrefKeys.SevensKilled, 0),
            AchievementType.AimbotSevenKilled => PlayerPrefs.GetInt(PrefKeys.AimbotSevensKilled, 0),
            AchievementType.TenKilled => PlayerPrefs.GetInt(PrefKeys.TensKilled, 0),
            AchievementType.AimbotTenKilled => PlayerPrefs.GetInt(PrefKeys.AimbotTensKilled, 0),
            AchievementType.BossKilled => PlayerPrefs.GetInt(PrefKeys.BossesKilled, 0),
            AchievementType.AimbotBossKilled => PlayerPrefs.GetInt(PrefKeys.AimbotBossesKilled, 0),
            _ => 0,
        };
        return amount;
    }

    public void Initialize(string name, AchievementType type, int reqAmount)
    {
        m_name = name;
        m_type = type;
        m_reqAmount = reqAmount;
    }

    [ContextMenu("Set Contents.")]
    private void SetContents()
    {
        m_nameText.text = m_name;
        m_numberText.text = m_reqAmount.ToString() + " / " + m_reqAmount.ToString();
    }
}