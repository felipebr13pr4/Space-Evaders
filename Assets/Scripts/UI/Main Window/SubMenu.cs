using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_returnButton;
    private bool m_firstTime = true;
    public static event Action<bool> OnSubMenuOpen;

    private void OnEnable()
    {   OnSubMenuOpen?.Invoke(!isActiveAndEnabled);
        if (m_returnButton != null) m_returnButton.SetActive(isActiveAndEnabled); }

    private void OnDisable()
    {   OnSubMenuOpen?.Invoke(!isActiveAndEnabled); }

    private void Start()
    {   if (m_firstTime)
        {   Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            m_firstTime = false; } }

}