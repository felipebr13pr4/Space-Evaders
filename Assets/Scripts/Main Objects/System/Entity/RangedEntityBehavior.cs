using System.Collections;
using UnityEngine;

public class RangedEntityBehavior : EntityBehavior
{
    [SerializeField] protected float m_fireRate = 3;
    private GameObject[] m_bullets;
    private readonly int m_maxBullets = 35;
    protected BulletData m_bulletData;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        m_bullets = new GameObject[m_maxBullets];

        GameObject tempObj2 = new(name + "'s Bullets");
        tempObj2.transform.SetParent(CreationsHolder.Transform);

        for (int i = 0; i < m_bullets.Length; i++)
        {   m_bullets[i] = new GameObject("Bullet " + (i+1));
            m_bullets[i].transform.SetParent(tempObj2.transform);
            Bullet bullet = m_bullets[i].AddComponent<Bullet>();
            bullet.InitializeStats(m_bulletData);
        }

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
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
