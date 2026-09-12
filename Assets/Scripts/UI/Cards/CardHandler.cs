using System;
using System.Collections;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_cardUI;
    public static event Action OnCardWaveNumber;

    private void OnEnable()
    {
        EnemyHandler.OnWaveStartWithNumber += IsCardWave;
        GameStateController.OnGamePaused += OnUnpause;
    }

    private void OnDisable()
    {
        EnemyHandler.OnWaveStartWithNumber -= IsCardWave;
        GameStateController.OnGamePaused -= OnUnpause;
    }

    private void IsCardWave(int wave)
    {
        bool isIt = false;
        if (wave % 3 == 0) isIt = true;
        if (isIt) { OnCardWaveNumber?.Invoke();
                    m_cardUI.SetActive(true);
                    StartCoroutine(MakeSureItsPaused()); }
    }

    // Necessary as spamming pauses when opening causes problems.
    private IEnumerator MakeSureItsPaused()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (Time.timeScale > 0) OnCardWaveNumber?.Invoke();
    }

    private void OnUnpause()
    {
        if (Time.timeScale > 0) m_cardUI.GetComponent<FadingMenu>().Disable();
    }
}
