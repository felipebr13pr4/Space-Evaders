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


    private void OnEnable()
    {
        GlobalHotkeysController.OnOpenMenu += OpenOverlayWindowWrap;
        // Put here another thing to make it open.
    }

    private void OnDisable()
    {
        GlobalHotkeysController.OnOpenMenu -= OpenOverlayWindowWrap;
        // Put here another thing to make it open.
    }

    private void OpenOverlayWindowWrap() => StartCoroutine(OpenOverlayWindow());

    private IEnumerator OpenOverlayWindow()
    {
        yield return null;
        ErrorLogger.DebugLog("reached openoverlay");
        bool isPaused = Time.timeScale == 0;
        yield return null;
        EnableOrDisable(isPaused, m_components);
        EnableOrDisable(isPaused, m_background);
        m_windowTitle.text = HandleTitle();
        foreach (var window in m_otherWindows) window.GetComponent<FadingMenu>().Disable();
    }

    private void EnableOrDisable(bool isPaused, GameObject obj)
    {
        if (obj.activeInHierarchy) obj.GetComponent<FadingMenu>().Enable();
        if (isPaused) obj.SetActive(true);
        else obj.GetComponent<FadingMenu>().Disable();
    }

    private string HandleTitle()
    {
        // Put here ifs and else ifs when there are other things that can make this open.
        if (Time.timeScale == 0)
        {
            return "Game Paused.";
        }else if (SceneManager.GetActiveScene().name == SceneNames.MainMenu)
        {
            return "Game Name";
        }
        return "Game Paused.";
    }
}
