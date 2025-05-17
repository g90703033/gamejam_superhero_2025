using UnityEngine;

public class BuffCarry : MonoBehaviour
{
    // which is id
    public string title;
    public BuffAttribute attribute;
    bool inited = false;

    public void Update()
    {
        if (!inited)
        {
            attribute = BuffManager.Instance.GetAttribute(title);

            if (attribute != null ) { inited = true; }
        }
    }
}
