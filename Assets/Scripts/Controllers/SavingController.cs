using System.Collections;
using UnityEngine;

public class SavingController : MonoBehaviour
{
    private bool m_isAutoSaveOn = true;

    public static SavingController Instance { get; private set; }
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

    private void OnEnable() => GameStateController.OnGamePaused += SaveAll;

    private void Start() => StartCoroutine(AutoSave());

    private void OnDisable() => GameStateController.OnGamePaused -= SaveAll;

    private void OnApplicationQuit() => SaveAll();

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5);
            if (m_isAutoSaveOn) Save();
        }
    }

    public void SaveAll()
    {
        Save();
        SavePlayerPrefs();
        PlayerPrefs.Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(PrefKeys.EnemyKilled, DataController.Instance.EnemiesKilled);
        PlayerPrefs.SetInt(PrefKeys.TimesKilled, DataController.Instance.TimesKilled);
        PlayerPrefs.SetInt(PrefKeys.DamageTaken, DataController.Instance.DamageTaken);
        PlayerPrefs.SetInt(PrefKeys.DamageDealt, DataController.Instance.DamageDealt);
        PlayerPrefs.SetInt(PrefKeys.BasicsKilled, DataController.Instance.BasicsKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotsKilled, DataController.Instance.AimbotsKilled);
        PlayerPrefs.SetInt(PrefKeys.TrisKilled, DataController.Instance.TrisKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotTrisKilled, DataController.Instance.AimbotTrisKilled);
        PlayerPrefs.SetInt(PrefKeys.FivesKilled, DataController.Instance.FivesKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotFivesKilled, DataController.Instance.AimbotFivesKilled);
        PlayerPrefs.SetInt(PrefKeys.SevensKilled, DataController.Instance.SevensKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotSevensKilled, DataController.Instance.AimbotSevensKilled);
        PlayerPrefs.SetInt(PrefKeys.TensKilled, DataController.Instance.TensKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotTensKilled, DataController.Instance.AimbotTensKilled);
        PlayerPrefs.SetInt(PrefKeys.BossesKilled, DataController.Instance.BossesKilled);
        PlayerPrefs.SetInt(PrefKeys.AimbotBossesKilled, DataController.Instance.AimbotBossesKilled);
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat(PrefKeys.Volume, AudioController.Instance.AudioVolume);
        PlayerPrefs.SetFloat(PrefKeys.MusicVolume, MusicController.Instance.AudioVolume);
        PlayerPrefs.SetInt(PrefKeys.ScreenWidth, Screen.width);
        PlayerPrefs.SetInt(PrefKeys.ScreenHeight, Screen.height);
        PlayerPrefs.SetInt(PrefKeys.FullScreen, Screen.fullScreenMode == FullScreenMode.FullScreenWindow ? 1 : 0);
        if (PlayerBehavior.Instance != null)
        PlayerPrefs.SetFloat(PrefKeys.PlayerBulletTransparency, PlayerBehavior.Instance.BulletTransparency);
    }
}