using System.Collections;
using UnityEngine;

public class RangedEntityBehavior : EntityBehavior
{
    private WaitForSeconds m_fireRateWait;
    private float m_fireRate = 3;
    public virtual float FireRate
    {   get => m_fireRate;
        set
        {   m_fireRate = value;
            m_fireRate = Mathf.Clamp(m_fireRate, FireRateMin, m_fireRateMax);
            m_fireRateWait = new WaitForSeconds(m_fireRate); } }
    protected virtual float FireRateMin => 0.5f;
    private readonly float m_fireRateMax = WaveData.Default().MaxFireRate;
    protected virtual int BulletsAmount => 25;
    [SerializeField] protected GameObject[] m_bulletsObjs;
    public GameObject[] BulletsObj => m_bulletsObjs;
    [SerializeField] protected Bullet[] m_bullets;
    [SerializeField] protected BulletData m_bulletData;
    public BulletData BulletsData
    {   get { m_bullets[BulletIndex].InitializeStats(m_bulletData); return m_bulletData; }
        set { m_bulletData = value; m_bullets[BulletIndex].InitializeStats(m_bulletData); } }
    protected int m_bulletIndex;
    public int BulletIndex
    {   get => m_bulletIndex;
        set { m_bulletIndex = value; if (BulletIndex >= BulletsAmount) BulletIndex = 0; } }
    [SerializeField] protected Shooting m_basicShooter;
    protected virtual int ShootDirection => 0;
    protected Color m_bulletColor;
    protected Color BulletColor
    { set { m_bulletColor = value;
            BulletsData.BulletColor = m_bulletColor; } }
    [SerializeField] protected AimbotModifier m_aimbotModifier;
    public AimbotModifier AimbotModifier => m_aimbotModifier;
    [SerializeField] protected MultiShooting m_multiShootModifier;
    public MultiShooting MultiShootModifier => m_multiShootModifier;

    protected override void Start()
    {
        base.Start();
        m_bulletColor = BulletsData.BulletColor;
        BulletsData.FixedDirection = ShootDirection;
    }

    public override void InitializeCreations(Transform storageLocation)
    {
        base.InitializeCreations(storageLocation);

        m_bulletsObjs = new GameObject[BulletsAmount];
        m_bullets = new Bullet[BulletsAmount];

        GameObject tempObj2 = new(name + "'s Bullets");
        tempObj2.transform.SetParent(storageLocation);

        for (int i = 0; i < m_bulletsObjs.Length; i++)
        {
            m_bulletsObjs[i] = new GameObject("Bullet " + (i + 1));
            m_bulletsObjs[i].transform.SetParent(tempObj2.transform);
            m_bullets[i] = m_bulletsObjs[i].AddComponent<Bullet>();
            m_bullets[i].InitializeStats(BulletsData);
            m_bullets[i].gameObject.SetActive(false);

            GameObject spriteChild = new("Sprite");
            spriteChild.transform.parent = m_bullets[i].gameObject.transform;
            spriteChild.transform.position = m_bullets[i].gameObject.transform.position;
            SpriteRenderer spritechildRen = spriteChild.AddComponent<SpriteRenderer>();
            spritechildRen.sprite = Resources.Load<Sprite>("Square");
        }
    }

    public IEnumerator StartShooting()
    {
        yield return null;
        yield return null;
        
        while (true)
        {
            foreach (Bullet bullet in m_bullets)
            {
                yield return m_fireRateWait;
                if (!gameObject.activeInHierarchy) yield break;
                while (m_multiShootModifier.IsShooting) yield return null;
                m_bullets[BulletIndex].InitializeStats(BulletsData);
                ShootBullet(BulletIndex);
            }
        }
    }

    protected virtual void ShootBullet(int i)
    {
        BulletsData.Direction = BulletsData.FixedDirection;
        m_basicShooter.ShootBullet(m_bulletsObjs[i]);
        BulletIndex++;

        if (m_multiShootModifier.isActiveAndEnabled) StartCoroutine(m_multiShootModifier.ShootBullet());
    }
}
