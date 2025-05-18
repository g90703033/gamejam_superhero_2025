using UnityEngine;

public class SoftShadowController : MonoBehaviour
{
    [Header("Shadow Settings")]
    public GameObject shadowPrefab;          // Assign the shadow quad prefab in Inspector
    public float maxHeight = 5f;             // Max height where shadow still visible
    public float maxScale = 3f;              // Scale multiplier at max height
    public float minAlpha = 0.2f;            // Alpha at max height

    [Header("Raycast Settings")]
    public LayerMask groundLayer;            // Layer(s) considered ground
    public float raycastDistance = 10f;

    private GameObject shadowInstance;
    private Material shadowMaterial;

    void Start()
    {
        // Instantiate shadow under the object
        if (shadowPrefab != null)
        {
            shadowInstance = Instantiate(shadowPrefab);
            shadowMaterial = shadowInstance.GetComponent<Renderer>().material;
        }
        else
        {
            Debug.LogError("Shadow Prefab not assigned.");
        }
    }

    void Update()
    {
        if (shadowInstance == null) return;

        RaycastHit hit;
        Vector3 origin = transform.position;

        if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            float height = Mathf.Clamp(hit.distance, 0.01f, maxHeight);
            float t = height / maxHeight;

            // Scale shadow based on height
            float scale = Mathf.Lerp(1f, maxScale, t);
            shadowInstance.transform.localScale = new Vector3(scale, scale, scale);

            // Set opacity
            if (shadowMaterial != null)
            {
             //   shadowMaterial.SetColor("_ShadowColor", new Color(0, 0, 0, 1)); // black
                shadowMaterial.SetFloat("_Alpha", Mathf.Lerp(1f, minAlpha, t)); // semi-transparent
            }

            // Move shadow to hit point (slightly above to avoid z-fighting)
            Vector3 shadowPos = hit.point + Vector3.up * 0.01f;
            shadowInstance.transform.position = shadowPos;
        }
        else
        {
            // Hide or disable the shadow if no ground hit
            shadowInstance.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (shadowInstance != null)
        {
            Destroy(shadowInstance);
        }
    }
}
