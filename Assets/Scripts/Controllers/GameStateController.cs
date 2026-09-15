using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioHolder))]
public class GameStateController : MonoBehaviour
{
    private bool m_canUnpause;
    private bool m_inSubMenu;
    private bool m_isGamePaused;
    public bool IsGamePaused => m_isGamePaused;
    public bool IsInSubMenu => m_canUnpause;

    public static event Action OnGamePaused;
    public static event Action<bool> OnGamePausedWithIfLocked;

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
        GlobalHotkeysController.OnOpenMenu += CheckIfCanPause;
        SubMenuHandler.IsSubMenuOpen += UpdateIfSubMenu;
        CardHandler.OnCardsActivated += BlockThenTPause;
        Card.OnCardChosen += UnblockThenCheckPause;
        PlayerBehavior.OnPlayerDeath += BlockThenTPause;
        // Put things when there is something to listen to prevent it from pausing.

    }

    private void OnDisable()
    {
        GlobalHotkeysController.OnOpenMenu -= CheckIfCanPause;
        SubMenuHandler.IsSubMenuOpen -= UpdateIfSubMenu;
        CardHandler.OnCardsActivated -= BlockThenTPause;
        Card.OnCardChosen -= UnblockThenCheckPause;
        PlayerBehavior.OnPlayerDeath -= BlockThenTPause;
        // Put things when there is something to listen to prevent it from pausing.
    }

    public void UnblockThenCheckPause()
    {
        m_canUnpause = true;
        CheckIfCanPause();
    }

    public void BlockThenTPause()
    {
        m_canUnpause = false;
        TogglePause();
    }

    public void UpdateIfSubMenu(bool isIn) => m_inSubMenu = isIn;

    public void CheckIfCanPause()
    {
        if (!m_canUnpause || m_inSubMenu) return;
        TogglePause();
    }

    private void TogglePause()
    {
        GetComponent<AudioHolder>().ActivateSound(0);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        m_isGamePaused = Time.timeScale == 0;
        OnGamePaused?.Invoke();
        OnGamePausedWithIfLocked?.Invoke(m_canUnpause);
    }

    public void ResetStates()
    {
        m_canUnpause = true;
        m_isGamePaused = false;
    }
}
