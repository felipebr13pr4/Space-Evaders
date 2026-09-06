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
        for (int i = 0; i < 10; i++)
            yield return null;
        ErrorLogger.DebugLog("reached openoverlay");
        bool isPaused = Time.timeScale == 0;
        StartCoroutine(UpdateTitle());
        StartCoroutine(ComponentsActivation(isPaused));
        foreach (var window in m_otherWindows) window.GetComponent<FadingMenu>().Disable();
    }

    private IEnumerator ComponentsActivation(bool isPaused)
    {
        for (int i = 0; i < 10; i++)
        {
            EnableOrDisable(isPaused, m_components);
            EnableOrDisable(isPaused, m_background);
            yield return null;
        }
    }

    private void EnableOrDisable(bool isPaused, GameObject obj)
    {
        if (obj.activeInHierarchy) obj.GetComponent<FadingMenu>().Enable();
        if (isPaused) obj.SetActive(true);
        else obj.GetComponent<FadingMenu>().Disable();
    }

    private IEnumerator UpdateTitle()
    {
        while (true)
        {
            while (!m_windowTitle.gameObject.activeInHierarchy) yield return null;
            m_windowTitle.text = HandleTitle();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private string HandleTitle()
    {
        // Put here ifs and else ifs when there are other things that can make this open.
        if (Time.timeScale == 0)
        {
            return "Game Paused.";
        }else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return "Game Name";
        }
        return "Game Paused.";
    }
}
