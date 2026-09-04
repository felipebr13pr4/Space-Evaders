using UnityEngine;

public class EnemyColors : MonoBehaviour
{
    public static Color EnemyColor => new(1, 0.75f, 0.75f);
    public static Color EnemyBulletColor => new(1f, 0.35f, 0.35f);
    public static Color AimbotEnemyColor => new(0.9f, 0.75f, 1f);
    public static Color AimbotEnemyBulletColor => new(0.8f, 0, 1f);
}