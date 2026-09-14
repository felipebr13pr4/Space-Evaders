using System.Collections;
using UnityEngine;

public class MultiShooting : SpecialShooter
{
    [SerializeField] private int m_multiShotAmount = 0;
    public int MultiShotAmount
    {   get { return m_multiShotAmount; }
        set {   m_multiShotAmount = value;
                m_multiShotAmount = Mathf.Clamp(m_multiShotAmount, 0, 999);
                enabled = m_multiShotAmount > 0; } }
    [SerializeField] private float m_multiShotAngle = 45;
    public float MultiShotAngle 
    {   get { return m_multiShotAngle; }
        set {   m_multiShotAngle = value;
                m_multiShotAngle = Mathf.Clamp(m_multiShotAngle, 1f, 359f); } }
    private bool m_isShooting;
    public bool IsShooting => m_isShooting;
    private AudioHolder m_audioHolder;
    public AudioHolder AudioHolder { set => m_audioHolder = value; }
    private int m_audioIndex;
    public int AudioIndex { set => m_audioIndex = value; }
    private const int m_soundEachMax = 4;
    private int m_soundEach;


    public IEnumerator ShootBullet()
    {
        m_isShooting = true;
        
        int amount = m_multiShotAmount;
        float angle = m_multiShotAngle;
        GameObject[] bulletObjs = new GameObject[amount];
        m_soundEach = m_soundEachMax;

        yield return null;

        for (int i = 0; i < amount;)
        {
            m_soundEach--;
            ModifyBullets(i, angle, bulletObjs, m_soundEach);
            i++;
            yield return null;

            if (i == amount) break;

            while (Time.timeScale == 0f)
                yield return null;

            m_soundEach--;
            ModifyBullets(i, -angle, bulletObjs, m_soundEach);
            i++;
            yield return null;
        }

        m_entity.BulletsData.Direction = m_entity.BulletsData.FixedDirection;
        m_isShooting = false;
    }

    public void ModifyBullets(int i, float angle, GameObject[] bulletObjs, int tmpSoundEach)
    {
        if (m_audioHolder != null && tmpSoundEach == 0)
            { m_audioHolder.ActivateSound(m_audioIndex); m_soundEach = m_soundEachMax; }
        bulletObjs[i] = m_entity.BulletsObj[m_entity.BulletIndex];
        m_entity.BulletsData.Direction += angle * (i + 1);
        ShootBullet(bulletObjs[i]);
        m_entity.BulletIndex++;
    }
}