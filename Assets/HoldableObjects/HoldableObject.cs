using Unity.VisualScripting;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public float weight = 60f;
    public ConfigurableJoint attachedJoint;
    public HeroArm attachedArm;
    public Hero attachedHero
    {
        get
        {
            if (attachedArm != null)
                return attachedArm.hero;
            else
                return null;
         }
     }

    public bool IsAttached
    {
        get
        {
            return attachedJoint != null;
        }
    }

    public virtual void ThrowObject(Vector3 force)
    {

     }

    public virtual void ReleaseObject()
    {

     }
}
