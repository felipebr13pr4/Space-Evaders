using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Entity
{
    [SerializeField] protected float m_speed = 6;
    public float Speed { set { m_speed = value; m_speed = Mathf.Clamp(m_speed, 0.2f, 20f); } }
    private Vector2 m_moveDir;
    private Vector2 MoveDir {
        get => m_moveDir;
        set {
            m_moveDir = value;
            m_moveDir = new(Mathf.Clamp(m_moveDir.x, -1, 1), Mathf.Clamp(m_moveDir.y, -1, 1));
        }
    }

    private void Update()
    {
        float xDir = 0;

        xDir += Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed ? 1 : 0;

        xDir -= Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed ? 1 : 0;
        

        float yDir = 0;

        yDir += Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed ? 1 : 0;

        yDir -= Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed ? 1 : 0;

        MoveDir = new(xDir, yDir);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_rb2d.linearVelocity = MoveDir * m_speed;

        ClampInBounds();
    }
    
    protected override float YClamp(float y)
    {
        return Mathf.Clamp(y, ScreenBounds.Bottom + m_sizeAdjustment.y,
                            (ScreenBounds.Top - m_sizeAdjustment.y) - 1.75f);
    }
}