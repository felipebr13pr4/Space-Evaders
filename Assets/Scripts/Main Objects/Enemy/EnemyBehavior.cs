using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyBehavior : RangedEntityBehavior
{
    public static event Action<EnemyBehavior> OnActive;
    public virtual Color EnemyColor => EnemyColors.EnemyColor;
    public virtual Color EnemyBulletColor => EnemyColors.EnemyBulletColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnActive?.Invoke(this);
        if (isActiveAndEnabled) StartCoroutine(StartShooting());
    }

    protected override void Start()
    {
        BulletsData = new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0);
        base.Start();
    }

    protected override void SetColor()
    {
        m_color = EnemyColor;
        BulletsData.Color = EnemyBulletColor;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) Die(); // for testing.
    }
#endif
}