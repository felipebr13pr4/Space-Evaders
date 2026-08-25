using System.Collections;
using UnityEngine;

public class RangedEntityBehavior : EntityBehavior
{
    [SerializeField] private float m_fireRate = 3;
    public float FireRate { set { m_fireRate = value; } }
    private GameObject[] m_bullets;
    private readonly int m_maxBullets = 35;
    private Vector2 m_shootDirection; // To be used to decide if the one shooting is enemy or player.

    public override void Initialize(int health)
    {
        base.Initialize(health);
        m_bullets = new GameObject[m_maxBullets];

        GameObject tempObj = new(name + " Bullets");
        tempObj.transform.SetParent(CreationsHolder.Transform);
        for (int i = 0; i < m_bullets.Length; i++)
        {   m_bullets[i] = new GameObject("Bullet");
            m_bullets[i].transform.SetParent(tempObj.transform);
            m_bullets[i].SetActive(false); }

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
