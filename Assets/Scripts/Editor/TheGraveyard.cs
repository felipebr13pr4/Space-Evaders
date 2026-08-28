#if UNITY_EDITOR
public class TheGraveyard
{
    // Hey, i'll use this script to just store things i had to rework. For documentation purposes.
    // (Even if yes, someone could just go to past commits)

    // I had this system as enemy movement. Thinking it was nice and clever.
    // Before i had the simpler and way more functional idea, to just make it random, 
    // which also enables the enemy to spawn at a completely random position, preventing
    // instant spawn killing.

    // Previously Inside Enemy Movement
    /*private IEnumerator Move(bool isXFlipped = false, bool isYFlipped = false)
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
    }*/

    /*public class EnemyPattern
    {
        public List<Movement> Movements = new List<Movement>();
    }*/
    /*public class Movement
    {
        public Vector2 Direction;
        public int RepeatAmount;
    }*/

    // Previously Inside Enemy Handler
    /*[SerializeField] private EnemyPattern[] m_patterns;*/
    // And
    /*m_enemysMovement[i].Initialize(m_waveData.MoveSpeeds, m_patterns[0]);
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], true); // The three tested and working.
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], true, true); // I'll leave them for testing.
    //m_enemysMovement[i].Initialize(0.5f, m_patterns[0], false, true);*/

    /*public class EnemyPatternCreator : MonoBehaviour
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
    }*/

    // In Bullet
    /*private IEnumerator InitializeMovement(float time, float distance, float angleZ)
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
    }*/
    //
}
#endif