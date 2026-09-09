using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(MeshRenderer))]
public class DamageNumber : MonoBehaviour
{
    private TextMeshPro m_text;
    private RectTransform m_rect;

    private void Start()
    {
        m_rect = GetComponent<RectTransform>();
        m_rect.sizeDelta = new(3,3);

        m_text = GetComponent<TextMeshPro>();
        m_text.fontSize = 8;
        m_text.horizontalAlignment = HorizontalAlignmentOptions.Center;
        m_text.verticalAlignment = VerticalAlignmentOptions.Middle;
        
        gameObject.SetActive(false);
    }

    public void Initialize(int amount, Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        m_text.color = Color.white;
        m_text.text = amount.ToString();
        StartCoroutine(Float());
    }

    private IEnumerator Float()
    {
        for (int i = 0; i < 100; i++) { 
            yield return new WaitForSeconds(0.01f);
            m_rect.position += new Vector3(0, 0.01f, 0);
            m_text.color -= new Color(0, 0, 0, 0.01f);
        }
        gameObject.SetActive(false);
    }
}