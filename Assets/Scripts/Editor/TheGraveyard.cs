#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UIElements;

public class TheGraveyard
{
    // Hey, i'll use this script to just store things i had to rework. For documentation purposes.
    // (Even if yes, someone could just go to past commits).
    // And the separation of the code is the /**/ (treat those like {}).
    // Any comments inside them were not made
    // here in the graveyard and were already on the code.

    // I had this system as enemy movement. Thinking it was nice and clever.
    // Before i had the simpler and way more functional idea, to just make it random, 
    // which also enables the enemy to spawn at a completely random position, preventing
    // instant spawn killing.
    // Previously Inside Enemy Movement
    /*
    private IEnumerator Move(bool isXFlipped = false, bool isYFlipped = false)
    {
        while (true)
        {
            for (int i = 0; i < m_path.Movements.Count; i++)
            {
                for (int j = 0; j < m_path.Movements[i].RepeatAmount; j++)
                {
                    ClampInBounds();
                    yield return new WaitForSeconds(m_moveSpeed);

                    if (isXFlipped && m_path.Movements[i].Direction.x != 0)
                    {
                        m_rb2d.position += -m_path.Movements[i].Direction; continue;
                    }

                    if (isYFlipped && m_path.Movements[i].Direction.y != 0)
                    {
                        m_rb2d.position += -m_path.Movements[i].Direction; continue;
                    }

                    m_rb2d.position += m_path.Movements[i].Direction;
                }
            }
        }
    }
    */
    /*
    public class EnemyPattern
    {
        public List<Movement> Movements = new List<Movement>();
    }
    */
    /*
    public class Movement
    {
        public Vector2 Direction;
        public int RepeatAmount;
    }
    */
    // Previously Inside Enemy Handler
    /*
    [SerializeField] private EnemyPattern[] m_patterns;
    */
    // And
    /*
    m_enemysMovement[i].Initialize(m_waveData.MoveSpeeds, m_patterns[0]);
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], true); // The three tested and working.
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], true, true); // I'll leave them for testing.
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], false, true);
    */
    /*
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
    */

    // In Bullet
    /*
    private IEnumerator InitializeMovement(float time, float distance, float angleZ)
    {
        WaitForSeconds wait = new WaitForSeconds(time);
        while (true)
        {
            yield return wait;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleZ);
            Vector3 fallDirection = -transform.up;
            transform.position += fallDirection * distance;
            transform.rotation = rotation;
            ClampInBounds();
        }
    }
    */

    // Player movement previously had collisions, removed after I decided to implement the
    // take and deal dmg system from broke and out.
    /*private bool m_isColliding;*/
    // And
    /*
    private void OnCollisionStay2D(Collision2D collider)
    {
        m_isColliding = true;
        if (collider.gameObject.CompareTag("Enemy"))
        {
            ResolveOverlaps();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) => m_isColliding = false;
    */
    // And
    /*
    // generated by Claude. I understand what it does. I did have to modify it a little though. (It didn't even work at first)
    private void ResolveOverlaps()
    {
        Collider2D[] nearby = Physics2D.OverlapBoxAll(m_rb2d.position, m_boxCol2d.size, quaternion.identity.value.z, LayerMask.GetMask("Default"));

        foreach (var other in nearby)
        {
            if (other == m_boxCol2d) continue;

            ColliderDistance2D dist = Physics2D.Distance(m_boxCol2d, other);

            if (dist.isOverlapped)
            {
                Vector2 pushOut = dist.normal * -dist.distance;

                m_rb2d.position += -pushOut * 1.5f;
            }
        }
    }
    //
    */

    // The way the enemy spacer originally worked was that i tried doing it so
    // it would automatically know the right amount to move to the other side.
    // Before i realized it wasn't properly working so i just made it shift
    // half of the enemies from one side to the other and vice-versa which
    // solves the issue i had with it.
    // And it kinda of just made them group in the middle.
    // New one just works. If i wipe down the entirety of
    // a half it'll repopulate quickly and spread.
    // Old one:
    /*private void CheckSide(Collider2D[] side1, Collider2D[] side2, bool goRight)
    {
        ErrorLogger.DebugLog($"side1 lenght: {side1.Length}");
        ErrorLogger.DebugLog($"side2 lenght: {side2.Length}");
        ErrorLogger.DebugLog("---------------");
        foreach (Collider2D enemy in side1)
        {
            ErrorLogger.DebugLog($"side1: {enemy.name}");
        }
        foreach (Collider2D enemy in side1)
        {
            ErrorLogger.DebugLog($"side2: {enemy.name}");
        }
        ErrorLogger.DebugLog("---------------");
        if (side1 != null && side1.Length != 0)
        {
            if (side2.Length < side1.Length)
            {
                StartCoroutine(PauseTimer());

                int diff = side1.Length - side2.Length;
                if (diff < 0) diff = side2.Length - side1.Length;

                ErrorLogger.DebugLog($"diff: {diff}");
                ErrorLogger.DebugLog($"direction: goRight={goRight}");
                ErrorLogger.DebugLog("changing enemy sides");
                if (diff > 1) diff = (int)(diff * 1.5f);

                if (diff > side1.Length || diff > side2.Length ||
                    side1.Length == 0 || side2.Length == 0) return;

                for (int i = 0; i < side1.Length; i++)
                {
                    EnemyMovement enemy = side1[i].GetComponent<EnemyMovement>();

                    if (enemy.ChangeSidesPriority > 15)
                    {
                        StartCoroutine(enemy.ChangeSides(goRight));
                        diff--;
                        if (diff == 0) return;
                    }
                }
            }
        }*/

