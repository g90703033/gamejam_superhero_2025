using UnityEngine;
using WildWind.ObjectPool;

public class VirtualShadowConsole : MonoBehaviour
{
    public static VirtualShadowConsole instance;
    public ObjectPoolSystem shadowPool;

    [Header("Shadow Settings")]      // Assign the shadow quad prefab in Inspector
    public float maxHeight = 5f;             // Max height where shadow still visible
    public float maxScale = 3f;              // Scale multiplier at max height
    public float minAlpha = 0.2f;            // Alpha at max height

    [Header("Raycast Settings")]
    public LayerMask groundLayer;            // Layer(s) considered ground
    public float raycastDistance = 10f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
         }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
