using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float m_speed = 6;
    protected Rigidbody2D m_rigidBody2d;
    private Vector2 m_moveDir;
    private Vector2 MoveDir {
        get => m_moveDir;
        set {
            m_moveDir = value;
            m_moveDir = new(Mathf.Clamp(m_moveDir.x, -1, 1), Mathf.Clamp(m_moveDir.y, -1, 1));
        }
    }
    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_rigidBody2d = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        m_rigidBody2d.linearVelocity = m_moveDir * m_speed;

        Vector3 pos = m_rigidBody2d.transform.position;
        float sizeAdjustmentX = m_spriteRenderer.size.x / 2;
        pos.x = Mathf.Clamp(pos.x, ScreenBounds.Left + sizeAdjustmentX,
                            ScreenBounds.Right - sizeAdjustmentX);
        m_rigidBody2d.position = pos;
    }
}