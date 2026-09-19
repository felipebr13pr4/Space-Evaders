using System.Collections;
using UnityEngine;

public class EnemySpacer : MonoBehaviour
{

    // Fun fact about this, after playing normally with the cheat on player beh
    // i realized that its really annoying at later waves (like >12) when enemies just
    // stay on one side and barely move as with the firerate they have its just really hard
    // to peek in and shoot at them so at that point it turns into just "wait for rng to make them
    // slowly leave from their fortress".

    private const int m_checkTimerNumber = 6;
    private WaitForSeconds m_checkTimer = new(m_checkTimerNumber);
    protected Vector2 m_sizeAdjustment;
    private Vector3 m_leftPos;
    private Vector3 m_rightPos;
    private Vector3 m_leftSize;
    private Vector3 m_rightSize;
    private Coroutine m_checkCoroutine;


    private void Start()
    {
        m_sizeAdjustment = new(transform.localScale.x / 2, transform.localScale.y / 2);
        
        m_leftPos = new(transform.position.x - (m_sizeAdjustment.x / 2),
            transform.position.y, transform.position.z);
        m_rightPos = new(transform.position.x + (m_sizeAdjustment.x / 2),
            transform.position.y, transform.position.z);
        
        m_leftSize = new(transform.localScale.x - (m_sizeAdjustment.x),
            transform.localScale.y, transform.localScale.z);
        m_rightSize = new(transform.localScale.x - (m_sizeAdjustment.x),
            transform.localScale.y, transform.localScale.z);
        if (m_checkCoroutine != null) StopCoroutine(m_checkCoroutine);
        m_checkCoroutine = StartCoroutine(CheckEnemies());
    }

    private IEnumerator CheckEnemies()
    {
        while (true)
        {
            yield return m_checkTimer;
            Collider2D[] leftSide = Physics2D.OverlapBoxAll(m_leftPos,
                m_leftSize, Quaternion.identity.z, LayerMask.GetMask("Enemy"));

            Collider2D[] rightSide = Physics2D.OverlapBoxAll(m_rightPos,
                m_rightSize, Quaternion.identity.z, LayerMask.GetMask("Enemy"));

            CheckSide(leftSide, rightSide, true);
            CheckSide(rightSide, leftSide, false);
            yield return null;
        }
    }

    private void CheckSide(Collider2D[] side1, Collider2D[] side2, bool goRight)
    {
        /*foreach (Collider2D enemy in side1)
        {
            ErrorLogger.DebugLog($"side1: {enemy.name}");
        }
        foreach (Collider2D enemy in side1)
        {
            ErrorLogger.DebugLog($"side2: {enemy.name}");
        }*/
        if (side1 != null && side1.Length != 0)
        {
            if (side2.Length < side1.Length)
            {
                StartCoroutine(PauseTimer());

                for (int i = 0; i < Mathf.RoundToInt(side1.Length/2); i++)
                {
                    EnemyMovement enemy = side1[i].GetComponent<EnemyMovement>();
                    StartCoroutine(enemy.ChangeSides(goRight));
                }
            }
        }
    }

    private IEnumerator PauseTimer()
    {
        if (m_checkCoroutine != null) StopCoroutine(m_checkCoroutine);
        yield return new WaitForSeconds(m_checkTimerNumber*2);
        m_checkCoroutine = StartCoroutine(CheckEnemies());
    }

    [ContextMenu("Set Size to camera bounds")]
    private void SetSize()
    {
        transform.localScale = new(ScreenBounds.Right * 2, ScreenBounds.Top * 2);
    }

    [ContextMenu("Check if the calculation sizes are correct")]
    private void CheckSizes()
    {
        GameObject obj = new("test - delete me later");
        GameObject objL = new("testLe - delete me later");
        GameObject objR = new("testRi - delete me later");
        Vector2 sizeAdjustment = new(transform.localScale.x / 2, transform.localScale.y / 2);
        Vector3 leftPos = new(transform.position.x - (sizeAdjustment.x / 2), transform.position.y, transform.position.z);
        Vector3 rightPos = new(transform.position.x + (sizeAdjustment.x / 2), transform.position.y, transform.position.z);
        Vector3 leftSize = new(transform.localScale.x - (sizeAdjustment.x), transform.localScale.y, transform.localScale.z);
        Vector3 rightSize = new(transform.localScale.x - (sizeAdjustment.x), transform.localScale.y, transform.localScale.z);
        objL.transform.parent = obj.transform;
        objR.transform.parent = obj.transform;
        objL.transform.position = leftPos;
        objL.transform.localScale = leftSize;
        objR.transform.position = rightPos;
        objR.transform.localScale = rightSize;
        objL.AddComponent<BoxCollider2D>();
        objR.AddComponent<BoxCollider2D>();
    }
}
