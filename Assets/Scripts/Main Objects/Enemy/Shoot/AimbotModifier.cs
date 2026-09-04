using UnityEngine;

public class AimbotModifier : SpecialShooter
{
    public void ModifyBullet(Vector3 target)
    {
        // By Claude. Basically I just needed to know the math for this. (Modified)
        Vector3 dir = target - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        //
        m_entity.BulletsData.FixedDirection = angle + 90;
    }
}