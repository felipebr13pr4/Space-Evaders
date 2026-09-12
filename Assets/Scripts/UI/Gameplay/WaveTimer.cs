using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTimer : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    private Coroutine m_changeTextCoroutine;

    private void Start() { m_text = GetComponent<TextMeshProUGUI>();
        EnemyHandler.OnWaveStart += WaveStart;
    }  

    private void OnDestroy() => EnemyHandler.OnWaveStart -= WaveStart;

    private void WaveStart() { gameObject.SetActive(true);
        if (m_changeTextCoroutine != null) StopCoroutine(m_changeTextCoroutine);
        m_changeTextCoroutine = StartCoroutine(ChangeText()); }

    private IEnumerator ChangeText()
    {
        ErrorLogger.DebugLog("Timer!");
        
        for (int i = 3; i > 0; i--)
        {
            ErrorLogger.DebugLog("Wave Timer time: " + i);
            m_text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        gameObject.SetActive(false);
    }
}