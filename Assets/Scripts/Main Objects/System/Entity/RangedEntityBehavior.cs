using System.Collections;
using UnityEngine;

public class RangedEntityBehavior : EntityBehavior
{
    private WaitForSeconds m_fireRateWait;
    private float m_fireRate = 3;
    public float FireRate
    {   set
        {   m_fireRate = value;
            m_fireRate = Mathf.Clamp(m_fireRate, 0.2f, 20f); } }
    private const int m_bulletsAmount = 55;
    protected GameObject[] m_bulletsObjs = new GameObject[m_bulletsAmount];
    protected Bullet[] m_bullets = new Bullet[m_bulletsAmount];
    protected BulletData m_bulletData;
    public BulletData BulletData { set => m_bulletData = value; }

    public void StartShooting() => StartCoroutine(Shoot());

    public override void InitializeCreations()
    {
        base.InitializeCreations();
        GameObject tempObj2 = new(name + "'s Bullets");
        tempObj2.transform.SetParent(CreationsHolder.Transform);

        for (int i = 0; i < m_bulletsObjs.Length; i++)
        {
            m_bulletsObjs[i] = new GameObject("Bullet " + (i + 1));
            m_bulletsObjs[i].transform.SetParent(tempObj2.transform);
            m_bullets[i] = m_bulletsObjs[i].AddComponent<Bullet>();
            m_bullets[i].InitializeStats(m_bulletData);
            m_bullets[i].gameObject.SetActive(false);

            GameObject spriteChild = new("Sprite");
            spriteChild.transform.parent = m_bullets[i].gameObject.transform;
            spriteChild.transform.position = m_bullets[i].gameObject.transform.position;
            spriteChild.AddComponent<SpriteRenderer>();
        }
    }

    private IEnumerator Shoot()
    {
        yield return null;
        yield return null;

        m_fireRateWait = new WaitForSeconds(m_fireRate);
        OnceEditBullet();

        while (true)
        {
            for (int i = 0; i < m_bulletsObjs.Length; i++)
            {
                yield return m_fireRateWait;
                if (!gameObject.activeInHierarchy) yield break;
                EditBullet(i);
                if (!m_bulletsObjs[i].activeInHierarchy) m_bulletsObjs[i].SetActive(true);
                m_bulletsObjs[i].transform.position = transform.position;
            }
        }
    }

    protected virtual void EditBullet(int i)
    {
        m_bullets[i].InitializeStats(m_bulletData);
    }

    protected virtual void OnceEditBullet() { }
}
