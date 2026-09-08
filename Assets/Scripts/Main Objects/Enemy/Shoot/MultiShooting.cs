using System.Collections;
using UnityEngine;

public class MultiShooting : SpecialShooter
{
    [SerializeField] private int m_multiShotAmount = 2;
    public int MultiShotAmount => m_multiShotAmount;
    [SerializeField] private int m_multiShotAngle = 45;
    public int MultiShotAngle => m_multiShotAngle;

    public IEnumerator ShootBullet()
    {
        int amount = m_multiShotAmount;
        int angle = m_multiShotAngle;
        GameObject[] bulletObjs = new GameObject[amount];

        yield return null;
        yield return null;

        for (int i = 0; i < amount;)
        {
            ModifyBullets(i, angle, bulletObjs);
            yield return null;
            yield return null;
            i++;

            if (i == amount) break;

            ModifyBullets(i, -angle, bulletObjs);
            yield return null;
            yield return null;
            i++;
        }

        m_entity.BulletsData.Direction = m_entity.BulletsData.FixedDirection;
    }

    public void ModifyBullets(int i, int angle, GameObject[] bulletObjs)
    {
        bulletObjs[i] = m_entity.BulletsObj[m_entity.BulletIndex];
        m_entity.BulletsData.Direction += angle * (i + 1);
        ShootBullet(bulletObjs[i]);
        m_entity.BulletIndex++;
    }
}