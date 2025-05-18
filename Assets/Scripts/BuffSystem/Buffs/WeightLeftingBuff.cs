using UnityEngine;

public class WeightLeftingBuff : Buff
{
    protected int index;

    public WeightLeftingBuff(int input)
    {
        buffName = "Weight Lifting Buff";
        index = input;
    }

    public override void Apply(GameObject go)
    {
        IMoveSpeedBuff controller = go.GetComponent<PlayerController2>() as IMoveSpeedBuff;
        if (controller != null)
        {
            controller.AddMoveSpeedLevel(index);
        }
    }

    public override void Remove(GameObject go)
    {
        IMoveSpeedBuff controller = go.GetComponent<PlayerController2>() as IMoveSpeedBuff;
        if (controller != null)
        {
            controller.RemoveMoveSpeedLevel(index);
        }
    }
}
