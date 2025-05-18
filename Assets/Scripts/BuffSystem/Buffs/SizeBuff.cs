using UnityEngine;

public class SizeBuff : Buff
{
    protected int index;

    public SizeBuff(int input)
    {
        buffName = "Size Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
    }

    public override void Remove(GameObject go)
    {
    }
}
