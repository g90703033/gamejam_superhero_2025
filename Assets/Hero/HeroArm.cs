using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class HeroArm : MonoBehaviour
{
    public enum ArmType
    {
        Right,
        Left
     }
    public ArmType armType;

    public Hero hero;
    public List<HoldableObject> holdableObjectList;
    public float holdingWeight;

    public UnityEvent onArmBrokenEvent;

    public GameObject deadVFX;

    public void AddHoldableObject(HoldableObject holdableObject)
    {
        holdableObjectList.Add(holdableObject);

        holdingWeight += holdableObject.weight;

        if (holdingWeight > hero.maxHoldingWeight)
        {
            ReleaseObjects();
            onArmBrokenEvent.Invoke();
            hero.OnArmBroken(armType);

            deadVFX.SetActive(true);
            Invoke("StopVFX", 3f);
        }
    }

    public void StopVFX()
    {
        deadVFX.SetActive(false);
     }

    public void RemoveHoldableObject(HoldableObject holdableObject)
    {
        if (holdableObjectList.Contains(holdableObject))
        {
            holdableObjectList.Remove(holdableObject);
        }

        holdingWeight -= holdableObject.weight;
    }
    public void ThrowObjects(Vector3 force)
    {
        for (int i = 0; i < holdableObjectList.Count; i++)
        {
            holdableObjectList[i].ThrowObject(force);
        }

        holdableObjectList.Clear();
        holdingWeight = 0f;
    }

    public void ReleaseObjects()
    {
        for (int i = 0; i < holdableObjectList.Count; i++)
        {
            holdableObjectList[i].ReleaseObject();
         }
        holdableObjectList.Clear();
        holdingWeight = 0f;
    }
}
