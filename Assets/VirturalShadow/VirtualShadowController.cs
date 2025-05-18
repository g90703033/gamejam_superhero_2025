using UnityEngine;

public class VirtualShadowController : MonoBehaviour
{
    public LayerMask groundLayer
    {
        get
        {
            return VirtualShadowConsole.instance.groundLayer;
         }
     }           // Layer(s) considered ground
    public float raycastDistance 
    {
        get
        {
            return VirtualShadowConsole.instance.raycastDistance;
         }
     }         
    public float maxHeight
    {
        get
        {
            return VirtualShadowConsole.instance.maxHeight;
        }
    } 
    public float maxScale
    {
        get
        {
            return VirtualShadowConsole.instance.maxScale;
        }
    } 
    public float minAlpha
    {
        get
        {
            return VirtualShadowConsole.instance.minAlpha;
        }
    }

    public VirtualShadow virtualShadow;
    public bool UseShadow
    {
        get
        {
            return virtualShadow != null;
         }
     }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UseShadow)
        {
            RaycastHit hit;
            Vector3 origin = transform.position;

            if (Physics.Raycast(origin, Vector3.down, out hit, raycastDistance, groundLayer))
            {
                float height = Mathf.Clamp(hit.distance, 0.01f, maxHeight);
                float t = height / maxHeight;

                // Scale shadow based on height
                float scale = Mathf.Lerp(1f, maxScale, t);
                virtualShadow.SetScale(scale);
                //shadowInstance.transform.localScale = new Vector3(scale, scale, scale);

                // Set opacity
                /*if (shadowMaterial != null)
                {
                    shadowMaterial.SetFloat("_Alpha", Mathf.Lerp(1f, minAlpha, t)); // semi-transparent
                }*/
                virtualShadow.SetAlpha(t);

                // Move shadow to hit point (slightly above to avoid z-fighting)
                Vector3 shadowPos = hit.point + Vector3.up * 0.01f;
                virtualShadow.transform.position = shadowPos;

                virtualShadow.SetActive(true);
            }
            else
            {
                // Hide or disable the shadow if no ground hit
                virtualShadow.SetActive(false);
            }
        }
    }

    public void EnableShadow()
    {
        GameObject shadowObj = VirtualShadowConsole.instance.shadowPool.GetPooledInstance(null);
        virtualShadow = shadowObj.GetComponent<VirtualShadow>();
        virtualShadow.SetActive(false);
    }

    public void DisableShadow()
    {
        virtualShadow.pooledObject.BackToPool();

        virtualShadow = null;
     }
}
