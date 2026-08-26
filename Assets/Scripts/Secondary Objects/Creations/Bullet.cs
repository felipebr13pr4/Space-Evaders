using System.Collections;
using UnityEngine;

public class Bullet : Entity
{
    private BulletData m_data;
    private bool m_started = false;

    protected override void Start()
    {
        GameObject spriteChild = new("Sprite");
        spriteChild.transform.parent = transform;
        spriteChild.transform.position = transform.position;
        spriteChild.AddComponent<SpriteRenderer>();
        base.Start();
        InitializeBullet();
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

    private void DealDamage(EntityBehavior target)
    {
        ErrorLogger.DebugLog("dealing dmg " + target.gameObject);
        if (target != null)
            target.TakeDamage(1);
        gameObject.SetActive(false);
    }

    private void InitializeBullet()
    {
        m_started = true;

        m_spriteRenderer.color = m_data.Color;
        m_spriteRenderer.sprite = Resources.Load<Sprite>("Square");
        m_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        m_spriteRenderer.size = m_data.Size;
        m_spriteRenderer.sortingOrder = -1;

        m_boxCol2d.isTrigger = true;
        m_boxCol2d.size = m_data.Size;

        m_rb2d.bodyType = RigidbodyType2D.Kinematic;
        m_rb2d.freezeRotation = true;

        StartCoroutine(InitializeMovement(m_data.MoveTime, m_data.MoveDistance, m_data.Direction));
    }

    public void InitializeStats(BulletData data)
    {
        gameObject.SetActive(false);
        m_data = data;
    }

    private IEnumerator InitializeMovement(float time, float distance, float angleZ)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleZ);
            Vector3 fallDirection = -transform.up;
            transform.position += fallDirection * distance;
            transform.rotation = rotation;
            ClampInBounds();
        }
    }

    protected override void ClampInBounds()
    {
        Vector2 pos = m_rb2d.position;
        Vector2 sizeAdjustment = new(m_spriteRenderer.size.x / 2, m_spriteRenderer.size.y / 2);
        float posxL = ScreenBounds.Left + sizeAdjustment.x;
        float posxR = ScreenBounds.Right - sizeAdjustment.x;
        float posyB = ScreenBounds.Bottom + sizeAdjustment.y;
        float posyT = ScreenBounds.Top - sizeAdjustment.y;

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
