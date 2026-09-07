using System;
using System.Collections;
using UnityEngine;

public class EntityBehavior : Entity
{
    private static readonly WaitForSeconds m_damageFlashDuration = new(0.1f);
    [SerializeField] protected int m_maxHealth = 3;
    public virtual int MaxHealth { get => m_maxHealth; set { m_maxHealth = value; Health = value; } }
    private int m_lastHealth;
    private int m_health;
    public virtual int Health
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
    [SerializeField] private DamageNumber[] m_damageNumbers;
    private const int m_maxDamageNumbers = 10;
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
    private Color m_spriteColor;

    protected override void Start()
    {
        base.Start();
        m_spriteColor = m_spriteRenderer.color;
        Initialize();
    }

    public virtual void TakeDamage(int damage = 0, EntityBehavior hitter = null, bool takeAndDeal = false)
    {
        if (hitter != null) damage = hitter.Health;
        OnDamageTaken?.Invoke(gameObject, damage);

        m_damageNumbers[DamageNumberIndex].Initialize(damage, transform.position);
        DamageNumberIndex += 1;

        if (isActiveAndEnabled) StartCoroutine(FlashDamage());

        if (hitter != null)
        {   Health -= damage;
            if (takeAndDeal) hitter.TakeDamage(m_lastHealth);
            return; }
        Health -= damage;
    }

    private IEnumerator FlashDamage()
    {
        m_spriteRenderer.color = Color.red;
        yield return m_damageFlashDuration;
        m_spriteRenderer.color = m_spriteColor;
    }

    protected virtual void Die()
    {
        if (m_isDead) return;
        m_spriteRenderer.color = m_spriteColor;
        m_spriteRenderer.gameObject.SetActive(false);
        m_isDead = true;
        gameObject.SetActive(false);
        OnDeath?.Invoke(this);
    }

    public virtual void Initialize()
    {
        m_isDead = false;
    }

    public virtual void InitializeCreations(Transform storageLocation)
    {
        m_damageNumbers = new DamageNumber[m_maxDamageNumbers];
        GameObject tempObj = new(name + "'s Damage Numbers");
        tempObj.transform.SetParent(storageLocation);

        for (int i = 0; i < m_damageNumbers.Length; i++)
        {
            GameObject number = new("Damage Number " + (i + 1));
            number.transform.SetParent(tempObj.transform);
            m_damageNumbers[i] = number.AddComponent<DamageNumber>();
        }
    }
}
