using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CreationsHolder : MonoBehaviour
{
    public static Transform Transform;

    private void Awake()
    {
        Transform = GetComponent<Transform>();
    }
}