using UnityEngine;

public class StrengthBuff : Buff
{
    protected int index;

    public StrengthBuff(int input)
    {
        buffName = "Strenth Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {

    }

    public override void Remove(GameObject go)
    {
    }
}
