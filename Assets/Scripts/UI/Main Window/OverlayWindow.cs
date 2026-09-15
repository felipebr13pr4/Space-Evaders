using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_windowTitle;
    [SerializeField] private GameObject m_components;
    [SerializeField] private GameObject m_background;
    [SerializeField] private GameObject[] m_otherWindows;
    private bool m_isPlayerDead;
    private bool m_isOnCardMenu = false;
    public static event Action<bool> OnOpen;


    private void OnEnable()
    {
        CardHandler.OnCardsActivated += CardMenu;
        GlobalHotkeysController.OnOpenMenu += OpenOverlayWindowWrap;
        PlayerBehavior.OnPlayerDeath += PlayerDied;
        PlayerBehavior.OnPlayerDeath += OpenOverlayWindowWrap;
        // Put here another thing to make it open. (player death)
    }

    private void OnDisable()
    {
        CardHandler.OnCardsActivated -= CardMenu;
        GlobalHotkeysController.OnOpenMenu -= OpenOverlayWindowWrap;
        PlayerBehavior.OnPlayerDeath -= PlayerDied;
        PlayerBehavior.OnPlayerDeath -= OpenOverlayWindowWrap;
        // Put here another thing to make it open.  (player death)
    }

    private void CardMenu() => m_isOnCardMenu = !m_isOnCardMenu; 
    private void PlayerDied() => m_isPlayerDead = true;

    private void OpenOverlayWindowWrap() => StartCoroutine(OpenOverlayWindow());

    private IEnumerator OpenOverlayWindow()
    {
        yield return null;
        ErrorLogger.DebugLog("reached openoverlay");
        bool shouldActivate = Time.timeScale == 0;
        if (m_isOnCardMenu && !m_otherWindows[2].activeInHierarchy)
        { shouldActivate = !m_components.activeInHierarchy; }
        yield return null;
        EnableOrDisable(shouldActivate, m_components);
        EnableOrDisable(shouldActivate, m_background);
        m_windowTitle.text = HandleTitle();
        foreach (var window in m_otherWindows) window.GetComponent<FadingMenu>().Disable();
    }

    private void EnableOrDisable(bool shouldActivate, GameObject obj)
    {
        if (shouldActivate) { obj.SetActive(true);
            if (obj.activeSelf) { obj.SetActive(false); obj.SetActive(true); } }
        else { if(!m_isPlayerDead) obj.GetComponent<FadingMenu>().Disable(); }
        OnOpen?.Invoke(shouldActivate);
    }

    private string HandleTitle()
    {
        // Put here ifs and else ifs when there are other things that can make this open.
        if (Time.timeScale == 0)
        {
            return "Game Paused.";
        }
        else if (m_isPlayerDead)
        {
            return "Game Name";
        }
        else if (SceneManager.GetActiveScene().name == SceneNames.MainMenu)
        {
            return "Game Name";
        }
        return "Game Paused.";
    }
}
