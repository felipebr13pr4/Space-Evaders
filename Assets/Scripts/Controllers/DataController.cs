using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
     
    private void Start()
    {
        // Put things when there is a stats to record.
    }

    private void OnEnable()
    {
        // Put things when there is something to listen and increase stats.
    }

    private void OnDisable()
    {
        // Put things when there is something to listen and increase stats.
    }
}
