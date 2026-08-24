using System;
using System.Collections;
using UnityEngine;

public class EntityBehavior : Entity
{
    [SerializeField] protected int m_maxHealth = 3;
    protected virtual int MaxHealth { get => m_maxHealth; set { m_maxHealth = value; m_health = value; } }
    private int m_lastHealth;
    private int m_health;
    public int Health
    {
        get { return m_health; }
        set
        {
            m_lastHealth = m_health;
            m_health = value;
            m_health = Mathf.Clamp(m_health, 0, m_maxHealth);
            if (m_health <= 0 & !m_isDead) Die();
        }
    }
    private bool m_isDead = false;
    public static event Action<GameObject, int> OnDamageTaken;

    public virtual void TakeDamage(int damage = 0, EntityBehavior hitter = null, bool takeAndDeal = false)
    {
        if (hitter != null) damage = hitter.Health;
        OnDamageTaken?.Invoke(gameObject, damage);
        StartCoroutine(FlashDamage());
        if (hitter != null)
        {   Health -= damage;
            if (takeAndDeal) hitter.TakeDamage(m_lastHealth);
            return; }
        Health -= damage;
    }

    private IEnumerator FlashDamage()
    {
        Color lastColor = m_spriteRenderer.color;
        m_spriteRenderer.color = Color.red;
        yield return null;
        m_spriteRenderer.color = lastColor;
    }

    protected virtual void Die()
    {
        if (m_isDead) return;
        m_isDead = true;
        gameObject.SetActive(false);
    }

    protected virtual void Initialize(int health)
    {
        m_health = health;
    }
}
