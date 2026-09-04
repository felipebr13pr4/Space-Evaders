using UnityEngine;

public class Bullet : Entity
{
    // Known bug, i don't know where to put this so i'll put it here.
    // Im still trying to fix it. But for some reason bullets are going realllyyy fast for no reason.
    // The weirdest part is that when you pause and that bullet exists, it just... Keeps going.
    // The transform y just keeps decreasing even while everything is paused.
    // This miiiiiiiight be because of the testing wave skip. As I think I did not notice it
    // happening in normal gameplay when not doing the test wave skip keybind.
    // After testing by going to a higher wave (13) without skipping them (but cheating for it
    // to be faster) i thiiink it is indeed the wave skipping causing weird things.
    // Nvm. It happens even while not skipping and just killing enemies...
    // Might be because its going too fast? I do realize that on wave 50 its probally that
    // As theres no cap on mov interval so it drops to 0. The value it decreases results in that
    // 0.001*50=0.05 which is the set value. The actual decrease i wanted was probally 0.0002.
    // I don't quite know if the thing I do will fix it so i'll keep this here for now.

    private BulletData m_data;
    private bool m_started = false;
    private float m_moveTimer;
    private float m_moveInterval;
    private float m_moveDistance;
    private Quaternion m_moveRotation;

    protected override void Start()
    {
        base.Start();
        Initialize();
        InitializeBullet();
        m_started = true;
    }

    private void OnEnable()
    {
        if (m_started) InitializeBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_data.Target == EntityType.Player)
        {
            if (!collision.CompareTag("Player")) return;
            DealDamage(collision.GetComponent<EntityBehavior>());
        }
        else if (m_data.Target == EntityType.Enemy)
        {
            if (!collision.CompareTag("Enemy")) return;
            DealDamage(collision.GetComponent<EntityBehavior>());
        }
    }

    private void Update()
    {
        m_moveTimer += Time.deltaTime;
        if (m_moveTimer >= m_moveInterval)
        {   m_moveTimer -= m_moveInterval;
            Vector3 fallDirection = -transform.up;
            transform.position += fallDirection * m_moveDistance;
            DeactivateOutBounds(); }
    }

    private void DealDamage(EntityBehavior target)
    {
        ErrorLogger.DebugLog("dealing dmg " + target.gameObject);
        if (target != null)
            target.TakeDamage(m_data.Damage);
        gameObject.SetActive(false);
    }

    private void Initialize()
    {
        if (m_spriteRenderer.sprite == null)
            m_spriteRenderer.sprite = Resources.Load<Sprite>("Square");
        m_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        m_spriteRenderer.sortingOrder = -1;

        m_boxCol2d.isTrigger = true;

        m_rb2d.bodyType = RigidbodyType2D.Kinematic;
        m_rb2d.freezeRotation = true;
    }

    private void InitializeBullet()
    {
        m_spriteRenderer.color = m_data.Color;
        m_spriteRenderer.size = m_data.Size;
        m_boxCol2d.size = m_data.Size;
        m_moveInterval = m_data.MoveInterval;
        m_moveDistance = m_data.MoveDistance;
        m_moveRotation = Quaternion.Euler(0f, 0f, m_data.Direction);

        transform.rotation = m_moveRotation;
    }

    public void InitializeStats(BulletData data)
    {
        m_data = data;
    }

    private void DeactivateOutBounds()
    {
        Vector2 pos = m_rb2d.position;
        float posxL = ScreenBounds.Left - m_sizeAdjustment.x;
        float posxR = ScreenBounds.Right + m_sizeAdjustment.x;
        float posyB = ScreenBounds.Bottom - m_sizeAdjustment.y;
        float posyT = ScreenBounds.Top + m_sizeAdjustment.y;

        if (m_rb2d.position.x < posxL || m_rb2d.position.x > posxR)
        {
            gameObject.SetActive(false);
        }
        else if (m_rb2d.position.y < posyB || m_rb2d.position.y > posyT)
        {
            gameObject.SetActive(false);
        }
    }
}
