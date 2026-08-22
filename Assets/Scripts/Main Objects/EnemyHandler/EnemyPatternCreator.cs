using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPatternCreator : MonoBehaviour
{
    // I don't know where to place this so i'll put this here.
    // A heads up is that if you want to actually create patterns and have them be saved.
    // Since its play mode and nothing saves, you have to copy the elements
    // (RMB on "▼ Patterns" in the unity inspector) and then copy.
    // Now leave play mode and paste it on the same spot.

    [SerializeField] private List<EnemyPattern> m_patterns;
    [SerializeField] private int m_target;

    private void Start()
    {
        if (m_patterns[m_target].Movements.Count == 0) m_patterns[m_target].Movements.Add(new());
    }

    private void Update()
    {
        float xDir = 0;

        xDir += Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.rightArrowKey.wasPressedThisFrame ? 1 : 0;

        xDir -= Keyboard.current.aKey.wasPressedThisFrame ||
            Keyboard.current.leftArrowKey.wasPressedThisFrame ? 1 : 0;


        float yDir = 0;

        yDir += Keyboard.current.wKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame ? 1 : 0;

        yDir -= Keyboard.current.sKey.wasPressedThisFrame ||
            Keyboard.current.downArrowKey.wasPressedThisFrame ? 1 : 0;

        transform.position += new Vector3(xDir, yDir, 0);

        if (xDir != 0 || yDir != 0)
        {
            if (m_patterns[m_target].Movements[^1].Direction != new Vector2(xDir, yDir))
                m_patterns[m_target].Movements.Add(new());

            var targetMovement = m_patterns[m_target].Movements[^1];

            if (targetMovement.Direction == new Vector2(xDir, yDir))
            { targetMovement.RepeatAmount += 1; return; }

            targetMovement.Direction = new(xDir, yDir);
            targetMovement.RepeatAmount += 1;
        }
        if (Keyboard.current.tKey.isPressed) m_patterns[m_target].Movements.Clear();

        if (transform.position == new Vector3(-14.5f, 0, 0) &&
            m_patterns[m_target].Movements.Count > 1)
        {
            m_patterns.Add(new());
            m_target = m_patterns.Count - 1;
            m_patterns[m_target].Movements.Add(new());
            ErrorLogger.DebugLog("Returned to beginning, creating a new pattern and selecting it.");
        }
    }

    [ContextMenu("Set Target As Lastest Pattern")]
    private void SetTargetAsLastestPattern() => m_target = m_patterns.Count - 1;
}
