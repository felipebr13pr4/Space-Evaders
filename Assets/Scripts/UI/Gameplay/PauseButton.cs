using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    private Button m_pauseButton;
    public static event Action OnPress;

    private void Awake() => m_pauseButton = GetComponent<Button>();

    private void OnEnable() => m_pauseButton.onClick.AddListener(TogglePause);

    private void OnDisable() => m_pauseButton.onClick.RemoveListener(TogglePause);

    private void TogglePause()
    {
        if (!MenuTransitionLock.IsLocked)
            { OnPress?.Invoke(); MenuTransitionLock.LockFor(OverlayWindow.m_waitTime); }
    }
}