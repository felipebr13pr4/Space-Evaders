using System.Collections;
using UnityEngine;

public class MultiShooting : SpecialShooter
{
    public IEnumerator ShootBullet(int amount, int angle)
    {
        GameObject[] bulletObjs = new GameObject[amount];

        yield return null;
        yield return null;

        int j = 0;
        for (int i = 0; i < amount; i++)
        {
            ModifyBullets(i, j, angle, bulletObjs);
            yield return null;
            yield return null;
            i++; j++;

            ModifyBullets(i, j, -angle, bulletObjs);
            yield return null;
            yield return null;
            i++; j++;
        }

        m_entity.BulletsData.Direction = m_entity.BulletsData.FixedDirection;
    }

    public void ModifyBullets(int i, int j, int angle, GameObject[] bulletObjs)
    {
        bulletObjs[j] = m_entity.BulletsObj[m_entity.BulletIndex];
        m_entity.BulletsData.Direction += angle * (i + 1);
        ShootBullet(bulletObjs[j]);
        m_entity.BulletIndex++;
    }
}