using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CreationsHolder : MonoBehaviour
{
    public static Transform Transform;

    private void OnEnable()
    {
        Transform = GetComponent<Transform>();
    }
}