using UnityEngine;
using WildWind.ObjectPool;

public class VirtualShadow : MonoBehaviour
{
    public PooledObject pooledObject;
    public Renderer m_renderer;

    private Material m_marerial;
    public Material shadowMaterial
    {
        get
        {
            if (m_renderer == null)
            {
                return null;
            }
            else if (m_marerial == null)
            {
                m_marerial = m_renderer.material;
            }

            return m_marerial;
        }
    }

    public void SetActive(bool isActive)
    {
        if (m_renderer.enabled != isActive)
        {
            m_renderer.enabled = isActive;
         }
     }

    public void SetScale(float val)
    {
        transform.localScale = Vector3.one * val;
    }

    public void SetAlpha(float progress)
    {
        shadowMaterial.SetFloat("_Alpha", Mathf.Lerp(1f, VirtualShadowConsole.instance.minAlpha, progress)); // semi-transparent
     }
}
