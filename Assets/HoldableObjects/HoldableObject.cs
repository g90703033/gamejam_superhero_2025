using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    public bool isFirstComing;
    public UnityEvent initEvent;
    public UnityEvent firstComingEndEvent;

    public virtual void EndFirstComing()
    {
        if (isFirstComing)
        {
            isFirstComing = false;

            firstComingEndEvent.Invoke();
         }
     }

    public virtual void Init()
    {
        isFirstComing = true;
        initEvent.Invoke();
     }

    public virtual void ThrowObject(Vector3 force)
    {

    }

    public virtual void ReleaseObject()
    {

     }
}
