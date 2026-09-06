using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadingMenu : MonoBehaviour
{
    private WaitForSecondsRealtime m_fadeTimer = new(0.01f);
    [SerializeField] private CanvasGroup m_canvasGroup;
    private Coroutine m_fadeCoroutine;
    private float m_alpha;
    private float Alpha
    {
        get => m_alpha;
        set
        {
            m_alpha = value;
            m_alpha = Mathf.Clamp01(m_alpha);
            if (m_alpha == 0) gameObject.SetActive(false);
            else if (m_alpha < 1) m_canvasGroup.blocksRaycasts = false;
            else if (m_alpha == 1) m_canvasGroup.blocksRaycasts = true;
        }
    }

    private void OnEnable()
    {
        Enable();
    }

    public void Disable()
    {
        if (!isActiveAndEnabled) return;
        if (m_fadeCoroutine != null) StopCoroutine(m_fadeCoroutine);
        m_fadeCoroutine = StartCoroutine(Fade(0, Alpha, -0.05f, true));
    }

    public void Enable()
    {
        if (m_fadeCoroutine != null) StopCoroutine(m_fadeCoroutine);
        m_fadeCoroutine = StartCoroutine(Fade(Alpha, 1, 0.05f, false));
    }

    private IEnumerator Fade(float alpha, float alpha2, float incrementation, bool isInversed)
    {
        // enable: Alpha < 1
        // disable: Alpha > 0
        while (alpha < alpha2)
        {
            Alpha += incrementation;
            if (!isInversed) alpha = Alpha;
            else alpha2 = Alpha;
            ErrorLogger.DebugLog(Alpha);
            m_canvasGroup.alpha = Alpha;
            yield return m_fadeTimer;
        }
    }
}
