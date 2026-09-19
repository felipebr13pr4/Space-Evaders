using UnityEngine;

public class Bullet : Entity
{
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

        gameObject.layer = m_data.Target == EntityType.Player ? LayerMask.NameToLayer("EnemyBullet") :
            LayerMask.NameToLayer("PlayerBullet");
    }

    private void InitializeBullet()
    {
        m_spriteRenderer.color = m_data.BulletColor;
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
