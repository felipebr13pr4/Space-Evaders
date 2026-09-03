using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyBehavior : RangedEntityBehavior
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (isActiveAndEnabled) StartCoroutine(StartShooting());
    }

    protected override void Start()
    {
        BulletsData = new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0);
        base.Start();
    }

    protected override void SetColor()
    {
        m_color = new(1, 0.75f, 0.75f);
        BulletsData.Color = new(1f, 0.35f, 0.35f);
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) Die(); // for testing.
    }
}