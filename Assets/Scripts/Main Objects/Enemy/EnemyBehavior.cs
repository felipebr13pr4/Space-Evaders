using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehavior : RangedEntityBehavior
{
    protected override void Start()
    {
        base.Start();
        m_bulletData = new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0);
        m_color = new(1, 0.75f, 0.75f);
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) Die(); // for testing.
    }

    protected override void OnceEditBullet()
    {
        m_bulletData.Color = Color.red;
    }
}