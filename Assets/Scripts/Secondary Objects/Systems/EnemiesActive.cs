using System.Collections.Generic;
using UnityEngine;

public class EnemiesActive : MonoBehaviour
{
    private Dictionary<string, GameObject> m_activeEnemies = new();
    private Dictionary<string, GameObject> m_unactiveEnemies = new();
    public Dictionary<string, GameObject> ActiveEnemies => m_activeEnemies;

    public static EnemiesActive Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EntityBehavior.OnDeath += EnemyUnactive;
    }

    private void OnDisable()
    {
        EntityBehavior.OnDeath -= EnemyUnactive;
    }

    public void EnemyActive(EnemyBehavior enemy)
    {
        m_activeEnemies.Add(enemy.name, enemy.gameObject);
        m_unactiveEnemies.Remove(enemy.name);
    }

    private void EnemyUnactive(EntityBehavior entity)
    {
        if (entity is not EnemyBehavior) return;
        m_unactiveEnemies.Add(entity.name, entity.gameObject);
        m_activeEnemies.Remove(entity.name);
    }
}
