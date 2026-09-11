using System.Collections;
using UnityEngine;

public class MultiShooting : SpecialShooter
{
    [SerializeField] private int m_multiShotAmount = 2;
    public int MultiShotAmount => m_multiShotAmount;
    [SerializeField] private int m_multiShotAngle = 45;
    public int MultiShotAngle => m_multiShotAngle;
    private bool m_isShooting;
    public bool IsShooting => m_isShooting;

    public void SetStats(int amount, int angle)
    {
        m_multiShotAmount = amount;
        m_multiShotAngle = angle;
    }

    public IEnumerator ShootBullet()
    {
        m_isShooting = true;
        
        int amount = m_multiShotAmount;
        int angle = m_multiShotAngle;
        GameObject[] bulletObjs = new GameObject[amount];

        yield return null;

        for (int i = 0; i < amount;)
        {
            ModifyBullets(i, angle, bulletObjs);
            i++;
            yield return null;

            if (i == amount) break;

            ModifyBullets(i, -angle, bulletObjs);
            i++;
            yield return null;
        }

        m_entity.BulletsData.Direction = m_entity.BulletsData.FixedDirection;
        m_isShooting = false;
    }

    public void ModifyBullets(int i, int angle, GameObject[] bulletObjs)
    {
        bulletObjs[i] = m_entity.BulletsObj[m_entity.BulletIndex];
        m_entity.BulletsData.Direction += angle * (i + 1);
        ShootBullet(bulletObjs[i]);
        m_entity.BulletIndex++;
    }
}