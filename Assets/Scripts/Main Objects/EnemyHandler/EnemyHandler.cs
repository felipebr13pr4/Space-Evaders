using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private EnemyPattern[] m_patterns;
    [SerializeField] private List<EnemyMovement> m_enemyMovement = new();
    private int m_waveNumber;
    public static event Action OnStartWave;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return null;
        OnStartWave?.Invoke();
        yield return new WaitForSeconds(3);
        ErrorLogger.DebugLog("starting wave");
        foreach (EnemyMovement enemy in m_enemyMovement) {
            enemy.gameObject.SetActive(true);
            yield return null;
            enemy.Initialize(0.5f, m_patterns[0]);
            //enemy.Initialize(0.5f, m_patterns[0], true); // The three tested and working.
            //enemy.Initialize(0.5f, m_patterns[0], true, true); // I'll leave them for testing.
            //enemy.Initialize(0.5f, m_patterns[0], false, true);
            ErrorLogger.DebugLog("enemy: " + enemy);
        }
    }

    [ContextMenu("Get all enemies prefabs")]
    private void GetAllEnemies()
    {
        ErrorLogger.DebugLog("getting all enemies");
        m_enemyMovement.Clear();
        m_enemyMovement.Add(GetComponentInChildren<EnemyMovement>());
        ErrorLogger.DebugLog("enemies: " + m_enemyMovement);
    }

    // Yet to further expand.
}