    // Not quite fitting as the code still exists but before the shoots were separated
    // they were inside their enemy behaviors, but then I stumbled upon the problem that
    // I couldn't mix aimbot with multi shots sooo i had to rework the system and
    // separated them out in new classes. (And the multi-shooting originally was hard
    // coded to 3 shots and 45 angle)

    // The fading system before i fully thought of just fusing and generalizing.
    // Inside Fading menu.
    /*private IEnumerator EnableFade()
    {
        while (Alpha < 1)
        {
            Alpha += 0.05f;
            m_canvasGroup.alpha = Alpha;
            yield return m_fadeTimer;
        }
    }
    private IEnumerator DisableFade()
    {
        while (Alpha > 0)
        {
            Alpha -= 0.05f;
            m_canvasGroup.alpha = Alpha;
            print(Alpha);
            yield return m_fadeTimer;
        }
    }*/
    // But then even after generalizing i talked a bit with Claude andd he showed me
    // that i just made something already existing (MoveTowards) so that also got scrapped lol.
    /*private IEnumerator Fade(float alpha, float alpha2, float incrementation, bool isInversed)
    {
        // enable: Alpha < 1
        // disable: Alpha > 0
        while (alpha < alpha2)
        {
            Alpha += incrementation;
            if (!isInversed) alpha = Alpha;
            else alpha2 = Alpha;
            m_canvasGroup.alpha = Alpha;
            yield return m_fadeTimer;
        }
    }*/
    // The old disable call
    /*m_fadeCoroutine = StartCoroutine(Fade(0, Alpha, -0.05f, true));*/
    // The old enable call
    /*m_fadeCoroutine = StartCoroutine(Fade(Alpha, 1, 0.05f, false));*/

    // Claude also helped me see that the overlay window script was kinda rough
    // as it had tacky solutions from the past that were not needed anymore and
    // a accidental infinite while true i hadn't caught. I suspect it was from how it was
    // set up before or i just didn't spot it.
    /*private IEnumerator OpenOverlayWindow()
    {
        for (int i = 0; i < 10; i++)
            yield return null;
        ErrorLogger.DebugLog("reached openoverlay");
        bool isPaused = Time.timeScale == 0;
        StartCoroutine(UpdateTitle());
        StartCoroutine(ComponentsActivation(isPaused));
        foreach (var window in m_otherWindows) window.GetComponent<FadingMenu>().Disable();
    }

    private IEnumerator ComponentsActivation(bool isPaused)
    {
        for (int i = 0; i < 10; i++)
        {
            EnableOrDisable(isPaused, m_components);
            EnableOrDisable(isPaused, m_background);
            yield return null;
        }
    }
    private IEnumerator UpdateTitle()
    {
        while (true)
        {
            while (!m_windowTitle.gameObject.activeInHierarchy) yield return null;
            m_windowTitle.text = HandleTitle();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }*/

    // Before there was a inheriting class for enemy types as i had not realized i could just
    // copy the way i had in player behavior to achieve multiple modifiers.
    // But since its a bit of various pieces of code everywhere im not going to
    // put them here (You can check past versions of commits though i think).
    // It is a nearly-entire rework.

    // Bullet Data wasss really bad, i don't know how i did not think about it
    // but claude helped me notice it.
    /*public BulletData(int damage, Vector2 size, float moveInterval, float moveDistance)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
    }

    public BulletData(int damage, EntityType target, Color color, Vector2 size, float moveInterval, float moveDistance, float angleZ)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        Target = target;
        BulletColor = color;
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
        Direction = angleZ;
        FixedDirection = angleZ;
    }

    public static BulletData DefaultAll() => new(1, EntityType.Player, Color.red, new(0.25f, 0.25f), 0.05f, 0.1f, 0f);
    public static BulletData Default() => new(1, new(0.25f, 0.25f), 0.05f, 0.1f);
    public void SetMainStats(int damage, Vector2 size, float moveInterval, float moveDistance)
    {
        m_damage = Mathf.Clamp(damage, 0, 10);
        m_size.x = Mathf.Clamp(size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(moveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(moveDistance, 0.1f, 2f);
    }
    public void SetMainStats(BulletData data)
    {
        m_damage = Mathf.Clamp(data.Damage, 0, 10);
        m_size.x = Mathf.Clamp(data.Size.x, 0.1f, 1f);
        m_size.y = Mathf.Clamp(data.Size.y, 0.1f, 1f);
        m_moveInterval = Mathf.Clamp(data.MoveInterval, 0.04f, 1f);
        m_moveDistance = Mathf.Clamp(data.MoveDistance, 0.1f, 2f);
    }*/
    //
}
#endif