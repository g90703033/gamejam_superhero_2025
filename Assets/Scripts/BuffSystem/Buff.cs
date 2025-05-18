using UnityEngine;
public abstract class Buff
{
    public string buffName;
    public abstract void Apply(GameObject go);
    public abstract void Remove(GameObject go);
}
