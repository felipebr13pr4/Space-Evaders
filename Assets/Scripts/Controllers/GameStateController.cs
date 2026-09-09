using System;
using UnityEngine;

[RequireComponent(typeof(AudioHolder))]
public class GameStateController : MonoBehaviour
{
    private bool m_canUnpause;
    private bool m_isGamePaused;
    public bool IsGamePaused => m_isGamePaused;
    public bool IsInSubMenu => m_canUnpause;

    public static event Action OnGamePaused;

    public static GameStateController Instance { get; private set; }
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

    private void OnEnable()
    {
        GlobalHotkeysController.OnOpenMenu += TogglePause;
        SubMenu.OnSubMenuOpen += DetermineIfCanUnpause;
        CardHandler.OnCardWaveNumber += PauseThenCannotUnpause;
        Card.OnCardChosen += CanUnpauseThenPause;
        // Put things when there is something to listen to prevent it from pausing.

    }

    private void OnDisable()
    {
        GlobalHotkeysController.OnOpenMenu -= TogglePause;
        SubMenu.OnSubMenuOpen -= DetermineIfCanUnpause;
        CardHandler.OnCardWaveNumber -= PauseThenCannotUnpause;
        Card.OnCardChosen -= CanUnpauseThenPause;
        // Put things when there is something to listen to prevent it from pausing.
    }

    public void CanUnpauseThenPause()
    {
        m_canUnpause = true;
        TogglePause();
    }

    public void PauseThenCannotUnpause()
    {
        TogglePause();
        m_canUnpause = false;
    }

    public void DetermineIfCanUnpause(bool can) => m_canUnpause = can;

    public void TogglePause()
    {
        if (!m_canUnpause) return;
        GetComponent<AudioHolder>().ActivateSound(0);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        m_isGamePaused = Time.timeScale == 0; 
        OnGamePaused?.Invoke();
    }

    public void ResetStates()
    {
        m_canUnpause = true;
        m_isGamePaused = false;
    }
}
