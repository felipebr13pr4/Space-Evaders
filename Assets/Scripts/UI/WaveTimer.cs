using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTimer : MonoBehaviour
{
    private TextMeshProUGUI m_text;

    private void Start() { m_text = GetComponent<TextMeshProUGUI>();
        EnemyHandler.OnStartWave += WaveStart;
    }  

    private void OnDestroy() => EnemyHandler.OnStartWave -= WaveStart;

    private void WaveStart() => StartCoroutine(ChangeText());

    private IEnumerator ChangeText()
    {
        ErrorLogger.DebugLog("Teeeeeeeeeeeeeeeeeeeeeeeeeest");
        gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            ErrorLogger.DebugLog("Teeeeeeeeeeeeeeeeeeeeeeeeeest 2 " + i);
            m_text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        gameObject.SetActive(false);
    }
}