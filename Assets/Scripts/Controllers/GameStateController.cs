using System;
using UnityEngine;

[RequireComponent(typeof(AudioHolder))]
public class GameStateController : MonoBehaviour
{
    private bool m_isPlayerDead;
    private bool m_isInSubMenu;
    private bool m_isGamePaused;
    public bool IsPlayerDead => m_isPlayerDead;
    public bool IsGamePaused => m_isGamePaused;
    public bool IsInSubMenu => m_isInSubMenu;

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
        SubMenu.OnSubMenuOpen += SubMenuOpen;
        // Put things when there is something to listen to prevent it from pausing.

    }

    private void OnDisable()
    {
        GlobalHotkeysController.OnOpenMenu -= TogglePause;
        SubMenu.OnSubMenuOpen -= SubMenuOpen;
        // Put things when there is something to listen to prevent it from pausing.
    }

    public void SubMenuOpen(bool isOpen) => m_isInSubMenu = isOpen;

    public void TogglePause()
    {
        if (m_isPlayerDead | m_isInSubMenu) return;
        GetComponent<AudioHolder>().ActivateSound(0);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        m_isGamePaused = Time.timeScale == 0; 
        OnGamePaused?.Invoke();
    }

    public void ResetStates()
    {
        m_isPlayerDead = false;
        m_isGamePaused = false;
    }
}
