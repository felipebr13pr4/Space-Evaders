using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalHotkeysController : MonoBehaviour
{
    public static event Action OnOpenMenu;
    private string m_sceneName;

    public static GlobalHotkeysController Instance { get; private set; }
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

    private void OnEnable() => SceneManager.sceneLoaded += UpdateSceneName;

    private void OnDisable() => SceneManager.sceneLoaded -= UpdateSceneName;

    private void UpdateSceneName(Scene scene, LoadSceneMode sceneLoadMode)
        => m_sceneName = scene.name;
    

    private void Update()
    {
        if (m_sceneName == SceneNames.MainMenu) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            OnOpenMenu?.Invoke();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Keyboard.current.qKey.wasPressedThisFrame)
            if (Time.timeScale > 0.2f) Time.timeScale -= 0.1f;
        if (Keyboard.current.eKey.wasPressedThisFrame)
            Time.timeScale += 0.1f;
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            Time.timeScale = 1;
#endif
    }
}
