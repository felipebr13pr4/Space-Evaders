using System.Collections;
using UnityEngine;

public class RangedEntityBehavior : EntityBehavior
{
    [SerializeField] private float m_fireRate = 3;
    public float FireRate
    {   set
        {   m_fireRate = value;
            m_fireRate = Mathf.Clamp(m_fireRate, 0.2f, 20f); } }
    private GameObject[] m_bullets;
    private readonly int m_maxBullets = 55;
    private BulletData m_bulletData;
    public BulletData BulletData { set => m_bulletData = value; }

    protected override void Start()
    {
        m_bullets = new GameObject[m_maxBullets];
        base.Start();
        Initialize();
    }

    public void StartShooting() => StartCoroutine(Shoot());

    protected override void InitializeCreations()
    {
        base.InitializeCreations();
        GameObject tempObj2 = new(name + "'s Bullets");
        tempObj2.transform.SetParent(CreationsHolder.Transform);

        for (int i = 0; i < m_bullets.Length; i++)
        {
            m_bullets[i] = new GameObject("Bullet " + (i + 1));
            m_bullets[i].transform.SetParent(tempObj2.transform);
            Bullet bullet = m_bullets[i].AddComponent<Bullet>();
            bullet.InitializeStats(m_bulletData);
        }
    }

    private IEnumerator Shoot()
    {
        yield return null;
        yield return null;
        while (true)
        {
            for (int i = 0; i < m_bullets.Length; i++)
            {
                yield return new WaitForSeconds(m_fireRate);
                if (!gameObject.activeInHierarchy) yield break;
                if (!m_bullets[i].activeInHierarchy) m_bullets[i].SetActive(true);
                m_bullets[i].transform.position = transform.position;
            }
        }
    }
}
