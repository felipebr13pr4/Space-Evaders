using UnityEngine;

public class AimbotEnemyBehavior : EnemyBehavior
{
    protected override void EditBullet(int i)
    {
        // By Claude. Basically I just needed to know the math for this. (Modified)
        Vector3 dir = PlayerBehavior.Transform.position - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        //
        m_bulletData.Direction = angle + 90;
        base.EditBullet(i);
    }

    protected override void OnceEditBullet()
    {
        m_bulletData.Color = new(0.8f, 0, 1f);
    }
}
