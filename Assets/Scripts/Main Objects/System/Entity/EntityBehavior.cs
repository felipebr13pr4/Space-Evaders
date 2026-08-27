using System;
using System.Collections;
using UnityEngine;

public class EntityBehavior : Entity
{
    [SerializeField] protected int m_maxHealth = 3;
    public int MaxHealth { get => m_maxHealth; set { m_maxHealth = value; m_health = value; } }
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
    public static event Action<EntityBehavior> OnDeath;
    private DamageNumber[] m_damageNumbers;
    private int m_maxDamageNumbers = 20;
    private int m_damageNumberIndex;
    private int DamageNumberIndex
    {
        get => m_damageNumberIndex;
        set
        {
            m_damageNumberIndex = value;
            m_damageNumberIndex = Mathf.Clamp(m_damageNumberIndex, 0, m_maxDamageNumbers);
            if (m_damageNumberIndex >= m_maxDamageNumbers) m_damageNumberIndex = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        InitializeCreations();
    }

    public virtual void TakeDamage(int damage = 0, EntityBehavior hitter = null, bool takeAndDeal = false)
    {
        if (hitter != null) damage = hitter.Health;
        OnDamageTaken?.Invoke(gameObject, damage);

        m_damageNumbers[DamageNumberIndex].Initialize(damage, transform.position);
        DamageNumberIndex += 1;

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
        yield return new WaitForSeconds(0.1f);
        m_spriteRenderer.color = lastColor;
    }

    protected virtual void Die()
    {
        if (m_isDead) return;
        m_spriteRenderer.color = Color.white;
        m_isDead = true;
        gameObject.SetActive(false);
        OnDeath?.Invoke(this);
    }

    public virtual void Initialize()
    {
        m_isDead = false;
    }

    protected virtual void InitializeCreations()
    {
        m_damageNumbers = new DamageNumber[m_maxDamageNumbers];
        GameObject tempObj = new(name + "'s Damage Numbers");
        tempObj.transform.SetParent(CreationsHolder.Transform);
        for (int i = 0; i < m_damageNumbers.Length; i++)
        {
            GameObject number = new GameObject("Damage Number " + (i + 1));
            number.transform.SetParent(tempObj.transform);
            m_damageNumbers[i] = number.AddComponent<DamageNumber>();
        }
    }
}
